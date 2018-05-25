/* ErrorList
 * http://Suplanus.de by Johann Weiher
 *
 * Control based on the Idea: http://errorlist.codeplex.com/
 *
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ErrorList {

    public partial class ErrorListControl {
        private readonly ErrorListDataModel _dataContext = new ErrorListDataModel();

        #region properties

        public IEnumerable<IMessageItem> Items { get { return (IEnumerable<IMessageItem>)GetValue(ItemsProperty); } set { SetValue(ItemsProperty, value); } }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(nameof(Items), typeof(IEnumerable<IMessageItem>), typeof(ErrorListControl), new FrameworkPropertyMetadata(null, (s, e) => {
            var t = (ErrorListControl)s;
            t._dataContext.Items = e.NewValue as IEnumerable<IMessageItem>;
        }));

        public IMessageItem SelectedItem { get { return (IMessageItem)GetValue(SelectedItemProperty); } set { SetValue(SelectedItemProperty, value); } }
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(nameof(SelectedItem), typeof(IMessageItem), typeof(ErrorListControl), new FrameworkPropertyMetadata());

        public IEnumerable<IMessageItem> SelectedItems { get { return (IEnumerable<IMessageItem>)GetValue(SelectedItemsProperty); } set { SetValue(SelectedItemsProperty, value); } }
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(nameof(SelectedItems), typeof(IEnumerable<IMessageItem>), typeof(ErrorListControl), new FrameworkPropertyMetadata());

        public DataGridSelectionMode SelectionMode { get { return (DataGridSelectionMode)GetValue(SelectionModeProperty); } set { SetValue(SelectionModeProperty, value); } }
        public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.Register(nameof(SelectionMode), typeof(DataGridSelectionMode), typeof(ErrorListControl), new FrameworkPropertyMetadata(DataGridSelectionMode.Single));

        #endregion properties

        public ErrorListControl() {
            InitializeComponent();

            dgv.DataContext = _dataContext;
            SetTextBoxBindings();
        }

        #region EventHandlers

        private void Errors_Checked(object sender, System.Windows.RoutedEventArgs e) {
            ToggleButton tgl = (ToggleButton)sender;
            _dataContext.ShowErrors = tgl.IsChecked.HasValue && tgl.IsChecked.Value;
        }

        private void Errors_Unchecked(object sender, System.Windows.RoutedEventArgs e) {
            ToggleButton tgl = (ToggleButton)sender;
            _dataContext.ShowErrors = tgl.IsChecked.HasValue && tgl.IsChecked.Value;
        }

        private void Warnings_Checked(object sender, System.Windows.RoutedEventArgs e) {
            ToggleButton tgl = (ToggleButton)sender;
            _dataContext.ShowWarnings = tgl.IsChecked.HasValue && tgl.IsChecked.Value;
        }

        private void Warnings_Unchecked(object sender, System.Windows.RoutedEventArgs e) {
            ToggleButton tgl = (ToggleButton)sender;
            _dataContext.ShowWarnings = tgl.IsChecked.HasValue && tgl.IsChecked.Value;
        }

        private void Informations_Checked(object sender, System.Windows.RoutedEventArgs e) {
            ToggleButton tgl = (ToggleButton)sender;
            _dataContext.ShowInformations = tgl.IsChecked.HasValue && tgl.IsChecked.Value;
        }

        private void Informations_Unchecked(object sender, System.Windows.RoutedEventArgs e) {
            ToggleButton tgl = (ToggleButton)sender;
            _dataContext.ShowInformations = tgl.IsChecked.HasValue && tgl.IsChecked.Value;
        }

        private void Notes_Checked(object sender, System.Windows.RoutedEventArgs e) {
            ToggleButton tgl = (ToggleButton)sender;
            _dataContext.ShowNotes = tgl.IsChecked.HasValue && tgl.IsChecked.Value;
        }

        private void Notes_Unchecked(object sender, System.Windows.RoutedEventArgs e) {
            ToggleButton tgl = (ToggleButton)sender;
            _dataContext.ShowNotes = tgl.IsChecked.HasValue && tgl.IsChecked.Value;
        }

        private void dgv_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e) {
            var collection = this.SelectedItems as ObservableCollection<IMessageItem>;
            if (collection is null) {
                collection = new ObservableCollection<IMessageItem>();
            }

            var selectedItems = this.dgv.SelectedCells
                .Select(x => x.Item)
                .OfType<IMessageItem>()
                .Distinct()
                .ToList();

            var removed = collection.Except(selectedItems).ToList();
            var added = selectedItems.Except(collection).ToList();

            foreach (var item in removed) {
                collection.Remove(item);
            }
            foreach (var item in added) {
                collection.Add(item);
            }

            this.SelectedItems = collection;
            this.SelectedItem = collection.FirstOrDefault();
        }

        #endregion EventHandlers

        private void SetTextBoxBindings() {
            txtErrors.DataContext = _dataContext;
            txtWarnings.DataContext = _dataContext;
            txtInformations.DataContext = _dataContext;
            txtNotes.DataContext = _dataContext;
        }
    }
}
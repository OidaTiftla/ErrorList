using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace ErrorList {

    public class ErrorListDataModel : INotifyPropertyChanged {

        public IEnumerable<IMessageItem> Items {
            get { return this._errorListData; }
            set {
                if (this._errorListData is INotifyCollectionChanged old_ncc) {
                    old_ncc.CollectionChanged -= this.Items_CollectionChanged;
                }
                this._errorListData = value;
                if (value is INotifyCollectionChanged new_ncc) {
                    new_ncc.CollectionChanged += this.Items_CollectionChanged;
                }
                SetView();
            }
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            SetView();
        }

        public string ErrorsText {
            get {
                return string.Format("{0} Errors", _errorListData?.Count(ed => ed.Level == ErrorListLevel.Error) ?? 0);
            }
        }

        public string WarningsText {
            get {
                return string.Format("{0} Warnings", _errorListData?.Count(ed => ed.Level == ErrorListLevel.Warning) ?? 0);
            }
        }

        public string InformationsText {
            get {
                return string.Format("{0} Informations", _errorListData?.Count(ed => ed.Level == ErrorListLevel.Information) ?? 0);
            }
        }

        public string NotesText {
            get {
                return string.Format("{0} Notes", _errorListData?.Count(ed => ed.Level == ErrorListLevel.Note) ?? 0);
            }
        }

        public ObservableCollection<IMessageItem> ErrorListData {
            get {
                return _errorListDataView;
            }
        }

        public bool ShowErrors {
            set {
                _showErrors = value;
                SetView();
            }
        }

        public bool ShowWarnings {
            set {
                _showWarnings = value;
                SetView();
            }
        }

        public bool ShowInformations {
            set {
                _showInformations = value;
                SetView();
            }
        }

        public bool ShowNotes {
            set {
                _showNotes = value;
                SetView();
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion INotifyPropertyChanged Members

        private void SetView() {
            var selectedLevels = new List<ErrorListLevel>();
            if (_showErrors)
                selectedLevels.Add(ErrorListLevel.Error);
            if (_showWarnings)
                selectedLevels.Add(ErrorListLevel.Warning);
            if (_showInformations)
                selectedLevels.Add(ErrorListLevel.Information);
            if (_showNotes)
                selectedLevels.Add(ErrorListLevel.Note);

            var selectedErrors = _errorListData?.Where(ed => selectedLevels.Contains(ed.Level))?.ToList() ?? new List<IMessageItem>();
            var removed = _errorListDataView.Except(selectedErrors).ToList();
            var added = selectedErrors.Except(_errorListDataView).ToList();
            foreach (var item in removed) {
                _errorListDataView.Remove(item);
            }
            foreach (var item in added) {
                _errorListDataView.AddSorted(item, x => selectedErrors.IndexOf(x));
            }

            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ErrorsText)));
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(WarningsText)));
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(InformationsText)));
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(NotesText)));
            }
        }

        private IEnumerable<IMessageItem> _errorListData = null;
        private readonly ObservableCollection<IMessageItem> _errorListDataView = new ObservableCollection<IMessageItem>();

        private bool _showErrors = true;
        private bool _showWarnings = true;
        private bool _showInformations = true;
        private bool _showNotes = true;
    }
}
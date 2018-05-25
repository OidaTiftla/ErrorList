using ErrorList;
using System.Collections.ObjectModel;
using System.Windows;

namespace ErrorListTest {

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private ObservableCollection<IMessageItem> _messages = new ObservableCollection<IMessageItem>();

        public MainWindow() {
            InitializeComponent();

            this.errorList.Items = _messages;

            // Performance-Test
            //for (int i = 0; i < 1000; i++)
            //{
            _messages.Add(new MessageItem() { Description = "Error unable to do something \"Name: Write PHP\", because the syntax is so looooooooooong", Level = ErrorListLevel.Error });
            _messages.Add(new MessageItem() { Description = "Error unable to do something \"Name: Write Flash\"", Level = ErrorListLevel.Error });
            _messages.Add(new MessageItem() { Description = "Error unable to do something \"Name: Program in F#, yet\"", Level = ErrorListLevel.Warning });
            _messages.Add(new MessageItem() { Description = "Info: your wasting your lunch coding..", Level = ErrorListLevel.Information });
            _messages.Add(new MessageItem() { Description = "Note: I need a better hobby than wasting my lunch coding..", Level = ErrorListLevel.Note });

            _messages.Add(new MessageItem() { Description = "Error unable to do something \"Name: Write PHP\", because the syntax is so looooooooooong", Level = ErrorListLevel.Error });
            _messages.Add(new MessageItem() { Description = "Error unable to do something \"Name: Write Flash\"", Level = ErrorListLevel.Error });
            _messages.Add(new MessageItem() { Description = "Error unable to do something \"Name: Program in F#, yet\"", Level = ErrorListLevel.Warning });
            _messages.Add(new MessageItem() { Description = "Info: your wasting your lunch coding..", Level = ErrorListLevel.Information });
            _messages.Add(new MessageItem() { Description = "Note: I need a better hobby than wasting my lunch coding..", Level = ErrorListLevel.Note });

            _messages.Add(new MessageItem() { Description = "Error unable to do something \"Name: Write PHP\", because the syntax is so looooooooooong", Level = ErrorListLevel.Error });
            _messages.Add(new MessageItem() { Description = "Error unable to do something \"Name: Write Flash\"", Level = ErrorListLevel.Error });
            _messages.Add(new MessageItem() { Description = "Error unable to do something \"Name: Program in F#, yet\"", Level = ErrorListLevel.Warning });
            _messages.Add(new MessageItem() { Description = "Info: your wasting your lunch coding..", Level = ErrorListLevel.Information });
            _messages.Add(new MessageItem() { Description = "Note: I need a better hobby than wasting my lunch coding..", Level = ErrorListLevel.Note });
            //}
        }

        private void BtnAddSampleData_OnClick(object sender, RoutedEventArgs e) {
            _messages.Add(new MessageItem() { Description = "Error unable to do something \"Name: Write PHP\"", Level = ErrorListLevel.Error });
            _messages.Add(new MessageItem() { Description = "Error unable to do something \"Name: Write Flash\"", Level = ErrorListLevel.Error });
            _messages.Add(new MessageItem() { Description = "Error unable to do something \"Name: Program in F#, yet\"", Level = ErrorListLevel.Warning });
            _messages.Add(new MessageItem() { Description = "Info: your wasting your lunch coding..", Level = ErrorListLevel.Information });
            _messages.Add(new MessageItem() { Description = "Note: I need a better hobby than wasting my lunch coding..", Level = ErrorListLevel.Note });
        }

        private void BtnRemoveSampleData_OnClick(object sender, RoutedEventArgs e) {
            _messages.Clear();
        }
    }
}
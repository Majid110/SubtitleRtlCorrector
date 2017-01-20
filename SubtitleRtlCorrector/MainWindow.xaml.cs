using System.Windows;
using System.Windows.Controls;
using SubtitleRtlCorrector.Helper;
using SubtitleRtlCorrector.ViewModel;

namespace SubtitleRtlCorrector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new MainWindowViewModel();
            DataContext = vm;
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedLanguage = vm.SelectedLanguage.Code;
            var previousLanguage = vm.PreviousLanguage.Code;
            CultureHelper.SetLanguage(selectedLanguage, previousLanguage);
            vm.PreviousLanguage = vm.SelectedLanguage;

        }
    }
}

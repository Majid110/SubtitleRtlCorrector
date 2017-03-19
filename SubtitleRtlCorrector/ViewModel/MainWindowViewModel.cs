using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using SubtitleCorrectorEngine;
using SubtitleRtlCorrector.Helper;
using SubtitleRtlCorrector.Model;

namespace SubtitleRtlCorrector.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            LoadResources();
            createCommands();
            SelectedLanguage = LanguageList.FirstOrDefault(x => x.Code == Thread.CurrentThread.CurrentCulture.Name);
            PreviousLanguage = SelectedLanguage;
            SpecialChars = AppConfigHelper.Instance.SpecialChars;
            StickyChars = AppConfigHelper.Instance.StickyChars;
            StartingBrackets = AppConfigHelper.Instance.StartingBracketChars;
            EndingBrackets = AppConfigHelper.Instance.EndingBracketChars;
            CorrectRtl = true;
            CorrectSpaces = true;
        }

        #region Properties

        public RelayCommand CopyToClipBoardCommand { get; private set; }
        public RelayCommand ClearEditorCommand { get; private set; }
        public RelayCommand BrowseFileCommand { get; private set; }
        public RelayCommand CorrectAndSaveFileCommand { get; private set; }
        public RelayCommand ResetFileChangesCommand { get; private set; }

        private List<Languages> _languageList;
        public List<Languages> LanguageList
        {
            get { return _languageList; }
            set
            {
                _languageList = value;
                RaisePropertyChanged(() => LanguageList);
            }
        }

        private Languages _selectedLanguage;
        public Languages SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                _selectedLanguage = value;
                RaisePropertyChanged(() => SelectedLanguage);
            }
        }

        private Languages _previousLanguage;
        public Languages PreviousLanguage
        {
            get { return _previousLanguage; }
            set
            {
                _previousLanguage = value;
                RaisePropertyChanged(() => PreviousLanguage);
            }
        }

        private string _fileText;
        public string FileText
        {
            get { return _fileText; }
            set
            {
                _fileText = value;
                RaisePropertyChanged(() => FileText);
            }
        }

        private string _specialChars;
        public string SpecialChars
        {
            get { return _specialChars; }
            set
            {
                _specialChars = value;
                AppConfigHelper.Instance.SpecialChars = _specialChars;
                RaisePropertyChanged(() => SpecialChars);
            }
        }

        private string _stickyChars;
        public string StickyChars
        {
            get { return _stickyChars; }
            set
            {
                _stickyChars = value;
                AppConfigHelper.Instance.StickyChars = _stickyChars;
                RaisePropertyChanged(() => StickyChars);
            }
        }

        private string _editorText;
        public string EditorText
        {
            get { return _editorText; }
            set
            {
                _editorText = value;
                RaisePropertyChanged(() => EditorText);
            }
        }

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                RaisePropertyChanged(() => FileName);
            }
        }

        private string _startingBrackets;
        public string StartingBrackets
        {
            get { return _startingBrackets; }
            set
            {
                _startingBrackets = value;
                AppConfigHelper.Instance.StartingBracketChars = _startingBrackets;
                RaisePropertyChanged(() => StartingBrackets);
            }
        }

        private string _endingBrackets;
        public string EndingBrackets
        {
            get { return _endingBrackets; }
            set
            {
                _endingBrackets = value;
                AppConfigHelper.Instance.EndingBracketChars = _endingBrackets;
                RaisePropertyChanged(() => EndingBrackets);
            }
        }

        private bool _correctRtl;
        public bool CorrectRtl
        {
            get { return _correctRtl; }
            set
            {
                _correctRtl = value;
                RaisePropertyChanged(() => CorrectRtl);
            }
        }

        private bool _correctSpaces;
        public bool CorrectSpaces
        {
            get { return _correctSpaces; }
            set
            {
                _correctSpaces = value;
                RaisePropertyChanged(() => CorrectSpaces);
            }
        }

        #endregion

        #region Private Methods

        private void LoadResources()
        {
            LanguageList = new List<Languages>
            {
                new Languages() {Code = "en-US", Name = "English"},
                new Languages() {Code = "fa-IR", Name = "Persian"}
            };
        }

        private void createCommands()
        {
            CopyToClipBoardCommand = new RelayCommand(copyToClipboard, canCopy);
            ClearEditorCommand = new RelayCommand(clearEditor, canClear);
            BrowseFileCommand = new RelayCommand(browseFile);
            CorrectAndSaveFileCommand = new RelayCommand(correctAndSaveFile, canCorrectAndSave);
            ResetFileChangesCommand = new RelayCommand(resetFileChanges, canCorrectAndSave);
        }

        private bool canCorrectAndSave()
        {
            return !string.IsNullOrEmpty(FileName);
        }

        private bool canClear()
        {
            return !string.IsNullOrEmpty(EditorText);
        }

        private bool canCopy()
        {
            return !string.IsNullOrEmpty(_editorText);
        }

        private void copyToClipboard()
        {
            var txtEditorText = _editorText;
            if (string.IsNullOrEmpty(txtEditorText)) return;
            var correctOption = new CorrectOption()
            {
                SpecialChars = SpecialChars,
                StickyChars = StickyChars,
                StartingBracketChars = StartingBrackets,
                EndingBracketChars = EndingBrackets,
                CorrectRtl = this.CorrectRtl,
                CorrectSpaces = this.CorrectSpaces
            };
            var core = new CorrectorCore(txtEditorText, correctOption);
            var corrected = core.ResetChanges();
            core = new CorrectorCore(corrected, correctOption);
            corrected = core.Correct();
            Clipboard.SetText(corrected);
        }

        private void resetFileChanges()
        {
            if (checkFileName()) return;
            var fileText = File.ReadAllText(FileName);
            var core = new CorrectorCore(fileText, new CorrectOption());
            var reseted = core.ResetChanges();
            saveNewFile(reseted);
        }

        private void correctAndSaveFile()
        {
            if (checkFileName()) return;
            var fileText = File.ReadAllText(_fileName);
            var correctOption = new CorrectOption()
            {
                SpecialChars = SpecialChars,
                StickyChars = StickyChars,
                StartingBracketChars = StartingBrackets,
                EndingBracketChars = EndingBrackets,
                FileExt = ".ass",
                CorrectRtl = this.CorrectRtl,
                CorrectSpaces = this.CorrectSpaces
            };
            var core = new CorrectorCore(fileText, correctOption);
            var corrected = core.ResetChanges();
            core = new CorrectorCore(corrected, correctOption);
            corrected = core.Correct();
            saveNewFile(corrected);
        }

        private bool checkFileName()
        {
            if (!string.IsNullOrEmpty(FileName)) return false;
            MessageBox.Show("هیچ فایلی انتخاب نشده است", "خطا", MessageBoxButton.OK, MessageBoxImage.Warning);
            return true;
        }

        private void saveNewFile(string corrected)
        {
            var saveDlg = new SaveFileDialog()
            {
                OverwritePrompt = true,
                FileName = "New_" + Path.GetFileName(FileName)
            };
            if ((bool)!saveDlg.ShowDialog()) return;
            File.WriteAllText(saveDlg.FileName, corrected, Encoding.UTF8);
            MessageBox.Show("فایل ذخیره شد", "ذخیره فایل", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void browseFile()
        {
            var openDlg = new OpenFileDialog()
            {
                Filter = "*.ass files|*.ass"
            };
            if ((bool)!openDlg.ShowDialog()) return;
            FileName = openDlg.FileName;
        }

        private void clearEditor()
        {
            EditorText = null;
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using SubtitleRtlCorrector.Helper;
using SubtitleRtlCorrector.Model;
using SubtitleRtlCorrector.ViewModel;

namespace SubtitleRtlCorrector
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            SetImportCatalog();
            SetLanguage();
        }

        private void SetImportCatalog()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                var catalog = new DirectoryCatalog(path);
                CompositionContainer container = new CompositionContainer(catalog);
                container.ComposeParts(BaseModel.Instance.ImportCatalog);

            }
            catch (Exception ex)
            {

            }
        }

        private void SetLanguage()
        {
            try
            {
                
                string cultureCode = AppConfigHelper.Instance.SelectedLanguage;
                CultureInfo cultureInfo = new CultureInfo(cultureCode);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                var dictionary = (from d in BaseModel.Instance.ImportCatalog.ResourceDictionaryList
                                  where d.Metadata.ContainsKey("Culture")
                                  && d.Metadata["Culture"].ToString().Equals(cultureCode)
                                  select d).FirstOrDefault();
                if (dictionary != null && dictionary.Value != null)
                {
                    this.Resources.MergedDictionaries.Add(dictionary.Value);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            AppConfigHelper.Instance.SelectedLanguage = Thread.CurrentThread.CurrentCulture.Name;
            AppConfigHelper.SaveConfig(AppConfigHelper.Instance);
        }
    }
}

using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using SubtitleRtlCorrector.ViewModel;

namespace SubtitleRtlCorrector.Helper
{
    public class CultureHelper
    {
        public static void SetLanguage(string selectedLanguage, string previousLanguage)
        {
            var currentResourceDictionary = (from d in BaseModel.Instance.ImportCatalog.ResourceDictionaryList
                where d.Metadata.ContainsKey("Culture")
                      && d.Metadata["Culture"].ToString().Equals(selectedLanguage)
                select d).FirstOrDefault();
            if (currentResourceDictionary != null)
            {
                var previousResourceDictionary = (from d in BaseModel.Instance.ImportCatalog.ResourceDictionaryList
                    where d.Metadata.ContainsKey("Culture")
                          && d.Metadata["Culture"].ToString().Equals(previousLanguage)
                    select d).FirstOrDefault();
                if (previousResourceDictionary != null && previousResourceDictionary != currentResourceDictionary)
                {
                    Application.Current.Resources.MergedDictionaries.Remove(previousResourceDictionary.Value);
                    Application.Current.Resources.MergedDictionaries.Add(currentResourceDictionary.Value);
                    CultureInfo cultureInfo = new CultureInfo(selectedLanguage);
                    Thread.CurrentThread.CurrentCulture = cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = cultureInfo;
                    Application.Current.MainWindow.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
                }
            }
        }
    }
}
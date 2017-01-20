using System.ComponentModel.Composition;
using System.Windows;

namespace SubtitleCorrector.Resources
{
	[ExportMetadata("Culture", "fa-IR")]
	[Export(typeof(ResourceDictionary))]
	public partial class FrenchLanguage : ResourceDictionary
	{
		public FrenchLanguage()
		{
			InitializeComponent();
		}
	}
}

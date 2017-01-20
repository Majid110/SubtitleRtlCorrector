using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;

namespace SubtitleRtlCorrector.ViewModel
{
	public class ImportModule
	{
		[ImportMany(typeof(ResourceDictionary))]
		public IEnumerable<Lazy<ResourceDictionary, IDictionary<string, object>>> ResourceDictionaryList { get; set; }
	}
}

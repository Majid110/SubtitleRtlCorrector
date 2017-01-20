namespace SubtitleRtlCorrector.ViewModel
{
	public class BaseModel 
	{
		private static BaseModel _instance;
		public static BaseModel Instance
		{
			get
			{
				if (_instance == null)
					_instance = new BaseModel();
				return _instance;
			}
		}

		private  ImportModule _importCatalog;
		public ImportModule ImportCatalog
		{
			get
			{
				_importCatalog = _importCatalog ?? new ImportModule();
				return _importCatalog;
			}
		}

		
	}
}

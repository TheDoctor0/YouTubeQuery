using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YouTubeQuery
{
	public class Options
	{
		private readonly string _filePath;

		public string ApiKey;
		public bool Thumbnails;

		public Options(string path)
		{
			_filePath = path;
		}

		public void Load()
		{
			if (!File.Exists(_filePath)) return;
			
			var loaded = JsonConvert.DeserializeObject<Options>(File.ReadAllText(_filePath));

			ApiKey = loaded.ApiKey;
			Thumbnails = loaded.Thumbnails;
		}

		public void Save()
		{
			if (!File.Exists(_filePath)) return;

			File.WriteAllText(_filePath, JsonConvert.SerializeObject(this));
		}
	}
}

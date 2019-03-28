using System.Windows;
using System.Windows.Controls;

namespace YouTubeQuery
{
	public partial class SettingsControl
	{
		private readonly Options _options;

		public SettingsControl(Options current)
		{
			InitializeComponent();

			_options = current;
			ApiKey.Text = _options.ApiKey ?? "";
			ThumbnailsEnabled.IsChecked = _options.Thumbnails;
		}

		private void ApiKey_TextChanged(object sender, TextChangedEventArgs e)
		{
			_options.ApiKey = ApiKey.Text;
			_options.Save();
		}
		
		private void GetApiKey_Click(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start("https://www.google.com/search?q=how+to+get+youtube+api+key");
		}

		private void ThumbnailsEnabled_Click(object sender, RoutedEventArgs e)
		{
			_options.Thumbnails = ThumbnailsEnabled.IsChecked ?? false;
			_options.Save();
		}
	}
}

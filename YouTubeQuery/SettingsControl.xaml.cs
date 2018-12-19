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
	}
}

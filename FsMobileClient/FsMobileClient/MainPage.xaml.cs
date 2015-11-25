using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using HttpService;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace FsMobileClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly VideoService _videoService;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            _videoService = new VideoService("http://fs-proxy.azurewebsites.net");
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void saveUrlBtn_Click(object sender, RoutedEventArgs e)
        {
            _videoService.Host = urlInput.Text;
        }

        private async void playBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = await _videoService.PlayVideoAsync();
            if (result == string.Empty)
            {
                urlInput.Text = "play";
            }
        }

        private async void pauseBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = await _videoService.PauseVideoAsync();
            if (result == string.Empty)
            {
                urlInput.Text = "pause";
            }
        }

        private async void volumeSlider_LostFocus(object sender, RoutedEventArgs e)
        {
            var result = await _videoService.ChangeVolumeAsync(volumeSlider.Value / 100);
            if (result == string.Empty)
            {
                urlInput.Text = "volume";
            }
        }
    }
}

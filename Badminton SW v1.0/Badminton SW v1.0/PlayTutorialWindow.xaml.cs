using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Badminton_SW_v1._0
{
    /// <summary>
    /// Interaction logic for PlayTutorialWindow.xaml
    /// </summary>
    public partial class PlayTutorialWindow : Window
    {

        public PlayTutorialWindow(string video)
        {
            InitializeComponent();
            videoPlayer.Source = new Uri( video, UriKind.Relative);
        }

        private void playBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            videoPlayer.Play();
        }

        private void pauseBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            videoPlayer.Pause();
        }

        private void stopBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            videoPlayer.Stop();
        }
    }
}

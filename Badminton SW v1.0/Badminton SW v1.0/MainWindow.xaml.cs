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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Badminton_SW_v1._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RecordingWindow recordingWindow;
        private PracticeWindow practiceWindow;
        private PlayTutorialWindow tutorialWindow;
        public MainWindow()
        {
            InitializeComponent();
            Directory.CreateDirectory("Recordings");
            Directory.CreateDirectory("Trainee Reports");
        }

        private void RecordButton_Click(object sender, RoutedEventArgs e)
        {
            recordingWindow = new RecordingWindow();
            recordingWindow.WindowLoaded(sender, e);
        }

        private void PracticeButton_Click(object sender, RoutedEventArgs e)
        {
            practiceWindow = new PracticeWindow();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("C:/Users/Abd El-Rahman/SkyDrive/Dr. Mostafa Shawky/badminton/Backhand/Backhand Smash - www.thwack.co.mpg");
            tutorialWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Backhand/1.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Backhand/2.mpg");
            tutorialWindow.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Backhand/3.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Backhand/4.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Backhand/5.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Backhand/6.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Backhand/7.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Backhand/8.flv");
            tutorialWindow.Show();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Backhand/9.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Doubel/1.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Doubel/2.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Doubel/3.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Footwork/1.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Footwork/2.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Footwork/3.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Footwork/4.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Footwork/5.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Footwork/6.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Forehand/1.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Forehand/2.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_21(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Forehand/3.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_22(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Forehand/4.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_23(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Forehand/5.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_24(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Forehand/6.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_25(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Forehand/7.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_26(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Forehand/8.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_27(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Forehand/9.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_28(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Warm Up/1.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_29(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Warm Up/2.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_30(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Warm Up/3.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_31(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Training/1.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_32(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Training/2.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_33(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Training/3.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_34(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Training/4.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_35(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Training/5.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_36(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Training/6.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_37(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Training/7.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_38(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Training/8.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_39(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Training/9.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_40(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Serve/1.flv");
            tutorialWindow.Show();
        }

        private void Button_Click_41(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Serve/2.flv");
            tutorialWindow.Show();
        }

        private void Button_Click_42(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Serve/3.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_43(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Serve/4.flv");
            tutorialWindow.Show();
        }

        private void Button_Click_44(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Serve/5.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_45(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Serve/6.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_46(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Serve/7.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_47(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Serve/8.flv");
            tutorialWindow.Show();
        }

        private void Button_Click_48(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Serve/9.flv");
            tutorialWindow.Show();
        }

        private void Button_Click_49(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Power/1.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_50(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Power/2.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_51(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Power/3.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_52(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/Power/4.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_53(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/How to Grip the Raquet and Use Wrist/1.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_54(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/How to Grip the Raquet and Use Wrist/2.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_55(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/How to Grip the Raquet and Use Wrist/3.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_56(object sender, RoutedEventArgs e)
        {
            tutorialWindow = new PlayTutorialWindow("Tutorials/How to Grip the Raquet and Use Wrist/4.mp4");
            tutorialWindow.Show();
        }

        private void Button_Click_57(object sender, RoutedEventArgs e)
        {
            string x = Directory.GetCurrentDirectory() + "/Tutorials/Manual.pdf";
            System.Diagnostics.Process.Start(x);
        }
    }
}

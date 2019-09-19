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
using Microsoft.Win32;
using System.Timers;
using System.Windows.Threading;
namespace AudioP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> PlayList = new List<string>();
        public List<string> NameList = new List<string>();
        string VolLang;
        WMPLib.WindowsMediaPlayer soundPlayer = new WMPLib.WindowsMediaPlayer();
        int time;
        int pos;
        SaveSystem save = new SaveSystem();
        bool ok;
        private DispatcherTimer timer = new DispatcherTimer();
        private int x;
        public MainWindow()
        {
            InitializeComponent();
            mediaElement.Source = new Uri(@"media/images.jpg", UriKind.Relative);
            mediaElement.Height = 200;
            mediaElement.Width = 320;
            mediaElement.LoadedBehavior = MediaState.Manual;
            mediaElement.Play();
            slider.Value = 5;
            if (System.IO.File.Exists("Music.xml"))
            {
                save.LoadParams(PlayList, NameList);
                for (int i = 0; i < NameList.Count; i++)
                    comboBox.Items.Add(NameList[i]);
            }

        }
        private void timerStart()
        {
         
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            try
            {
                if (pos != (int)soundPlayer.currentMedia.duration)
                {
                    pos = (int)soundPlayer.controls.currentPosition;
                    time = pos;
                    int h = time / 3600;
                    int m = (time - (h * 3600)) / 60;
                    time = time - (h * 3600 + m * 60);
                    label1.Content = String.Format("{0:D}:{1:D2}:{2:D2}", h, m, time);
                }
            }
            catch(Exception)
            { }
            
        }


        private void button_Click_1(object sender, RoutedEventArgs e)
        { try
            {
                for (int i = 0; i < PlayList.Count; i++)
                {
                    if (comboBox.SelectedIndex == i)
                        soundPlayer.URL = PlayList[i];
                }
                soundPlayer.controls.play();
                ok = true;
                footage(ok);
                timerStart();
                timeTrac();
            }
            catch(Exception)
            {

            }
           
               /*/ soundPlayer.URL = PlayList[0];
               
                soundPlayer.controls.play();
                ok = true;
                footage(ok);
                timerStart();
                timeTrac();
            /*/
            
        }
        void footage(bool ok)
        {   if (ok == true)
            {
                mediaElement.Source = new Uri(@"media/Динамический эффект для музыки Цветомузыка Футаж дискотечный.mp4", UriKind.Relative);
                mediaElement.Height = 180;
                mediaElement.Width = 326;
                mediaElement.LoadedBehavior = MediaState.Manual;
                mediaElement.Play();
            }
            else
            {
                mediaElement.Pause();
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "audio files (*.mp3)|*mp3|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
           
            if (openFileDialog1.ShowDialog() == true)
            {
                PlayList.Add(openFileDialog1.FileName);

                NameList.Add(System.IO.Path.GetFileName(PlayList[PlayList.Count - 1]));
                save.SaveParams(PlayList, NameList);

            }
            comboBox.Items.Add(NameList[PlayList.Count - 1]);

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            soundPlayer.controls.stop();
            ok = false;
            footage(ok);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            int i;
            try
            {
                i = comboBox.SelectedIndex;
                i = i - 1;
                soundPlayer.URL = PlayList[i];
                comboBox.SelectedIndex = i;
                soundPlayer.controls.play();
                ok = true;
                footage(ok);
                timerStart();
                timeTrac();
            }
            catch (Exception)
            {
                i = 0;
                ok = true;
                footage(ok);
                timerStart();
                timeTrac();
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            int i;
            try
            {
                i = comboBox.SelectedIndex;
                i = i + 1;
                soundPlayer.URL = PlayList[i];
                comboBox.SelectedIndex = i;
                soundPlayer.controls.play();
                timeTrac();
                timerStart();
                ok = true;
                footage(ok);
            }
            catch (Exception)
            {
                i = PlayList.Count - 1;
                ok = true;
                footage(ok);
                timerStart();
                timeTrac();
            }
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int volume;
            soundPlayer.settings.volume = (int)slider.Value * 10;
            volume = (int)slider.Value * 10;
            //volume = volume / 10;
            label.Content = "Volume "+VolLang + volume.ToString() + "%";
        }
        public void timeTrac()
        {   try
            {
                time = (int)soundPlayer.currentMedia.duration;
                int h = time / 3600;
                int m = (time - (h * 3600)) / 60;
                time = time - (h * 3600 + m * 60);
                label2.Content = String.Format("{0:D}:{1:D2}:{2:D2}", h, m, time);

                pos = (int)soundPlayer.controls.currentPosition;
                time = pos;
                h = time / 3600;
                m = (time - (h * 3600)) / 60;
                time = time - (h * 3600 + m * 60);
                label1.Content = String.Format("{0:D}:{1:D2}:{2:D2}", h, m, time);
            }
            catch(Exception)
            {

            }
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            /*/pos = (int)soundPlayer.controls.currentPosition;
            pos = pos * 50 / 2;
            soundPlayer.controls.currentPosition = slider1.Value;
            timeTrac();/*/
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            pos = (int)soundPlayer.controls.currentPosition;
            pos = pos - 10;
            soundPlayer.controls.currentPosition = pos;
            timeTrac();
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            pos = (int)soundPlayer.controls.currentPosition;
            pos = pos + 10;
            soundPlayer.controls.currentPosition = pos;
            timeTrac();
        }
    }
}

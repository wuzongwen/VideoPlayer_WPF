using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

namespace VideoPlayer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //获取配置
        Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public MainWindow()
        {
            InitializeComponent();
            //全屏
            if (config.AppSettings.Settings["allScreen"].Value == "true")
            {
                // 设置全屏    
                this.WindowState = System.Windows.WindowState.Normal;
                this.WindowStyle = System.Windows.WindowStyle.None;
                this.ResizeMode = System.Windows.ResizeMode.NoResize;
                this.Topmost = true;
                this.Left = 0.0;
                this.Top = 0.0;
                this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
                this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
                this.Video.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
                this.Video.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            }
            else
            {
                try
                {
                    this.Left = 0.0;
                    this.Top = 0.0;
                    this.Width = Double.Parse(config.AppSettings.Settings["width"].Value);
                    this.Height = Double.Parse(config.AppSettings.Settings["height"].Value);
                    this.Video.Width = Double.Parse(config.AppSettings.Settings["width"].Value);
                    this.Video.Height = Double.Parse(config.AppSettings.Settings["height"].Value);
                }
                catch
                {
                    MessageBox.Show("播放器配置错误，请检查配置");
                }
            }

            MediaElementControl();
            //循环播放
            this.Video.MediaEnded += new RoutedEventHandler(MediaElementControl);

        }

        //播放
        private void MediaElementControl()
        {
            this.Video.LoadedBehavior = MediaState.Manual;
            //Uri uri = new Uri(config.AppSettings.Settings["url"].Value);
            Uri uri = new Uri(AppDomain.CurrentDomain.BaseDirectory + "/video.mp4");
            this.Video.Source = uri;
            this.Video.Play();
        }

        //循环播放
        private void MediaElementControl(object sender, RoutedEventArgs e)
        {
            this.Video.LoadedBehavior = MediaState.Manual;
            Uri uri = new Uri(AppDomain.CurrentDomain.BaseDirectory + "/video.mp4");
            this.Video.Source = uri;
            this.Video.Play();
        }
    }
}

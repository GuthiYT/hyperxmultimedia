using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;

namespace WBR
{
    /// <summary>
    /// </summary>
    public partial class MainWindow : Window
    {

        public Main main;
        private NotifyIcon trayIcon;
        private WindowState storedWindowState = WindowState.Normal;
        public static Config config = new Config();
        public MainWindow()
        {
            InitializeComponent();

            main = new Main();
            Start();
            SetStartup();
        }
        private void TrayIconClick(object sender, EventArgs e)
        {
            Show();
            WindowState = storedWindowState;
            Activate();
        }
        protected override void OnClosed(EventArgs e)
        {
            trayIcon.Visible = true;
            base.OnClosed(e);
            Process.GetCurrentProcess().Kill();
        }
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized && config.ShouldHideInTray) this.Hide();

            //base.OnStateChanged(e);
        }
        private void SetStartup()
        {


            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

           // if (!rk.GetValueNames().Contains("WBR"))
            {
                rk.SetValue("WBR", Environment.CurrentDirectory + "\\WBR.exe");
            }

            rk = Registry.LocalMachine.OpenSubKey
            ("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            // if (!rk.GetValueNames().Contains("WBR"))
            {
                rk.SetValue("WBR", Environment.CurrentDirectory + "\\WBR.exe");
            }

            rk = Registry.LocalMachine.OpenSubKey
            ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\run", true);

            // if (!rk.GetValueNames().Contains("WBR"))
            {
                rk.SetValue("WBR", Environment.CurrentDirectory + "\\WBR.exe");
            }


        }
        
        private void Start(object sender, RoutedEventArgs e)
        {
            Start();
        }
        private void Start()
        {
            SetupTray();

            config.LoadConfig();
            ApplyConfig();
            Apply();

            Active.Text = main.Started.ToString();
        }

        private void SetupTray()
        {
            trayIcon = new System.Windows.Forms.NotifyIcon();

            trayIcon.BalloonTipText = "";
            trayIcon.BalloonTipTitle = "WBR";
            trayIcon.Text = "WBR";
            trayIcon.Icon = new System.Drawing.Icon(FileHandler.EnvironmentPath + "icon.ico");

            trayIcon.Click += new EventHandler(TrayIconClick);
            storedWindowState = WindowState;
        }
        private void Stop(object sender, RoutedEventArgs e)
        {
            Stop();
        }
        private void Stop()
        {
            main.Stop();

            Active.Text = main.Started.ToString();
        }
        private void Apply(object sender, RoutedEventArgs e)
        {
            Apply();
        }

        private void ApplyConfig() 
        { 
            Vid.Text = config.VendorID.ToString("X");
            Pid.Text = config.ProductID.ToString("X");
            Interval.Text = config.Interval.ToString();
            Keycode1.Text = config.Keycode1.ToString("X");
            Keycode2.Text = config.Keycode2.ToString("X");
            Keycode3.Text = config.Keycode3.ToString("X");
            var items = DeviceName.Items;
            int i;
            for(i = 0; i < items.Count; i++)
            {
                DeviceName.SelectedIndex = i;
                if(GetDeviceName() == config.Device)
                    break;
            }
            DeviceName.SelectedIndex = i;
            HideTray.IsChecked = config.ShouldHideInTray;
            Active.Text = main.Started.ToString();
        }

        private void Apply()
        {

            main.Stop();
            if (!main.Started)
            {
                main.Stop();
            }
            

            try
            {
                int vid = ParseHexStringToInt(Vid.Text);
                int pid = ParseHexStringToInt(Pid.Text);
                int devices =  main.Start(GetDeviceName(), vid, pid);
                config.VendorID = vid;
                config.ProductID = pid;

                DeviceAmount.Text = devices.ToString();
            }
            catch (Exception ex) {}
            try
            {
                MediaHandler.PLAY_PAUSE = ParseHexStringToByte(Keycode1.Text);
                config.Keycode1 = MediaHandler.PLAY_PAUSE;
            }
            catch (Exception ex) { }
            try
            {
                MediaHandler.NEXT = ParseHexStringToByte(Keycode2.Text);
                config.Keycode2 = MediaHandler.NEXT;
            }
            catch (Exception ex) { }
            try
            {
                MediaHandler.PREV = ParseHexStringToByte(Keycode3.Text);
                config.Keycode3 = MediaHandler.PREV;
            } catch (Exception ex) { }

            try
            {
                ClickHandler.ClickInterval = ParseStringToInt(Interval.Text);
                config.Interval = ClickHandler.ClickInterval;
            }
            catch (Exception ex) { }

            try
            {
                //MediaHandler.VOLUME_AMOUNT = ParseStringToInt(VolumeStep.Text);
            }
            catch (Exception ex) { }

            try
            {
                bool? b = HideTray.IsChecked;
                if(b != null)
                {
                    config.ShouldHideInTray = (bool)b;
                }
            }
            catch (Exception ex) { }

            config.VendorID = int.Parse(Vid.Text, System.Globalization.NumberStyles.HexNumber);
            config.ProductID = int.Parse(Pid.Text, System.Globalization.NumberStyles.HexNumber);
            config.Device = GetDeviceName();
            config.SaveConfig();
            

            /*

            Vid.Text = config.VendorID.ToString("X");
            Pid.Text = config.ProductID.ToString("X");
            Interval.Text = config.Interval.ToString();
            Keycode1.Text = config.Keycode1.ToString("X");
            Keycode2.Text = config.Keycode2.ToString("X");
            Keycode3.Text = config.Keycode3.ToString("X");
            DeviceName.SelectedIndex = DeviceName.Items.IndexOf(config.Device);
            HideTray.IsChecked = config.ShouldHideInTray;

            */

            Active.Text = main.Started.ToString();
        }
        private int ParseStringToInt(string text)
        {
            int result = int.Parse(text);
            return result;
        }
        private int ParseHexStringToInt(string text)
        {
            int result = int.Parse(text, System.Globalization.NumberStyles.HexNumber);
            return result;
        }
        private bool ParseStringToBool(string text)
        {
            return bool.Parse(text);
        }
        private byte ParseHexStringToByte(string text)
        {
            byte result = Convert.ToByte(text, 16);
            return result;
        }

        public static Window GetWindow()
        {
            return Window.GetWindow(App.Current.MainWindow) as Window;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private string GetDeviceName()
        {
            return DeviceName.SelectedValue.ToString();
        }
    }
}

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
using static System.Net.Mime.MediaTypeNames;

namespace WBR
{
    public partial class MainWindow : Window
    {

        public Main Main;
        private NotifyIcon TrayIcon;
        private WindowState StoredWindowState = WindowState.Normal;
        public static Config Config = new Config();
        public MainWindow()
        {
            InitializeComponent();
            Main = new Main();
            Start();
            SetStartup();
        }

        // Start
        private void Start(object sender, RoutedEventArgs e)
        {
            Start();
        }
        private void Start()
        {
            SetupTray();

            Config.LoadConfig();
            ApplyConfig();
            Apply();

            Active.Text = Main.Started.ToString();
        }


        // Apply
        private void Apply(object sender, RoutedEventArgs e)
        {
            Apply();
        }


        private void Apply()
        {

            Main.Stop();
            if (!Main.Started)
            {
                Main.Stop();
            }

            int devices = Main.Start(GetDeviceName(), Config.VendorID, Config.ProductID);
            DeviceAmount.Text = devices.ToString();

            MediaHandler.PLAY_PAUSE = ErrorHandler.Try(ParseHexStringToByte, Keycode1.Text);
            MediaHandler.NEXT = ErrorHandler.Try(ParseHexStringToByte, Keycode2.Text);
            MediaHandler.PREV = ErrorHandler.Try(ParseHexStringToByte, Keycode3.Text);
            ClickHandler.ClickInterval = ErrorHandler.Try(ParseStringToInt, Interval.Text); 

            Config.Keycode1 = MediaHandler.PLAY_PAUSE;
            Config.Keycode2 = MediaHandler.NEXT;
            Config.Keycode3 = MediaHandler.PREV;
            Config.Interval = ClickHandler.ClickInterval;

            if (HideTray.IsChecked != null)
                Config.ShouldHideInTray = (bool)HideTray.IsChecked;

            TrayIcon.Visible = Config.ShouldHideInTray;

            Config.VendorID = ErrorHandler.Try(ParseHexStringToInt, Vid.Text);
            Config.ProductID = ErrorHandler.Try(ParseHexStringToInt, Pid.Text);
            Config.Device = GetDeviceName();
            Config.SaveConfig();

            Active.Text = Main.Started.ToString();
        }
        private void ApplyConfig()
        {
            Vid.Text = Config.VendorID.ToString("X");
            Pid.Text = Config.ProductID.ToString("X");
            Interval.Text = Config.Interval.ToString();
            Keycode1.Text = Config.Keycode1.ToString("X");
            Keycode2.Text = Config.Keycode2.ToString("X");
            Keycode3.Text = Config.Keycode3.ToString("X");
            var items = DeviceName.Items;
            int i;
            for (i = 0; i < items.Count; i++)
            {
                DeviceName.SelectedIndex = i;
                if (GetDeviceName() == Config.Device)
                    break;
            }
            DeviceName.SelectedIndex = i;
            HideTray.IsChecked = Config.ShouldHideInTray;
            Active.Text = Main.Started.ToString();
        }



        private void TrayIconClick(object sender, EventArgs e)
        {
            Show();
            WindowState = StoredWindowState;
            Activate();
        }
        protected override void OnClosed(EventArgs e)
        {
            TrayIcon.Visible = false;
            base.OnClosed(e);
            Process.GetCurrentProcess().Kill();
        }
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized && Config.ShouldHideInTray) this.Hide();

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
        


        private void SetupTray()
        {
            TrayIcon = new NotifyIcon();

            TrayIcon.BalloonTipText = "";
            TrayIcon.BalloonTipTitle = "WBR";
            TrayIcon.Visible = true;
            TrayIcon.Text = "WBR";
            TrayIcon.Icon = new System.Drawing.Icon(FileHandler.EnvironmentPath + "icon.ico");

            TrayIcon.Click += new EventHandler(TrayIconClick);
            StoredWindowState = WindowState;
        }
        private void Stop(object sender, RoutedEventArgs e)
        {
            Stop();
        }
        private void Stop()
        {
            Main.Stop();

            Active.Text = Main.Started.ToString();
        }

        private int ParseStringToInt(string text)
        {
            return ErrorHandler.Try(int.Parse, text);

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

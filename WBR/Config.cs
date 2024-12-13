using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;

namespace WBR
{
    /// <summary>
    /// Saves/reads all needed config variables and converts them to one single string
    /// Not a very good way, but good enough for now
    /// </summary>
    public class Config
    {
        public string FileName = "config.json";
        public int VendorID  { get; set; }//= 1008; //
        public int ProductID  { get; set; }//= 1686;
        public int Interval  { get; set; }//= 500; //
        public byte Keycode1  { get; set; }//= 179; //
        public byte Keycode2  { get; set; }//= 176; //
        public byte Keycode3  { get; set; }//= 177; //
        public bool ShouldHideInTray  { get; set; }//= false;//
        public string Device  { get; set; } //= "HyperX Cloud II Wireless(DTS)";/

        /// <summary>
        /// Saves the current config as a file
        /// </summary>
        public void SaveConfig()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(this, options);

            FileHandler.WriteToAppData(FileName, jsonString);
        }

        /// <summary>
        /// Reads text from config.yml File and imports it as variables to the config class
        /// </summary>
        /// <returns></returns>
        /// 

        private void SetConfigDefaults()
        {
            // Config defualts
            VendorID = 1008;
            ProductID = 1686;
            Device = "HyperX Cloud II Wireless (DTS)";
            Keycode1 = 179;
            Keycode2 = 176;
            Keycode3 = 177;
            ShouldHideInTray = false;
            Interval = 500;
        }

        public void LoadConfig()
        {
            try
            {
                string json = FileHandler.ReadFromAppData(FileName);
                Config config =
                    JsonSerializer.Deserialize<Config>(json);
                SetConfig(config);
            }
            catch(Exception e)
            {
                ErrorHandler.NewError(e);
                SetConfigDefaults();
                SaveConfig();
            }

        }

        public void SetConfig(Config config)
        {
            VendorID = config.VendorID;
            ProductID = config.ProductID;
            Interval = config.Interval;
            Keycode1 = config.Keycode1;
            Keycode2 = config.Keycode2;
            Keycode3 = config.Keycode3;
            ShouldHideInTray = config.ShouldHideInTray;
            Device = config.Device;
        }
    }
}

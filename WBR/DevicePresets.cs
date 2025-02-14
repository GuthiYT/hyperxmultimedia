using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBR
{
    internal class DevicePresets
    {
        private static readonly Dictionary<string, DevicePresets> Presets = new Dictionary<string, DevicePresets>() {
            {   "HyperX Cloud II Wireless (DTS)",
                new DevicePresets(new byte[,] {
                    { 255, 187, 32, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 255, 187, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
                    { 255, 187, 32, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 127, 0, 0 }, 
                    { 255, 187, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 127, 0, 0 }, 
                })
            },
            {   "Corsair Virtuoso XT",
                new DevicePresets(new byte[,] {
                    { 1, 1, 142, 0, 0, 0, 0, 0, 197, 107, 181,
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                    { 1, 1, 142, 0, 1, 0, 0, 0, 197, 107, 181,
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                })
            },
            {   "HyperX Cloud III Wireless",
                new DevicePresets(new byte[,] {
                    { 10,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                    { 10,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                })
            },
            {   "HyperX Cloud Alpha",
                new DevicePresets(new byte[,] {
                    { 187,35,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                    { 187,35,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                })
            },

            /* adding a new device
            {   "PLACEHOLDER",
                new DevicePresets(new byte[,] {
                    { 0, 0, 0, 0},
                    { 0, 0, 0, 0 },
                    { 0, 0, 0, 0 },
                    { 0, 0, 0, 0 },
                })
            },
            */


        };

        public static bool Contains(string device, byte[] bytes)
        {
            if (!Presets.ContainsKey(device))
            {
                ErrorHandler.NewError($"Preset {device} couldn't be found!");
                return false;
            }


            ref byte[,] byteArray = ref Presets[device].Bytes;

            // amount of possible byte arrays
            int l1 = byteArray.GetLength(0);
            // byte array length / fixed packet size
            int l2 = byteArray.GetLength(1);

            // skip comparison if length not equal
            if (l2 != bytes.Length)
                return false;

            for (int i = 0; i < l1; i++)
            {
                bool same = true;
                for (int j = 0; j < l2; j++)
                {
                    if (bytes[j] != byteArray[i, j])
                        same = false;
                }
                if (same)
                    return true;
            }

            return false;
        }

        private DevicePresets(byte[,] bytes)
        {
            Bytes = bytes;
        }

        private byte[,] Bytes;
    }


}

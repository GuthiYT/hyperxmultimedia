using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBR
{
    internal class DevicePresets
    {
        private static Dictionary<string, DevicePresets> Presets = new Dictionary<string, DevicePresets>() {
            {   "HyperX Cloud II Wireless (DTS)",
                new DevicePresets(new byte[,] {
                    { 255, 187, 32, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 255, 187, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
                    { 255, 187, 32, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 127, 0, 0 }, 
                    { 255, 187, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 127, 0, 0 }, 
                })
            },
            //1 1 142 0 0 0 0 0 197 107 181 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
            {   "Corsair Virtuoso XT",
                new DevicePresets(new byte[,] {
                    { 1, 1, 142, 0, 0, 0, 0, 0, 197, 107, 181,
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                    { 1, 1, 142, 0, 1, 0, 0, 0, 197, 107, 181,
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
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
            if(Presets.ContainsKey(device))
            {
                ref byte[,] byteArray = ref Presets[device].Bytes;

                int l1 = byteArray.GetLength(0);
                int l2 = byteArray.GetLength(1);


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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBR
{
    /// <summary>
    /// Handles the start and stop process
    /// </summary>
    public class Main
    {
        public bool Started { get; private set; } = false;
        public Device Device { get; private set; }
        private int Devices = 0;

        public Main(){}

        public int Start(string deviceName, int vid, int pid)
        {
            if (Started) return Devices;

            Device = new Device(deviceName, vid, pid);
            Devices = Device.Init();

            Started = true;

            return Devices;
        }
        public void Stop()
        {
            if(!Started) return;

            Device.Stop();
            Device = null;

            Started = false;
        }

    }
}

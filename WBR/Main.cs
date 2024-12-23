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

        public Main(){}

        public void Start(string deviceName, int vid, int pid)
        {
            Device = new Device(deviceName, vid, pid);
            Device.Init();

            Started = true;
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

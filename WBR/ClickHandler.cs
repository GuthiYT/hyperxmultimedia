using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WBR
{
    public static class ClickHandler
    {
        public static int ClickInterval = 500; // Time between clicks in which we detect the users input

        private static Stopwatch Clock; // tracks time between clicks
        private static int ConsecutiveClicks = 0; // tracks number of consecutive clicks in one interval

        /// <summary>
        /// Handles the click event and calls diferent actions depending on how many times
        /// it was clicked in a row
        /// </summary>
        public static void HandleClick()
        {
            // Starts Clock at the very first click or if interval elapsed
            if (Clock == null || Clock.ElapsedMilliseconds > ClickInterval)
            {
                ConsecutiveClicks = 0;

                Clock = Stopwatch.StartNew();
            }
            ConsecutiveClicks++;
         
            ClickAction(ConsecutiveClicks);
        }

        /// <summary>
        /// Checks if clicked then does the action associated with clicking once, then either
        /// 1. User presses 3 times (max) or
        /// 2. User presses 2 times and interval elapsed
        /// </summary>
        /// <param name="clicks"></param>
        private static void ClickAction(int clicks)
        {
            if(clicks == 1)
            {
                MediaHandler.PauseResumeAudio();
            } else if(clicks == 2)
            {
                WaitForIntervalToFinish();
            }
            else if (clicks == 3)
            {
                MediaHandler.PrevAudio();
                ConsecutiveClicks = 0;
            }
        }

        /// <summary>
        /// Starts a new Thread to handle the waiting process until the interval has elapsed
        /// </summary>
        private static void WaitForIntervalToFinish()
        {
            // Start a new seperate thread
            new Thread(() =>
            {
                // "Wait" until interval elapsed
                while (Clock.ElapsedMilliseconds < ClickInterval)
                {

                }
                // Check if in the elapsed time frame no other click occurred
                if (ConsecutiveClicks == 2)
                {
                    MediaHandler.NextAudio();
                    ConsecutiveClicks = 0;
                }
                return;
            }).Start();
        }
    }
}


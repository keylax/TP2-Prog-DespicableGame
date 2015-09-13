using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DespicableGame.Observer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace DespicableGame
{
    public class Countdown : Subject
    {
        private TimeSpan countDown;
        private NotifyReason reasonOfCountdown;
        public Countdown(int hours, int minutes, int seconds, NotifyReason reason)
        {
            countDown = new TimeSpan(hours, minutes, seconds);
            reasonOfCountdown = reason;
        }
        public TimeSpan CountDown
        {
            get
            {
                return countDown;
            }
            set
            {
                countDown = value;

                if (countDown.TotalMilliseconds <= 0)
                {
                    NotifyAllObservers(reasonOfCountdown);
                }
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.Observer
{
    public class Subject
    {
        public enum NotifyReason { MONEY_GAINED, MONEY_DESTROYED, EXIT_REACHED, LIFE_LOST, TRAP_ACTIVATED}

        protected List<Observer> observers;

        public Subject()
        {
            observers = new List<Observer>();
        }

        public void AddObserver(Observer obs)
        {
            observers.Add(obs);
        }

        public void RemoveObserver(Observer obs)
        {
            if (observers.Contains(obs))
            {
                observers.Remove(obs);
            }
        }

        protected void NotifyAllObservers(NotifyReason reason)
        {
            foreach (Observer obs in observers)
            {
                obs.Notify(this, reason);
            }
        }

    }
}
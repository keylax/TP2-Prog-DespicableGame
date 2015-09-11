using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.Observer
{
    public class Subject
    {
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

        protected void NotifyAllObserver()
        {
            foreach (Observer obs in observers)
            {
                obs.Notify(this);
            }
        }

    }
}
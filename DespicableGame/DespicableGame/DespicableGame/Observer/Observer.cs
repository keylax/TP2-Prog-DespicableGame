using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.Observer
{
    public interface Observer
    {
        void Notify(Subject subject, Subject.NotifyReason reason);
    }
}
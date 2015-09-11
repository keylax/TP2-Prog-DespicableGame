using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame
{
    public interface ICommand
    {
        void Execute(Gamepad gp);
    }
}
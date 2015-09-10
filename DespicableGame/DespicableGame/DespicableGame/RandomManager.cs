using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame
{
    static class RandomManager
    {
        static Random randomizer = new Random();

        public static int GetRandomInt(int minimum, int maximum)
        {
            return randomizer.Next(minimum, maximum + 1);
        }



    }
}
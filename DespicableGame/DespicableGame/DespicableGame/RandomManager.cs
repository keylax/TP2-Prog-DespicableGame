using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    static class RandomManager
    {
        static Random randomizer = new Random();

        public static int GetRandomInt(int minimum, int maximum)
        {
            return randomizer.Next(minimum, maximum + 1);
        }

        public static Vector2 GetRandomVector(int maximumX, int maximumY)
        {
            return new Vector2(GetRandomInt(0, maximumX - 1), GetRandomInt(0, maximumY - 1));
        }

    }
}
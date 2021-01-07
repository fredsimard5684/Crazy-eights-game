using System;

namespace inf1035_crazy_eights.Helper
{

    /*
    * Summary: this class will store custom math functions
    */
    internal static class MathHelper
    {
        /*
        * Summary: A modulo function that will return the correct positive reminder, even if the numerator is negative
        * param name="a": the numerator
        * param name="b": the denominator
        * return: the positive reminder
        */
        public static int Modulo(int a, int b)
        {
            int reminder = (Math.Abs(a * b) + a) % b;
            return reminder;
        }
    }
}
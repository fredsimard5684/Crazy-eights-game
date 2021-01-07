using System;

namespace inf1035_crazy_eights.Helper
{
    public class ArrayHelper
    {
        public static void Populate<T>(T[,] arr, T value)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = value;
                }
            }
        }

        public static void Merge<T>(T[,] arr1, T[,] arr2, Func<T, T, bool> fn)
        {
            for (int i = 0; i < arr1.GetLength(0); i++)
            {
                for (int j = 0; j < arr1.GetLength(1); j++)
                {
                    bool result = fn(arr1[i, j], arr2[i, j]);
                    if (result) arr1[i, j] = arr2[i, j];
                }
            }
        }
    }
}

using System;
using System.Collections;
using System.Threading;

namespace inf1035_crazy_eights.Board
{
    /*
     * This class allow to print stuff more easily to the console by predefining the console behavior
     */
    public static class ConsoleHelper
    {
        //TODO: NOT IN USE
        public static void PrintArray(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine("{0} - {1}", i, arr[i]);
            }
        }

        public static void PrintList(ArrayList list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("{0} - {1}", i + 1, list[i]);
            }
        }

        //Print a string
        public static void PrintString(string str)
        {
            Console.WriteLine(str);
        }

        /*
        * Summary: Write to the console and stop the program for the desire time
        * param name="phrase": The phrase you want to display
        * param name="sleep": the time to stop the program
        */
        public static void PrintString(string phrase, int sleep)
        {
            Console.WriteLine(phrase);
            Thread.Sleep(sleep);
        }

        /*
        * Summary: Ask a string
        * param name="question": The question that needs to be asked
        */
        public static string AskSring(string question)
        {
            string str = "";
            int valid = 0;

            while (valid == 0)
            {
                Console.Write(question);
                str = Console.ReadLine();

                if (str.Length > 1)
                {
                    valid = 1;
                }
                else
                {
                    Console.WriteLine(question);
                }
            }

            return str;
        }

        /*
        * Summary: ask for a number. This function checks if an integer has been enter correctly in the console
        * param name="question": the question that needs to be asked
        * param name="min": the minimum value
        * param name="max": the maximum value
        * return: the value that the user chose
        */
        public static int AskInteger(string question, int min, int max)
        {
            int valid = 0;
            int value = -1;

            while (valid == 0)
            {
                try
                {
                    Console.Write(question);
                    var val = Console.ReadLine();
                    value = Convert.ToInt32(val);
                    if (value <= max && value >= min)
                    {
                        valid = 1;
                    }
                    else
                    {
                        Console.WriteLine("Please, provide a valid number");
                    }
                }
                catch (System.Exception)
                {
                    Console.WriteLine("Please, provide a valid number");
                }
            }

            return value;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace HalloDebugger
{

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hallo Debugger");


            Trace.Listeners.Add(new EventLogTraceListener("Application"));
            MyTrace.Log("Test #1");

            List<byte[]> zeug = new List<byte[]>();

            for (int i = 0; i < 10000; i++)
            {

                ZeigeZahl(i);

                zeug.Add(new byte[100000]);
                ZeigeZahl(GetWichtigeNummer());


                //Console.ReadKey();
                Trace.WriteLine($"TRACE {i}", "LALA");
                Debug.WriteLine($"DEBUG {i}");
                MyTrace.Log($"My Trace {i}");
            }

#if DEBUG
            Console.WriteLine("DEBUG VERSION");
#endif

#if WURST
            Console.WriteLine("WURST VERSION");
#endif

            Console.ReadLine();
        }

        private static void ZeigeZahl(int i)
        {
            Console.WriteLine($"{i:00}");
            Console.WriteLine("--");
        }

        public static int GetWichtigeNummer()
        {
            return CalcBla();
        }

        private static int CalcBla()
        {
            Console.ReadKey();
            
            return DateTime.Now.Second * 64;
        }
    }

    internal class MyTrace
    {
        internal static void Log(string txt, [CallerMemberName]string cmn = "")
        {
            Trace.WriteLine($"{DateTime.Now:d} {DateTime.Now:t} {txt} ({cmn})");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace HalloTPL
{
    delegate void EinfacherDelete();
    delegate void DeleMitPara(string teowuhfowejnfoewfofwemxt);
    delegate long CalcDelegate(int a, int b);

    class HalloDelegate
    {
        public HalloDelegate()
        {

            EinfacherDelete meinDele = EinfacheMethode;
            Action meinDeleAlsAction = EinfacheMethode;
            Action meinDeleAno = delegate () { Console.WriteLine("lala"); };
            Action meinDeleAno2 = () => { Console.WriteLine("lala"); };
            Action meinDeleAno3 = () => Console.WriteLine("lala");

            DeleMitPara deleMitPara = MethodeMitPara;
            Action<string> deleMitParaAlsAction = MethodeMitPara;
            Action<string> deleMitParaAlsActionAno = (string text) => { Console.WriteLine(text); };
            Action<string> deleMitParaAlsActionAno2 = (text) => Console.WriteLine(text); 
            Action<string> deleMitParaAlsActionAno3 = x => Console.WriteLine(x); 

            CalcDelegate calc = Sum;
            Func<int, int, long> calcAlsFunc = Sum;
            Func<int, int, long> calcAlsFuncAno = (x, y) => { return x + y; };
            Func<int, int, long> calcAlsFuncAno2 = (x, y) =>  x + y;

            List<string> texte = new List<string>();
            texte.Where(x => x.StartsWith("b"));
            texte.Where(Filter);
        }

        private bool Filter(string arg)
        {
            if (arg.StartsWith("b"))
                return true;
            else
                return false;
        }

        private long Sum(int a, int b)
        {
            return a + b;
        }

        private void MethodeMitPara(string text)
        {
            Console.WriteLine(text);
        }

        private void EinfacheMethode()
        {
            Console.WriteLine("Hallo");
        }
    }
}

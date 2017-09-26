using System;
using eKoodi.Utilities.Test;

namespace ski_jumping_points_calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            for (int i = 1; i < 4; i++)
            {
                var nimi = TestUtility.SkiJumpingHills(i.ToString()).Item1;
                Console.Write("{0}:{1}. ", i, nimi);
            }
            Console.WriteLine("Valitse Mäki antamalla mäen luku:");

            string makiValinta = Console.ReadLine();
            var maki = TestUtility.SkiJumpingHills(makiValinta);


            Console.WriteLine("Anna hyppääjien määrä!");
            string hyppaajienMaara = Console.ReadLine();
            TestUtility.SkiJumpingPointsCalculator(maki.Item1, maki.Item2, maki.Item4, maki.Item3, hyppaajienMaara);

            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace eKoodi.Utilities.Test
{
    public class TestUtility
    {

        static public Tuple<string, string, int, int> SkiJumpingHills(string userInput)
        {
            int makiValinta;
            int.TryParse(userInput, out makiValinta);

            if (makiValinta >= 1 && makiValinta <= 3)
            {
                dynamic d1 = new System.Dynamic.ExpandoObject();
                dynamic d2 = new System.Dynamic.ExpandoObject();
                dynamic d3 = new System.Dynamic.ExpandoObject();
                var dict = new Dictionary<int, dynamic>();
                dict[1] = d1;
                dict[1].maki = new { tyyppi = "Suurmäki", luokka = 1, kpiste = 120, nimi = "Rukatunturi" };

                dict[2] = d2;
                dict[2].maki = new { tyyppi = "Suurmäki", luokka = 1, kpiste = 116, nimi = "Lahti" };

                dict[3] = d3;
                dict[3].maki = new { tyyppi = "Normaalimäki", luokka = 0, kpiste = 90, nimi = "Vuokatti" };

                string tyyppi = dict[makiValinta].maki.tyyppi;
                int luokka = dict[makiValinta].maki.luokka;
                int kPiste = dict[makiValinta].maki.kpiste;
                string nimi = dict[makiValinta].maki.nimi;

                return Tuple.Create(nimi, tyyppi, kPiste, luokka);
            }

            return Tuple.Create("", "", 0, 0);
        }

        static public string SkiJumpingPointsCalculator(string makiNimi, string makiTyyppiNimi, int makiTyyppi, int makiKpiste, string hyppaajienMaaraInput)
        {
            double hypynPituus;
            int kPiste = makiKpiste;


            int hyppaajienMaara;
            int.TryParse(hyppaajienMaaraInput, out hyppaajienMaara);

            double metriKerroin;
            if (makiTyyppi == 0)
            {
                metriKerroin = 2;
            }
            else
            {
                metriKerroin = 1.8;
            }

            Random hypynPituusRand = new Random();
            Random tyyliPiste = new Random();
            Random tuulenNopeus = new Random();

            var tulokset = new Dictionary<int, double> { };
            for (int j = 1; j <= hyppaajienMaara; j++)
            {
                double pisteet = 0;

                hypynPituus = Math.Round(hypynPituusRand.NextDouble() * (144 - 35) + 35, 2);
                Console.WriteLine("\n Kilpailija {0} hyppäsi {1} metriä.", j, hypynPituus);
                Console.WriteLine("_____________________");

                //kpiste
                if (hypynPituus >= kPiste)
                {
                    pisteet += 60;
                    double yliJaama = Math.Round((hypynPituus - kPiste) * metriKerroin, 1);
                    pisteet += yliJaama;
                    Console.WriteLine("K-Piste ylitetty: +60 pistettä. Mäen K-Piste on {0}", kPiste);
                    Console.WriteLine("Hyppy pisteet K-Pisteen ylijääville metreille: +{0} pistettä", yliJaama);
                    Console.WriteLine("Hyppy pisteet yhteensä: {0}", pisteet);
                }
                else
                {
                    double aliJaama = 60 + Math.Round((hypynPituus - kPiste) * metriKerroin, 1);
                    if (aliJaama < 0)
                    {
                        aliJaama = 0;
                    }
                    pisteet += aliJaama;
                    Console.WriteLine("K-Pistettä ei ylitetty: +{0} pistettä. Mäen K-Piste on {1}", aliJaama, kPiste);
                }

                //tyyli + tuuli
                List<double> tyyliPisteet = new List<double> { };
                List<double> tuulenNopeudet = new List<double> { };

                for (int i = 1; i <= 5; i++)
                {
                    double tuomarinPisteet = Math.Round(tyyliPiste.NextDouble(), 1) * (20 - 12) + 12;
                    Console.WriteLine("Tuomari {0}: {1}", i, tuomarinPisteet);
                    tyyliPisteet.Add(tuomarinPisteet);
                    tuulenNopeudet.Add(Math.Round(tuulenNopeus.NextDouble(), 1) * (5 - (-5)) + (-5));
                }
                double maxTuomari = tyyliPisteet.Max();
                double minTuomari = tyyliPisteet.Min();
                double kokonaisTyyli = tyyliPisteet.Sum(item => item) - (maxTuomari + minTuomari);
                pisteet += kokonaisTyyli;
                Console.WriteLine("Tuomari pisteet: +{0}", kokonaisTyyli);

                //tuuli
                double tuulenKeskiarvio = (Math.Ceiling((tuulenNopeudet.Average() * (kPiste - 36) / 20) * 2) / 2) * metriKerroin; //ceil == ceiling?

                if (tuulenKeskiarvio > 0)
                {
                    Console.WriteLine("Tuulen nopeus: {0} myötätuuli. +{1} pistettä", tuulenNopeudet.Average(), tuulenKeskiarvio);
                }
                else
                {
                    Console.WriteLine("Tuulen nopeus: {0} vastatuuli. {1} pistettä", tuulenNopeudet.Average(), tuulenKeskiarvio);
                }
                pisteet += tuulenKeskiarvio;

                // lahtö lava
                double lahtoKorkeus = 1.4;
                double pisteVahennus = lahtoKorkeus * 5 * metriKerroin;

                pisteet -= pisteVahennus;
                Console.WriteLine("Lahtokorkeus: {0}, -{1} pistettä", lahtoKorkeus, pisteVahennus);
                Console.WriteLine("Kokonaispisteet: {0}", pisteet);
                Console.WriteLine("_____________________ \n");

                tulokset.Add(j, pisteet);
            }

            var sortedDict = from entry in tulokset orderby entry.Value descending select entry;
            int sija = 0;
            foreach (KeyValuePair<int, double> pair in sortedDict)
            {
                sija++;
                Console.WriteLine("Sija: {2}, Hyppaaja {0} - Pisteet {1}", pair.Key, pair.Value, sija);
            }
            return "";
        }


    }
}
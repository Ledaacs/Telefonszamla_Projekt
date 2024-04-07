using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelefonszamlaProjekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fajl = "hivasok.txt";
            Telefonszamla t = new Telefonszamla(fajl);

            Console.Write("Adja meg a telefonszámot (pl.: 301234567): ");
            string telefonszam = Console.ReadLine();

            bool mobil = Hivas.IsMobil(telefonszam);
            Console.WriteLine("A telefonszám mobil: {0}", mobil ? "Igen" : "Nem");

            Console.WriteLine("Adja meg a hívás kezdeti időpontját (óra:perc:másodperc):");
            string kezdetiIdopontStr = Console.ReadLine().Replace(" ", ":");
            Console.WriteLine("Adja meg a hívás befejezési időpontját (óra:perc:másodperc):");
            string befejezoIdopontStr = Console.ReadLine().Replace(" ", ":");

            TimeSpan kezdetiIdopont = TimeSpan.Parse(kezdetiIdopontStr);
            TimeSpan befejezoIdopont = TimeSpan.Parse(befejezoIdopontStr);
            Hivas ujHivas = new Hivas(telefonszam, kezdetiIdopont, befejezoIdopont);

            Console.WriteLine(ujHivas.Info());

            t.KiirSzamlaba("szamla.txt");

            Console.ReadLine();

        }
    }
}
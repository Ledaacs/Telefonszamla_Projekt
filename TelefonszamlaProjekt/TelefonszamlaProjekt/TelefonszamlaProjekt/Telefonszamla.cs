using System;
using System.Collections.Generic;
using System.IO;

namespace TelefonszamlaProjekt
{
    internal class Telefonszamla
    {
        private string fajl;
        List<Hivas> hivasok = new List<Hivas>();


        public Telefonszamla(string fajl)
        {
            this.fajl = fajl;
            Beolvas();
        }

        private void Beolvas()
        {
            string[] sorok = File.ReadAllLines(fajl);

            for (int i = 0; i < sorok.Length; i += 2)
            {
                sorok[i] = sorok[i].Replace(' ', ';');

                string[] adatok = sorok[i].Split(';');

                int kora = int.Parse(adatok[0]);
                int kperc = int.Parse(adatok[1]);
                int kmasodperc = int.Parse(adatok[2]);
                TimeSpan kezdetiIdopont = new TimeSpan(kora, kperc, kmasodperc);

                int vora = int.Parse(adatok[3]);
                int vperc = int.Parse(adatok[4]);
                int vmasodperc = int.Parse(adatok[5]);
                TimeSpan vegezoIdopont = new TimeSpan(vora, vperc, vmasodperc);

                string telefonszam = sorok[i + 1];

                hivasok.Add(new Hivas(telefonszam, kezdetiIdopont, vegezoIdopont));
            }
        }

        public void KiirSzamlaba(string szamlafajl)
        {
            using (StreamWriter writer = new StreamWriter(szamlafajl))
            {
                foreach (Hivas hivas in hivasok)
                {
                    writer.WriteLine(hivas.Info());
                }
            }

            Console.WriteLine("A kiírás befejeződött a {0} nevű fájlba.", szamlafajl);
        }
    }
}
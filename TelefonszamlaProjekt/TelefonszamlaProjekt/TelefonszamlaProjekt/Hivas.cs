using System;

namespace TelefonszamlaProjekt
{
    internal class Hivas
    {
        public string Telefonszam;
        public TimeSpan KezdetiIdopont;
        public TimeSpan VegezoIdopont;
        TimeSpan hivasIdotartama;

        public Hivas(string telefonszam, TimeSpan kezdetiIdopont)
        {
            this.Telefonszam = telefonszam;
            this.KezdetiIdopont = kezdetiIdopont;
        }

        public Hivas(string telefonszam, TimeSpan kezdetiIdopont, TimeSpan vegezoIdopont)
        {
            this.Telefonszam = telefonszam;
            this.KezdetiIdopont = kezdetiIdopont;
            this.VegezoIdopont = vegezoIdopont;
        }

        public static bool IsMobil(string telefonszam)
        {
            return telefonszam.StartsWith("39") || telefonszam.StartsWith("41") || telefonszam.StartsWith("71");
        }

        public int Idotartam()
        {
            this.hivasIdotartama = this.VegezoIdopont - this.KezdetiIdopont;
            int szamlazottPercek = (int)Math.Ceiling(this.hivasIdotartama.TotalMinutes);
            return szamlazottPercek;
        } 

        public string IdoKifejezese()
        {
            string kimenet;
            if (Idotartam() >= 60)
            {
                int ora = Idotartam() / 60;
                int perc = Idotartam() % 60;
                kimenet = $"{ora} óra {perc} perc";
            }
            else
            {
                kimenet = $"{Idotartam()} perc";
            }
            return kimenet;
        }

        public double DijMegallapitas()
        {
            double csusidobenDij;
            double csusidonKivulDij;

            if (IsMobil(this.Telefonszam))
            {
                csusidobenDij = 69.175;
                csusidonKivulDij = 46.675;
            }
            else
            {
                csusidobenDij = 30;
                csusidonKivulDij = 15;
            }


            double csusidobenPercek = 0;
            double csusidonKivulPercek = Idotartam();

            if (this.KezdetiIdopont.Hours >= 7 && this.KezdetiIdopont.Hours < 18)
            {
                csusidobenPercek = Idotartam();
                csusidonKivulPercek = 0;
            }

            double osszesenDij = csusidobenPercek * csusidobenDij + csusidonKivulPercek * csusidonKivulDij;
            return osszesenDij;
        }

        public string Info()
        {
            string infoString = string.Format("Telefonszám: {0}\nÖsszesen: {1} Ft\nTelefonált idő: {2}\n", this.Telefonszam, DijMegallapitas(), IdoKifejezese());
            return infoString;
        }
    }
}
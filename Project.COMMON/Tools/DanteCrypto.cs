using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project.COMMON.Tools
{
    public static class DanteCrypto
    {

        public static string CrypHeaven(string hayat)
        {
            //String bir charlar arrayidir
            Random dnt = new Random();
            string[] sinArray = { "limbo","lust","gluttony","greed","anger","heresy","violence","fraud","treachery"};
            string beden = "";
            foreach (char gunah in hayat)
            {
                int gunahSayisi;
                switch (dnt.Next(1, 10))
                {
                    case 1:
                        gunahSayisi = (Convert.ToInt32(gunah) + 1) * 3;
                        beden += $"{gunahSayisi.ToString()}{sinArray[0]}";
                        break;

                    case 2:
                        gunahSayisi = (Convert.ToInt32(gunah) + 2) * 2;
                        beden += $"{gunahSayisi.ToString()}{sinArray[1]}";
                        break;

                    case 3:
                        gunahSayisi = (Convert.ToInt32(gunah) + 3) * 1;
                        beden += $"{gunahSayisi.ToString()}{sinArray[2]}";
                        break;

                    case 4:
                        gunahSayisi = (Convert.ToInt32(gunah) + 4) * 3;
                        beden += $"{gunahSayisi.ToString()}{sinArray[3]}";
                        break;

                    case 5:
                        gunahSayisi = (Convert.ToInt32(gunah) + 5) * 2;
                        beden += $"{gunahSayisi.ToString()}{sinArray[4]}";
                        break;

                    case 6:
                        gunahSayisi = (Convert.ToInt32(gunah) + 6) * 1;
                        beden += $"{gunahSayisi.ToString()}{sinArray[5]}";
                        break;

                    case 7:
                        gunahSayisi = (Convert.ToInt32(gunah) + 7) * 3;
                        beden += $"{gunahSayisi.ToString()}{sinArray[6]}";
                        break;

                    case 8:
                        gunahSayisi = (Convert.ToInt32(gunah) + 8) * 2;
                        beden += $"{gunahSayisi.ToString()}{sinArray[7]}";
                        break;

                    case 9:
                        gunahSayisi = (Convert.ToInt32(gunah) + 9) * 1;
                        beden += $"{gunahSayisi.ToString()}{sinArray[8]}";
                        break;
                   
                }

            }

            return beden;
        }
        public static string CrypHell(string beden)
        {
            //Kullanma Kılvauzu
            //beden Kriptoyu kaldirmek istediğimiz deger
            //ruh criptonun temizlenmiş hali
            //arinmis criptonun 1 adımı denk gelen gunahın ilk ayrılma anı
            //Aydinlanma Miktari 2. adımı Gunahsayisından arındırıyoruz
            //ardından aydinlanma miktarini chara dönüştürüp ruhun bir parçasını elde ediyoruz
            //Ve son adım olarak ruhParçalarını döngüde birleştiriyoruz
            string ruh = "";
            List<string> parts = Regex.Split(beden, @"(?<=[*_?])").ToList();
            foreach (string item in parts)
            {
                if (item.Contains("limbo"))
                {

                    string arinmis = item.TrimEnd('*');

                    int aydinlanmaMiktari = (Convert.ToInt32(arinmis) / 3) - 1;
                    string ruhParcasi = Convert.ToChar(aydinlanmaMiktari).ToString();
                    ruh += ruhParcasi;
                }
                else if (item.Contains("lust"))
                {

                    string arinmis = item.TrimEnd('*');

                    int aydinlanmaMiktari = (Convert.ToInt32(arinmis) / 2) - 2;
                    string ruhParcasi = Convert.ToChar(aydinlanmaMiktari).ToString();
                    ruh += ruhParcasi;
                }
                else if (item.Contains("gluttony"))
                {

                    string arinmis = item.TrimEnd('*');

                    int aydinlanmaMiktari = (Convert.ToInt32(arinmis) / 1) - 3;
                    string ruhParcasi = Convert.ToChar(aydinlanmaMiktari).ToString();
                    ruh += ruhParcasi;
                }
                else if (item.Contains("greed"))
                {

                    string arinmis = item.TrimEnd('*');

                    int aydinlanmaMiktari = (Convert.ToInt32(arinmis) / 3) - 4;
                    string ruhParcasi = Convert.ToChar(aydinlanmaMiktari).ToString();
                    ruh += ruhParcasi;
                }
                else if (item.Contains("anger"))
                {

                    string arinmis = item.TrimEnd('*');

                    int aydinlanmaMiktari = (Convert.ToInt32(arinmis) / 2) - 5;
                    string ruhParcasi = Convert.ToChar(aydinlanmaMiktari).ToString();
                    ruh += ruhParcasi;
                }
                else if (item.Contains("heresy"))
                {

                    string arinmis = item.TrimEnd('*');

                    int aydinlanmaMiktari = (Convert.ToInt32(arinmis) / 1) - 6;
                    string ruhParcasi = Convert.ToChar(aydinlanmaMiktari).ToString();
                    ruh += ruhParcasi;
                }
                else if (item.Contains("violence"))
                {

                    string arinmis = item.TrimEnd('*');

                    int aydinlanmaMiktari = (Convert.ToInt32(arinmis) / 3) - 7;
                    string ruhParcasi = Convert.ToChar(aydinlanmaMiktari).ToString();
                    ruh += ruhParcasi;
                }
                else if (item.Contains("fraud"))
                {

                    string arinmis = item.TrimEnd('*');

                    int aydinlanmaMiktari = (Convert.ToInt32(arinmis) / 2) - 8;
                    string ruhParcasi = Convert.ToChar(aydinlanmaMiktari).ToString();
                    ruh += ruhParcasi;
                }
                else if (item.Contains("treachery"))
                {

                    string arinmis = item.TrimEnd('*');

                    int aydinlanmaMiktari = (Convert.ToInt32(arinmis) / 1) - 9;
                    string ruhParcasi = Convert.ToChar(aydinlanmaMiktari).ToString();
                    ruh += ruhParcasi;
                }
            }
            return ruh;
        } 
    }
}

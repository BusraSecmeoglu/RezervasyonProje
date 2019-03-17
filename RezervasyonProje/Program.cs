using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RezervasyonProje.Classes;

namespace RezervasyonProje
{
    class Program
    {
        static void Main(string[] args)
        {
            Rezervasyon rezervasyon;
            int secim;
            Console.WriteLine("1 ) Otel      Rezervasyonu \n"
                             + "2 ) Çadır     Rezervasyonu \n"
                             + "3 ) YatLimanı Rezervasyonu \n"
                             + "4 ) Otel ve YatLimanı Rezervasyonu \n");
            Console.WriteLine("Rezervasyon Tipini Seçiniz");
            secim = Convert.ToInt32(Console.ReadLine());

            if (secim == 1)
            {
                rezervasyon = new OtelRezervasyon();

            }
            else if (secim == 2)
            {

                rezervasyon = new CadirRezervasyon();
            }
            else if (secim == 3)
            {
                rezervasyon = new YatLimaniRezervasyon();
            }
            else if (secim == 4)
            {
                rezervasyon = new OtelVeYatRezervasyon();
                rezervasyon.RasgeleDoldur();
            }
            else
            {
                Console.WriteLine("Yanlış Seçim");
                return;
            }

            rezervasyon.RasgeleDoldur();
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("        " + rezervasyon.UygulamaAdi());
                Console.WriteLine("   1- Bugunku bos " + rezervasyon.KiralananYerTipi() + " goster");
                Console.WriteLine("   2- 30 gunluk doluluk durumu");
                Console.WriteLine("   3- Gun bazinda doluluk oranlari");
                Console.WriteLine("   4- Bugun icin hizli rezervasyon");
                Console.WriteLine("   5- Iki tarih arasi rezervasyon");
                char gunSonuIslemi = '6';
                if (rezervasyon.YanYanaIkiYerBirdenRezervasyonYapilabilirMi())
                {
                    Console.WriteLine("   6- Bugun icin hizli rezervasyon (yan yana iki yer)");
                    Console.WriteLine("   7- Iki tarih arasi rezervasyon (yan yana iki yer)");
                    gunSonuIslemi = '8';
                }
                Console.WriteLine("   {0}- Gun sonu islemi", gunSonuIslemi);

                char c = Console.ReadKey().KeyChar;
                switch (c)
                {
                    case '1':
                        {
                            Console.WriteLine();
                            Console.WriteLine("Bugunku bos yerler");
                            rezervasyon.BugunkuBosOdalar();
                            break;
                        }
                    case '2':
                        {
                            Console.WriteLine();
                            Console.WriteLine("30 gunluk doluluk durumu");
                            rezervasyon.AylikDolulukDurumu();
                            break;
                        }
                    case '3':
                        {
                            Console.WriteLine();
                            Console.WriteLine("Gun bazinda doluluk oranlari");
                            rezervasyon.GunBazindaDolulukOranlari();
                            break;
                        }
                    case '4':
                        {
                            Console.WriteLine();
                            Console.WriteLine("Bugun icin hizli rezervasyon");
                            rezervasyon.RezervasyonYap();
                            break;
                        }
                    case '5':
                        {
                            Console.WriteLine();
                            Console.WriteLine("Iki tarih arasi rezervasyon");
                            DateTime date1 = DateTime.Today;
                            DateTime date2 = DateTime.Today;
                            try
                            {
                                Console.Write("Rezervasyon baslangic tarihi (gg/aa/yyyy): ");
                                string baslangicTarihi = Console.ReadLine();
                                date1 = Convert.ToDateTime(baslangicTarihi);

                                Console.Write("Rezervasyon bitis tarihi (gg/aa/yyyy) veya Gün sayisi Giriniz: ");
                                string bitisTarihi = Console.ReadLine();

                                bool sonuc = DateTime.TryParse(bitisTarihi, out date2); // Burada eğer girilen tarih
                                //ise çıktı olarak date2 değişkenine ata

                                if (!sonuc)
                                {
                                    //Eğer girdiğimiz değer bir sayi ise gunSayisi değişkenini inte çevir işlem yap
                                    int gunSayisi = Convert.ToInt32(bitisTarihi);
                                    rezervasyon.RezervasyonYap(date1, gunSayisi);
                                }
                                else
                                {
                                    rezervasyon.RezervasyonYap(date1, date2);
                                }

                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Tarih formatina dikkat ediniz");
                            }

                            break;
                        }
                    case '6':
                        {
                            Console.WriteLine();

                            if (secim == 1 || secim == 4)
                            {
                                rezervasyon.GunSonuIslemi();
                            }
                            else
                            {
                                Console.WriteLine("Bugun icin hizli rezervasyon (yan yana iki yer)");
                                rezervasyon.RezervasyonYap(yanYanaIkiRezervasyon: true);
                            }

                            break;
                        }
                    case '7':
                        {
                            Console.WriteLine();
                            Console.WriteLine("Iki tarih arasi rezervasyon (yan yana iki yer)");
                            DateTime date1 = DateTime.Today;
                            DateTime date2 = DateTime.Today;
                            try
                            {
                                Console.Write("Rezervasyon baslangic tarihi (gg/aa/yyyy): ");
                                string baslangicTarihi = Console.ReadLine();
                                date1 = Convert.ToDateTime(baslangicTarihi);

                                Console.Write("Rezervasyon bitis tarihi (gg/aa/yyyy): ");
                                string bitisTarihi = Console.ReadLine();

                                bool sonuc = DateTime.TryParse(bitisTarihi, out date2);

                                if (!sonuc)
                                {

                                    int gunSayisi = Convert.ToInt32(bitisTarihi);
                                    rezervasyon.RezervasyonYap(date1, gunSayisi);
                                }
                                else
                                {
                                    rezervasyon.RezervasyonYap(date1, date2);
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Tarih formatina dikkat ediniz");
                            }
                            rezervasyon.RezervasyonYap(date1, date2, yanYanaIkiRezervasyon: true);
                            break;
                        }
                    default:
                        {
                            if (c == gunSonuIslemi)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Gun sonu islemi");
                                rezervasyon.GunSonuIslemi();
                            }
                            break;
                        }
                }
            }
        }
    }
}

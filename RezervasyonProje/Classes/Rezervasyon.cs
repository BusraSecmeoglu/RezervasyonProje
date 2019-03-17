using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervasyonProje.Classes
{
    //Abstract class, bir sınıfın özelliklerini proje içinde diğer sınıflar içerisinde kullanmamızı sağlayan bir kalıtımdır.
//    Abstract Class’lar, diğer sınıflara base Class olmak için yazılır.Bu nedenle Abstract Class’dan nesne türetilemez.

//-Abstract method, sadece Abstract Class’ların içerisinde tanımlanır ve Abstract Class’ı kalıtan sınıf tarafından override edilmek zorundadır. 
//Oluşturduğumuz Abstract Class içerisine, Abstract Method yazılırken gövdesi yazılmaz ve daha sonra Abstract Class’ımızı kalıtacağımız sınıfta
//Abstract Method’u override ederiz.

//-Abstract Method’lar, Private olarak tanımlanamaz.

    abstract public class Rezervasyon
    {

        protected const int odaSayisi = 10;
        protected const int gunSayisi = 30;

        //Değişkenlerin alabileceği değerlerin sabit(belli) olduğu durumlarda programı daha okunabilir hale getirmek için kullanılır.Programda birçok değişkene tek tek sayısal değer vermek yerine "enum" kullanılabilir.
        protected enum RezervasyonEnum
        {
            Bos,
            Dolu,
            Temizlik
        }

        protected RezervasyonEnum[,] rezervasyonDurumu = new RezervasyonEnum[odaSayisi, gunSayisi];

        public Rezervasyon()
        {
        }
        public Rezervasyon(DateTime baslangic, DateTime bitis)
        {

        }
        public Rezervasyon(DateTime baslangic, int kalinacakGun)
        {

        }

        public abstract string KiralananYerTipi();
        public abstract string UygulamaAdi();
        public abstract bool YanYanaIkiYerBirdenRezervasyonYapilabilirMi();
        public abstract int Fiyat(int gunSayisi, bool cift);
        protected virtual bool ErtesiGunTemizlikIcinBosBirakilacakMi()
        {
            return true;
        }

        public virtual void BugunkuBosOdalar()
        {
            bool bosOdaYok = true;
            for (int i = 0; i < odaSayisi; i++)
            {
                if (rezervasyonDurumu[i, 0] == RezervasyonEnum.Bos
                    && rezervasyonDurumu[i, 1] == RezervasyonEnum.Bos)
                {
                    bosOdaYok = false;
                    Console.WriteLine(i + 1);
                }
            }
            if (bosOdaYok)
                Console.WriteLine("Bugun icin bos " + KiralananYerTipi() + " yok");
        }
        public virtual void RasgeleDoldur()
        {
            rezervasyonDurumu[0, 0] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[0, 1] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[0, 5] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[0, 6] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[1, 7] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[1, 8] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[2, 9] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[2, 10] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[5, 15] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[5, 16] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[8, 20] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[8, 21] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[0, 27] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[0, 28] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[9, 29] = RezervasyonEnum.Dolu;

            rezervasyonDurumu[4, 0] = RezervasyonEnum.Temizlik;
            rezervasyonDurumu[5, 1] = RezervasyonEnum.Dolu;
            rezervasyonDurumu[5, 2] = RezervasyonEnum.Temizlik;
        }

        //Virtual(sanal metod) 'lar kalıtıldığı sınıflarda içeriği değiştirilebilen metodlardır. Yani temel sınfımız da bir method oluşturuyoruz. Bu methodun içeriğini ileride değiştirme ihtiyacı duyuyoruz bu gibi durumlarda methodumuza virtual anahtar kelimesini ekleyerek override (ezmek) etmeye olanak tanımış oluyoruz.
        public virtual void AylikDolulukDurumu()
        {
            Console.Write("      ");
            TarihCetveliniYazdir();
            Console.WriteLine();
            for (int i = 0; i < odaSayisi; i++)
            {
                Console.Write("" + KiralananYerTipi() + " {0:00}", i + 1);
                for (int j = 0; j < gunSayisi; j++)
                {
                    if (rezervasyonDurumu[i, j] == RezervasyonEnum.Bos)
                        Console.Write(" - ");
                    else if (rezervasyonDurumu[i, j] == RezervasyonEnum.Dolu)
                        Console.Write(" D ");
                    else
                        Console.Write(" x ");
                }
                Console.WriteLine();
            }
        }
        public virtual void RezervasyonYap(bool yanYanaIkiRezervasyon = false)
        {
            RezervasyonYap(DateTime.Today, DateTime.Today, yanYanaIkiRezervasyon);
        }
        // baslangic tarihinden itibaren verilen gun sayisi kadar rezervasyon yapar
        public virtual void RezervasyonYap(DateTime basTarihi, int gun, bool yanYanaIkiRezervasyon = false)
        {
            RezervasyonYap(basTarihi, basTarihi.AddDays(gun - 1), yanYanaIkiRezervasyon);
        }
        // verilen iki tarih arasinda rezervasyon yapar
        public virtual void RezervasyonYap(DateTime date1, DateTime date2, bool yanYanaIkiRezervasyon = false)
        {
            if (yanYanaIkiRezervasyon == true && YanYanaIkiYerBirdenRezervasyonYapilabilirMi() == false)
            {
                Console.WriteLine("Ayni rezervasyonda yan yana iki yer ayrilmasina izin verilmiyor");
                return;
            }
            if (date1 < DateTime.Today)
            {
                Console.WriteLine("Baslangic tarihi bugunden kucuk olamaz");
                return;
            }
            if (date2 < date1)
            {
                Console.WriteLine("Bitis tarihi baslangic tarihinden kucuk olamaz");
                return;
            }
            if ((date1 - DateTime.Today).Days >= gunSayisi)
            {
                Console.WriteLine("Baslangic tarihi {0:dd/MM/yyyy} tarihinden buyuk olamaz", DateTime.Today.AddDays(gunSayisi - 1));
                return;
            }
            if ((date2 - DateTime.Today).Days >= gunSayisi)
            {
                Console.WriteLine("Bitis tarihi {0:dd/MM/yyyy} tarihinden buyuk olamaz", DateTime.Today.AddDays(gunSayisi - 1));
                return;
            }
            int gun1 = (date1 - DateTime.Today).Days;
            int gun2 = (date2 - DateTime.Today).Days;
            bool bosOdaYok = true;
            for (int i = 0; i < odaSayisi; i++)
            {
                // yerleri ikiser ikiser kontrol ederken, hep bir sonraki yere de bak.
                // son yerden bir onceki yere kadar devam et.
                if (yanYanaIkiRezervasyon && i == (odaSayisi - 1))
                    break;

                bool odaMusait = true;

                for (int k = 0; k <= (yanYanaIkiRezervasyon ? 1 : 0); k++)
                {
                    for (int j = gun1; j <= gun2; j++)
                    {
                        if (rezervasyonDurumu[i + k, j] != RezervasyonEnum.Bos)
                        {
                            odaMusait = false;
                            break;
                        }
                    }

                    if (ErtesiGunTemizlikIcinBosBirakilacakMi())
                    {
                        if (gun2 < (gunSayisi - 1))
                        {
                            if (rezervasyonDurumu[i + k, gun2 + 1] != RezervasyonEnum.Bos)
                                odaMusait = false;
                        }
                    }
                }
                if (odaMusait)
                {
                    bosOdaYok = false;
                    for (int j = gun1; j <= gun2; j++)
                    {
                        for (int k = 0; k <= (yanYanaIkiRezervasyon ? 1 : 0); k++) // sonraki yeri de rezerve et.
                            rezervasyonDurumu[i + k, j] = RezervasyonEnum.Dolu;
                    }

                    if (ErtesiGunTemizlikIcinBosBirakilacakMi())
                    {
                        if (gun2 < (gunSayisi - 1))
                        {
                            for (int k = 0; k <= (yanYanaIkiRezervasyon ? 1 : 0); k++) // sonraki yeri de rezerve et.
                                rezervasyonDurumu[i + k, gun2 + 1] = RezervasyonEnum.Temizlik;
                        }
                    }
                    Console.WriteLine("{0} "
                        + (yanYanaIkiRezervasyon ? "ve {1} " : "")
                        + "numarali " + KiralananYerTipi().ToLower() + " sizin icin ayrildi", i + 1, i + 2);

                    Console.WriteLine("Toplam fiyat : {1} TL, {0} gun.",
                        (date2 - date1).Days + 1, this.Fiyat((date2 - date1).Days + 1, yanYanaIkiRezervasyon));
                    break;
                }
            }
            if (bosOdaYok)
                Console.WriteLine("Istediginiz tarihte bos " + KiralananYerTipi().ToLower() + " yok");
        }

        public virtual void GunBazindaDolulukOranlari()
        {
            Console.Write(" -------- ");
            TarihCetveliniYazdir();

            Console.Write("\nDoluluk % ");
            int doluOdaSayisi = 0;

            for (int j = 0; j < gunSayisi; j++)
            {
                doluOdaSayisi = 0;
                for (int i = 0; i < odaSayisi; i++)
                {
                    if (rezervasyonDurumu[i, j] == RezervasyonEnum.Dolu)
                    {
                        doluOdaSayisi++;
                    }
                }
                Console.Write("{0,3}", (int)(100f * doluOdaSayisi / odaSayisi));
            }
            Console.WriteLine();
        }
        public virtual void GunSonuIslemi()
        {
            for (int i = 0; i < odaSayisi; i++)
            {
                for (int j = 0; j < gunSayisi - 1; j++)
                {
                    rezervasyonDurumu[i, j] = rezervasyonDurumu[i, j + 1];
                }
                if (rezervasyonDurumu[i, gunSayisi - 2] == RezervasyonEnum.Dolu)
                    rezervasyonDurumu[i, gunSayisi - 1] = RezervasyonEnum.Temizlik;
                else
                    rezervasyonDurumu[i, gunSayisi - 1] = RezervasyonEnum.Bos;
            }
        }


        //Bir özelliği veya metodu static yapmak için erişim belirleyicisinden sonra “static” anahtar kelimesini getirmemiz gerekir.Static bir metodun içerisinden static olmayan metotlar ve değişkenler çağrılamaz.Sadece diğer static metotlar ve değişkenler çağrılabilir.Statik metotlar ait oldukları sınıftan bir nesne oluşturulmadan çağrılabilir.Statik metotlar çok sık kullanılan işlemlerde kullanılır, böylelikle sürekli nesne oluşturulması engellenir.

        public static void TarihCetveliniYazdir()
        {
            for (int j = 0; j < gunSayisi; j++)
            {
                Console.Write(" {0:00}", DateTime.Today.AddDays(j).Day);
            }
        }



    }
}

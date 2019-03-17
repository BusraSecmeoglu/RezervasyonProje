using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervasyonProje.Classes
{
    public class YatLimaniRezervasyon : Rezervasyon
    {
        public override int Fiyat(int gunSayisi, bool ciftYer)
        {
            return gunSayisi * 1000 * (ciftYer ? 2 : 1);
        }

        public override string KiralananYerTipi()
        {
            return "Yer";
        }

        public override string UygulamaAdi()
        {
            return "Yat";
        }

        public override bool YanYanaIkiYerBirdenRezervasyonYapilabilirMi()
        {
            return true;
        }

        protected override bool ErtesiGunTemizlikIcinBosBirakilacakMi()
        {
            return false;
        }
        public override void AylikDolulukDurumu()
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
                        Console.Write(" - ");
                }
                Console.WriteLine();
            }
        }
    }
}

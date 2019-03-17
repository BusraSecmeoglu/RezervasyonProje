using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervasyonProje.Classes
{
    public class CadirRezervasyon : Rezervasyon
    {
        public override int Fiyat(int gunSayisi, bool ciftYer)
        {
            // kucuk cadir icin tek yer gunluk 100 TL. 
            // buyuk cadir icin cift yer gunluk 160 TL.
            return gunSayisi * (ciftYer ? 160 : 100);
        }

        public override string KiralananYerTipi()
        {
            return "Yer";

        }

        public override string UygulamaAdi()
        {
            return "Çadır";
        }

        public override bool YanYanaIkiYerBirdenRezervasyonYapilabilirMi()
        {
            return true;
        }

    }
}

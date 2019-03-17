using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervasyonProje.Classes
{
    public class OtelRezervasyon : Rezervasyon
    {

        public override int Fiyat(int gunSayisi, bool ciftYer)
        {
            return gunSayisi > 5 ? 90 : 100; // Eğer gün sayisi 5 ten büyük ise 90 değillse 100 lira ödesin
        }

        public override string KiralananYerTipi()
        {
            return "Oda";

        }

        public override string UygulamaAdi()
        {
            return "Otel";
        }

        public override bool YanYanaIkiYerBirdenRezervasyonYapilabilirMi()
        {
            return false;
        }

    }
}

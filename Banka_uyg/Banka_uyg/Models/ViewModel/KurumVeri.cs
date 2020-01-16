using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banka_uyg.Models.ViewModel
{
    public class KurumVeri
    {
        public int HgsId { get; set; }
        public int HgsHesap { get; set; }
        public string MusteriTc { get; set; }
        public decimal Tutar { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }

        public virtual KurumVeri Hgs1 { get; set; }
        public virtual KurumVeri Hgs2 { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Banka_uyg.Models;
using System.Web.Services;

namespace Banka_uyg
{
   
    /// <summary>
    /// Summary description for Service
    /// </summary>
   // [WebService(Namespace = "http://tempuri.org/")]
  //  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
   // [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    //public class Service : System.Web.Services.WebService
    //{
    //    HGSEntities db = new HGSEntities();
    //    BankaEntities data = new BankaEntities();
    //    Islemler hgs = new Islemler();
    //    [WebMethod]
    //    public string HGSIslemleri(string tc,string hesapNo,int ekno,int tutar)
    //    {
    //        var kisiTc = db.Islemler.Where(u=>u.Tc==tc).FirstOrDefault();
    //        var kisiTc2 = data.Hesaplar.Where(m => m.MusteriTc == tc && m.HesapNo == hesapNo && m.EkNumara == ekno&&m.HesapDurum==true).FirstOrDefault();
       
    //        if ( kisiTc ==null && kisiTc2!=null && kisiTc2.Bakiye>=tutar)
    //        {
    //            hgs.Tc = tc.ToString();
    //            hgs.Tarih = DateTime.Now;
    //            hgs.Tutar = tutar;
    //            kisiTc2.Bakiye -= tutar;
    //            db.Islemler.Add(hgs);
    //            db.SaveChanges();
    //            data.SaveChanges();
    //            return "İşlem gerçekleşmiştir...";
    //        }
    //       if(kisiTc != null && kisiTc2 != null && kisiTc2.Bakiye >= tutar)
    //        {
    //            kisiTc.Tutar += tutar;
    //            hgs.Tutar = tutar;
    //            kisiTc2.Bakiye -= tutar;
    //            db.SaveChanges();
    //            data.SaveChanges();
    //            return "İşlem gerçekleşmiştir...";
    //        }
    //        if (kisiTc == null && kisiTc2 != null && kisiTc2.Bakiye < tutar)
    //            return "Bakiye Tutarınız Yetersizdir...";

    //        if (kisiTc != null && kisiTc2 != null && kisiTc2.Bakiye < tutar)
    //            return "Bakiye Tutarınız Yetersizdir...";

    //        if (kisiTc != null && kisiTc2 == null )
    //            return "Hesabınız olmadan işlem gerçekleştiremezsiniz...";

    //        if (kisiTc == null && kisiTc2 == null)
    //            return "Böyle müşteri sistemde yok...";

    //        else
    //            return "Hata Oluşmuştur...";
    //    }

       
    //}
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Banka_uyg.Models;
using Banka_uyg.Models.ViewModel;

namespace Banka_uyg.Controllers
{
    public class KurumController : Controller
    {
        BankaEntities1 banka = new BankaEntities1();

        // GET: Kurum
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HgsSatis()
        {
            Session["HgsHesap"] = "";
            Hgs hgs = new Hgs();
            Random rnd = new Random();
            int hgsno = rnd.Next(10000, 90000);
            var hgsNo = banka.Hgs.Where(m => m.HgsHesap == hgsno).FirstOrDefault();
            if (Session["UserTc"] == null && Session["UserAdSoyad"] == null )
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                string tc = Session["UserTc"].ToString();
                List<Hesaplar> Hesaplar = banka.Hesaplar.Where(x => x.MusteriTc == tc && x.HesapDurum == true).ToList();
                if (hgsNo == null)
                {
                    hgs.HgsHesap = hgsno;
                    Session["HgsHesap"] = hgs.HgsHesap;
                }
                else
                {
                    Random r = new Random();
                    int hg = rnd.Next(10000, 90000);
                    hgs.HgsHesap = hg;
                    Session["HgsHesap"] = hgs.HgsHesap;
                }

                return View(Hesaplar);
            }
        }
        [HttpPost]
        public ActionResult HgsSatis(FormCollection frm)
        {
            Hgs hgs = new Hgs();
            string tc = Session["UserTc"].ToString();
            hgs.MusteriTc = Session["UserTc"].ToString();
            var musteri = banka.Musteriler.Where(m => m.TC == hgs.MusteriTc).FirstOrDefault();
           // var hesap= banka.Hesaplar.Where(m => m.MusteriTc == hgs.MusteriTc).FirstOrDefault();
            if (musteri != null)
            {
                hgs.HgsHesap = Convert.ToInt32(Session["HgsHesap"]);
                hgs.Tutar = Convert.ToDecimal(frm["tutar"]);
                hgs.Tarih = DateTime.Now;
                hgs.Kanal = "Web";
                banka.Hgs.Add(hgs);

                if(frm["hesap"]=="0")
                {
                    ViewBag.hataa=" Hesap Numarası seçiniz";
                    
                    List<Hesaplar> Hesaplar = banka.Hesaplar.Where(x => x.MusteriTc == tc && x.HesapDurum == true).ToList();
                
                    return View(Hesaplar);

                }
                else
                {
                    int hesapno = Convert.ToInt32(frm["hesap"]);
                    
                    var hesap = banka.Hesaplar.Where(x => x.EkNumara == hesapno&& x.MusteriTc==tc).FirstOrDefault();
                    if (hgs.Tutar > hesap.Bakiye)
                    {
                        ViewBag.kontrol = "hesabınızda " + hesap.Bakiye + " bakiye bulunurken bu miktardan daha fazla para  yükleyemezsiniz!!";
                       
                        List<Hesaplar> Hesaplar = banka.Hesaplar.Where(x => x.MusteriTc == tc && x.HesapDurum == true).ToList();
                        return View(Hesaplar);
                    }
                    else
                    {
                        hesap.Bakiye -= hgs.Tutar;
                        banka.SaveChanges();
                        KurumVeri veri = new KurumVeri();
                        veri.HgsHesap = hgs.HgsHesap;
                        veri.MusteriTc = hgs.MusteriTc;
                        veri.Tarih = hgs.Tarih;
                        veri.Tutar = hgs.Tutar;

                        HttpResponseMessage response = GlobalVariables.WEbApiClient.PostAsJsonAsync("Hgs", veri).Result;
                        TempData["Kayit"] = "İşlem Başarıyla Gerçekleştirilmiştir";
                        
                        List<Hesaplar> Hesaplar = banka.Hesaplar.Where(x => x.MusteriTc == tc && x.HesapDurum == true).ToList();
                        ViewBag.kontrol = "İşleminiz Başarıyla Gerçekleştirilmiştir.";
                        return View(Hesaplar);
                    }
                }
               
              
            }
            else
            {
                return RedirectToAction("HgsSatis", "Kurum");
            }
          
            
        }

        public ActionResult HgsBakiyeYukleme()
        {
            if (Session["UserTc"] == null && Session["UserAdSoyad"] == null )
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                string tc = Session["UserTc"].ToString();
                List<Hesaplar> Hesaplar = banka.Hesaplar.Where(x => x.MusteriTc == tc && x.HesapDurum == true).ToList();

                return View(Hesaplar);
            }
        }

        [HttpPost]
        public ActionResult HgsBakiyeYukleme(FormCollection frm)
        {
            if (Session["UserTc"] == null && Session["UserAdSoyad"] == null)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                string tc = Session["UserTc"].ToString();
                //List<Hgs> HgsHesaplar = banka.Hgs.Where(x => x.MusteriTc == tc).ToList();
                if (frm["hesap"] == "0")
                {
                    ViewBag.hataa = " Hesap Numarası seçiniz";
                    
                    List<Hesaplar> Hesaplar = banka.Hesaplar.Where(x => x.MusteriTc == tc && x.HesapDurum == true).ToList();

                    return View(Hesaplar);

                }
                else
                {
                    int hesapno = Convert.ToInt32(frm["hesap"]);
                    var hesap = banka.Hesaplar.Where(x => x.EkNumara == hesapno&&x.MusteriTc==tc).FirstOrDefault();
                    decimal tutar = Convert.ToDecimal(frm["tutar"]);
                    if (tutar > hesap.Bakiye)
                    {
                        ViewBag.kontrol = "hesabınızda " + hesap.Bakiye + " bakiye bulunurken bu miktardan daha fazla para  yükleyemezsiniz!!";

                        List<Hesaplar> Hesaplar = banka.Hesaplar.Where(x => x.MusteriTc == tc && x.HesapDurum == true).ToList();
                        return View(Hesaplar);

                    }
                    else
                    {

                        int hgsHesapNo = Convert.ToInt32(frm["hgsNo"]);
                        var kurum = banka.Hgs.Where(x => x.HgsHesap == hgsHesapNo).FirstOrDefault();
                        KurumVeri veri = new KurumVeri();
                        veri.HgsId = kurum.HgsId;
                        veri.HgsHesap = kurum.HgsHesap;
                        veri.MusteriTc = kurum.MusteriTc;
                        veri.Tarih = DateTime.Now;
                        veri.Tutar = kurum.Tutar + tutar;
                        kurum.Tutar = veri.Tutar;
                        kurum.Tarih = veri.Tarih;
                        kurum.Kanal = "Web";
                        HttpResponseMessage response = GlobalVariables.WEbApiClient.PutAsJsonAsync("Hgs/" + kurum.HgsId, veri).Result;
                        hesap.Bakiye -= tutar;

                        banka.SaveChanges();


                        List<Hesaplar> Hesaplar = banka.Hesaplar.Where(x => x.MusteriTc == tc && x.HesapDurum == true).ToList();
                        ViewBag.kontrol = "İşleminiz Başarıyla Gerçekleştirilmiştir.";
                        return View(Hesaplar);
                    }
                }
               
                

            }
        }
        public ActionResult HgsSorgu()
        {
          


                return View(new KurumVeri());

        }
        [HttpPost]
        public ActionResult HgsSorgu(FormCollection frm)
        {
            int hgsHesapNo = Convert.ToInt32(frm["hgsNo"]);
            var kurum = banka.Hgs.Where(x => x.HgsHesap == hgsHesapNo).FirstOrDefault();
          
            HttpResponseMessage response = GlobalVariables.WEbApiClient.GetAsync("Hgs/"+kurum.HgsId.ToString()).Result;
          
            return View(response.Content.ReadAsAsync<KurumVeri>().Result);

            

        }
    }
}
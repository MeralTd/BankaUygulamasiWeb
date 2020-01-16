using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Banka_uyg.Models;


namespace Banka_uyg.Controllers
{
    public class HomeController : Controller
    {
        BankaEntities1 db = new BankaEntities1();
    
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            if (Session["UserTc"] == null && Session["UserAdSoyad"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Login()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
           
                Musteriler mus = new Musteriler();
                mus.TC = form["tc"];
                
                mus.Sifre = form["sifre"];
                var musteri = db.Musteriler.Where(q => q.TC == mus.TC && q.Sifre == mus.Sifre).FirstOrDefault();
              
                if (musteri != null)
                {

                Session["UserAdSoyad"] = musteri.Ad +" "+ musteri.Soyad;
                Session["UserTc"] = musteri.TC;
   

                return RedirectToAction("About");

                }


                else
                {
                    ViewBag.giris = "Hatalı Tc Kimlik ve şifre girdiniz.";
                    return View();
                }


        }


        public ActionResult KayitOl()
        {

            return View();
        }
        [HttpPost]
        public ActionResult KayitOl(FormCollection frm)
        {
            Musteriler musteri = new Musteriler();
            musteri.TC = frm["tc"];
            ViewBag.mesaj = "Hatalı TC girilmiştir. ";
            String Tc = frm["tc"];
            


            if ((!(db.Musteriler.Where(m => m.TC == musteri.TC).Any()))&&(Tc.Length==11))
            {
                musteri.Ad = frm["ad"];
                musteri.Soyad = frm["soyad"];
                musteri.Cinsiyet = frm["cinsiyet"];
                musteri.DoğumTarihi = frm["tarih"];

                musteri.Telefon = frm["telefon"];
                musteri.EMail = frm["email"];
                musteri.Adres = frm["adres"];
                musteri.Sifre = frm["sifre"];
                musteri.Kanal = "web";
                db.Musteriler.Add(musteri);
                db.SaveChanges();
                return RedirectToAction("Login", "Home");
            }
            else
           
            return View(); 

            
        }


        public ActionResult HesapIslemleri()
        {
            List<Hesaplar> gHesaps = new List<Hesaplar>();
            if (Session["UserTc"] == null && Session["UserAdSoyad"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                string tc = Session["UserTc"].ToString();
                List<Hesaplar> Hesaplar = db.Hesaplar.Where(x => x.MusteriTc == tc).ToList();

                if (Hesaplar.Count == 0)
                {
                    return View();
                }
                else
                {
                    //gelen kişinin hesapları çekiliyor tek hesap üzerinde çalıştıgımızdan başka biri giriş yapsa dahi hesapekle.cshtlm'deki modele tüm true değere sahip hesapların hepsi geliyordu.

                    foreach (var item in Hesaplar)
                    {
                        if (item.HesapDurum == true)
                            gHesaps.Add(item);
                    }
                    return View(gHesaps);

                }

            }
        }

        public ActionResult HesapEkle()
        {
            int sayac = 1001;
            if (Session["UserTc"] == null && Session["UserAdSoyad"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                string tc = Session["UserTc"].ToString();
                List<Hesaplar> Hesaplar = db.Hesaplar.Where(x => x.MusteriTc == tc).ToList();
                foreach (var item in Hesaplar)
                {
                    sayac++;
                }

                if (Hesaplar.Count == 0)
                {
                    Random r = new Random();
                    //ilk defa gelen müşteri modele hiç bir şey göndermesem dahi nesne başvurusu ayarlanmadı uyarısı alıyorum
                    ViewBag.HesapNo = r.Next(10000, 90000).ToString() + "-" + sayac;
                   
                    
                    Session["HesapNo"] = r.Next(10000, 90000).ToString();
                    Session["HesapNoTamami"] = Session["HesapNo"] + "-" + sayac;
                    return View();
                }
                else
                {


                    //gelen kişinin hesapları çekiliyor tek hesap üzerinde çalıştıgımızdan başka biri giriş yapsa dahi hesapekle.cshtlm'deki modele tüm true değere sahip hesapların hepsi geliyordu.
                    var hesap = db.Hesaplar.Where(x => x.MusteriTc == tc).FirstOrDefault();
                    Session["HesapNoTamami"] = hesap.HesapNo + "-" + sayac;
                    Session["HesapNo"] = hesap.HesapNo;
                    return View();
                }


            }
        }
        [HttpPost]
        public ActionResult HesapEkle(FormCollection frm)
        {
            
            Hesaplar hesap = new Hesaplar();
            int sayac = 1001;
            if(Session["UserTc"]==null&& Session["UserAdSoyad"]==null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                string tc = Session["UserTc"].ToString();
                List<Hesaplar> Hesaplar = db.Hesaplar.Where(x => x.MusteriTc == tc).ToList();
                if (Hesaplar.Count == 0)
                {

                    String hesapNo = Session["HesapNo"].ToString();
                    hesap.HesapNo = hesapNo;
                    hesap.EkNumara = sayac;

                }
                else
                {
                    foreach (var item in Hesaplar)
                    {
                        sayac++;
                        String hesapNo = Session["HesapNo"].ToString();
                        hesap.HesapNo = hesapNo;
                        hesap.EkNumara = sayac;

                    }

                }

                hesap.MusteriTc = Session["UserTc"].ToString();
                hesap.Bakiye = Convert.ToDecimal(frm["bakiye"]);
                hesap.HesapDurum = true;
                db.Hesaplar.Add(hesap);
                db.SaveChanges();
                var hesap2 = db.Hesaplar.Where(x => x.MusteriTc == tc).FirstOrDefault();
                Session["HesapNoTamami"] = hesap2.HesapNo + "-" + sayac;
                frm.Clear();


                return RedirectToAction("HesapIslemleri");
            }
           
        }



        public ActionResult HesapKapat(int? id)
        {

            string Tc = Session["UserTc"].ToString();

            Hesaplar hesap = db.Hesaplar.Where(x => x.MusteriTc == Tc && x.EkNumara == id).FirstOrDefault();
            if (hesap.Bakiye > 0)
            {
                ViewData["HesapKapat"] = "Bakiyeniz 0 olmadığı için hesabınızı kapatamazsınız.";
            }
            else
            {
                hesap.HesapDurum = false;
                db.SaveChanges();
            }


            return RedirectToAction("HesapIslemleri");
        }


        public ActionResult LogOut()
        {
            Session["UserAdSoyad"] = null;
            Session["UserTc"] = null;
            Session["HesapNo"] = null;
            Session["HesapNoTamami"] = null;
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }



        public ActionResult BakiyeEkle(int? id)
        {
            if (Session["UserTc"] == null && Session["UserAdSoyad"] == null&& id==null)
            {
                return RedirectToAction("Index");
            }
            else
            {

                string tc = Session["UserTc"].ToString();
                Hesaplar hesap = db.Hesaplar.Where(x => x.MusteriTc == tc && x.EkNumara == id).FirstOrDefault();
                if (hesap == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Hesap = hesap.HesapNo + "-" + id;
                Session["Hesap"] = ViewBag.Hesap;
                Session["eknumara"] = id;
                return View();

            }
        }
        [HttpPost]
        public ActionResult BakiyeEkle(FormCollection frm)
        {
            string tc = Session["UserTc"].ToString();
            int ek = Convert.ToInt32(Session["eknumara"]);
            Hesaplar hesap = db.Hesaplar.Where(x => x.MusteriTc == tc && x.EkNumara == ek).FirstOrDefault();
            decimal bakiye = Convert.ToDecimal(frm["bakiye"]);
            hesap.Bakiye = hesap.Bakiye + bakiye;
            db.SaveChanges();
            return RedirectToAction("HesapIslemleri");
        }


        public ActionResult BakiyeCikar(int? id)
        {
            if (Session["UserTc"] == null && Session["UserAdSoyad"] == null && id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {

                string tc = Session["UserTc"].ToString();
                Hesaplar hesap = db.Hesaplar.Where(x => x.MusteriTc == tc && x.EkNumara == id).FirstOrDefault();
                if (hesap == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Hesap = hesap.HesapNo + "-" + id;
                Session["Hesap"] = ViewBag.Hesap;
                Session["eknumara"] = id;
                return View();

            }
        }
        [HttpPost]
        public ActionResult BakiyeCikar(FormCollection frm)
        {
            string tc = Session["UserTc"].ToString();
            int ek = Convert.ToInt32(Session["eknumara"]);
            Hesaplar hesap = db.Hesaplar.Where(x => x.MusteriTc == tc && x.EkNumara == ek).FirstOrDefault();
            decimal bakiye = Convert.ToDecimal (frm["bakiye"]);
            if (bakiye > hesap.Bakiye)
            {
                ViewBag.kontrol = "hesabınızda " + hesap.Bakiye + " bakiye bulunurken bu miktardan daha fazla para  çekemezsiniz!!";
                return View();
            }
            else
            {
                hesap.Bakiye = hesap.Bakiye - bakiye;
                db.SaveChanges();
                return RedirectToAction("HesapIslemleri");
            }
            
        }


        public ActionResult Havale(int? id)
        {
            if (Session["UserTc"] == null && Session["UserAdSoyad"] == null && id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {

                string tc = Session["UserTc"].ToString();
                Hesaplar hesap = db.Hesaplar.Where(x => x.MusteriTc == tc && x.EkNumara == id).FirstOrDefault();
                if (hesap == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Hesap = hesap.HesapNo + "-" + id;
                Session["Hesap"] = ViewBag.Hesap;
                Session["eknumara"] = id;
                return View();

            }
        }
        [HttpPost]
        public ActionResult Havale(FormCollection frm)
        {
            string tc = Session["UserTc"].ToString();
            int ek = Convert.ToInt32(Session["eknumara"]);
            string hsbNo = frm["yatirHesapNo"];
            int ekno = int.Parse(frm["yatirEkNo"]);
            Hesaplar hesap = db.Hesaplar.Where(x => x.MusteriTc == tc && x.EkNumara == ek).FirstOrDefault();
            var Yatirhs = db.Hesaplar.Where(x => x.HesapNo == hsbNo && x.EkNumara == ekno).FirstOrDefault();
            decimal bakiye = Convert.ToDecimal(frm["bakiye"]);

            Session["HesapNo"] = frm["yatirHesapNo"] + " -" + frm["yatirEkNo"];
                if (Yatirhs != null )
                {
                if (hesap.HesapNo == hsbNo)
                {
                    ViewBag.havale = "Havale işleminde Kendi hesaplarınız arasında para gönderme işlemi yapamazsınız.";
                }
                else if (Yatirhs.HesapDurum == true)
                {
                    if (bakiye > hesap.Bakiye)
                    {
                        ViewBag.havale = "hesabınızda " + hesap.Bakiye + " bakiye bulunurken bu miktardan daha fazla para yatıramazsınız!!";
                        return View();
                    }
                    else
                    {
                        Yatirhs.Bakiye = Yatirhs.Bakiye + bakiye;
                        hesap.Bakiye = hesap.Bakiye - bakiye;
                        Havale hvl = new Havale();
                        hvl.GelenHesapNo = Session["Hesap"].ToString();
                        hvl.GidenHesapNo = Session["HesapNo"].ToString();
                        hvl.MusteriId = tc;
                        hvl.Miktar = Convert.ToDecimal(frm["bakiye"]);
                        hvl.Tarih = DateTime.Now;
                        db.Havale.Add(hvl);
                        db.SaveChanges();
                        ViewBag.havale = "işlem başarılı";
                    }
                }
                
                }
                else
                {
                    ViewBag.havale = "yatırmak istediğiniz hesap aktif değil!!";
                }
            return View();
        }


        public ActionResult Virman(int? id)
        {
            if (Session["UserTc"] == null && Session["UserAdSoyad"] == null && id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {

                string tc = Session["UserTc"].ToString();
                Hesaplar hesap = db.Hesaplar.Where(x => x.MusteriTc == tc && x.EkNumara == id).FirstOrDefault();
                if (hesap == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Hesap = hesap.HesapNo + "-" + id;
                Session["Hesap"] = ViewBag.Hesap;
                Session["eknumara"] = hesap.EkNumara;
                List<Hesaplar> Hesaplar = db.Hesaplar.Where(x => x.MusteriTc == tc).ToList();
                return View(Hesaplar);

            }
        }
        [HttpPost]
        public ActionResult Virman(FormCollection frm)
        {
            string tc = Session["UserTc"].ToString();
            List<Hesaplar> Hesaplar = db.Hesaplar.Where(x => x.MusteriTc == tc).ToList();
            
            int ek = Convert.ToInt32(Session["eknumara"]);
            decimal bakiye = Convert.ToDecimal(frm["bakiye"]); 

            Hesaplar hesap = db.Hesaplar.Where(x => x.MusteriTc == tc && x.EkNumara == ek).FirstOrDefault();
            if (bakiye > hesap.Bakiye)
            {
                ViewBag.Virman = "hesabınızda " + hesap.Bakiye + " bakiye bulunurken bu miktardan daha fazla para yatıramazsınız!!";
                return View(Hesaplar);
            }
            else
            {
                hesap.Bakiye = hesap.Bakiye - bakiye;
                int gidenEkNo = int.Parse(frm["ekNo"]);
                if (gidenEkNo.ToString() == null)
                {
                    ViewBag.Virman = "lütfen bir hesap seçiniz ek hesabınız yok ise ek hesap açıp deneyiniz!!";
                    return View();
                }
                else
                {
                    
                    Hesaplar hsb = db.Hesaplar.Where(x => x.MusteriTc == tc && x.EkNumara == gidenEkNo).FirstOrDefault();
                    hsb.Bakiye = hsb.Bakiye + bakiye;

                    Session["HesapNo"] = hesap.HesapNo + " -" + gidenEkNo;
                    Virman vrm = new Virman();
                    vrm.GelenHesapNo = Session["Hesap"].ToString();
                    vrm.GidenHesapNo = Session["HesapNo"].ToString();
                    vrm.MusteriId = hesap.MusteriTc;
                    vrm.Miktar = Convert.ToDecimal(frm["bakiye"]);
                    vrm.Tarih = DateTime.Now;

                    db.Virman.Add(vrm);

                    ViewBag.Virman = "İşlem başarıyla oluşturuldu.";
                    db.SaveChanges();
                }
                
            }
            return View(Hesaplar);
        }

     
    }
}
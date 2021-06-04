using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using APP2000.Models;

namespace APP2000.Controllers
{
    public class HomeController : Controller
    {
        APP2000Entities db = new APP2000Entities();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.TBLUserInfo.ToList());
        }

        // Referanse til registrering view
        public ActionResult Registrer()
        {
            return View();
        }

        // Registrering format, med post funksjon
        [HttpPost]
        public ActionResult Registrer (TBLUserInfo tBLUserInfo)
        {
            if(db.TBLUserInfo.Any(x=>x.Brukernavn == tBLUserInfo.Brukernavn))
            {
                ViewBag.Notification = "This account has already existed";
                return View();
            }
            else
            {
                db.TBLUserInfo.Add(tBLUserInfo);
                db.SaveChanges();

                Session["IdSS"] = tBLUserInfo.Id.ToString();
                Session["BrukernavnSS"] = tBLUserInfo.Brukernavn.ToString();
                return RedirectToAction("Home", "Home");
            }
            
        }

        // Log ut funksjon
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // Login funksjon i applikasjon
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TBLUserInfo tBLUserInfo)
        {
            var checklogin = db.TBLUserInfo.Where(x => x.Brukernavn.Equals(tBLUserInfo.Brukernavn)|| x.Passord.Equals(tBLUserInfo.Passord)).FirstOrDefault();
            if (checklogin != null)
            {
                Session["IdSS"] = tBLUserInfo.Id.ToString();
                Session["BrukernavnSS"] = tBLUserInfo.Brukernavn.ToString();
                return RedirectToAction("About", "Home");
            }
            else
            {
                ViewBag.Notifications = "Feil brukernavn eller passord";
            }
            return View();
        }

        // referanse til home view
        public ActionResult Home()
        {
            return View();
        }

        // referanse til about view
        public ActionResult About()
        {
            ViewBag.Message = "Her kan man finne information litt informasjon om oppgaven og gruppen";

            return View();
        }

        // referanse til contact view
        public ActionResult Contact()
        {
            ViewBag.Message = "Finn kontakt informasjon";

            return View();
        }

        // passer på at man må være logget inn for å gå til nettsiden (Lagt til i web.config fil for å gjøre det sånn)
        [Authorize]
        public ActionResult Kandidat()
        {
            return View();
        }


        // Hente ut kandidatene i modellen
        public ActionResult GetData()
        {
            using (DBModel db = new DBModel())
            {
                List<Kandidat> kanList = db.APP2000DB.ToList<Kandidat>();
                return Json(new { data = kanList }, JsonRequestBehavior.AllowGet);
            }
        }


        // Finn og sjekke kandidat for legge til eller endre
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Kandidat());
            else
            {
                using (DBModel db = new DBModel())
                {
                    return View(db.APP2000DB.Where(x => x.bruker == id).FirstOrDefault<Kandidat>());
                }
            }
        }


        // Legge til eller endre kandidat i databasemodell
        [HttpPost]
        public ActionResult AddOrEdit(Kandidat kan)
        {
            using (DBModel db = new DBModel())
            {
                if (kan.bruker == 0)
                {
                    db.APP2000DB.Add(kan);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Kandidat nominert" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(kan).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Kandidat info oppdatert" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        // Slette kandidat fra database
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                Kandidat kan = db.APP2000DB.Where(x => x.bruker == id).FirstOrDefault<Kandidat>();
                db.APP2000DB.Remove(kan);
                db.SaveChanges();
                return Json(new { success = true, message = "Kandidat slettet" }, JsonRequestBehavior.AllowGet);
            }
        }

}
}

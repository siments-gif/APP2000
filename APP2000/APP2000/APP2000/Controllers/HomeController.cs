using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APP2000.Models;

namespace APP2000.Controllers
{
    public class HomeController : Controller
    {
        APP2000Entities db = new APP2000Entities();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.TBLUserInfoes.ToList());
        }
        public ActionResult Registrer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrer (TBLUserInfo tBLUserInfo)
        {
            if(db.TBLUserInfoes.Any(x=>x.Brukernavn == tBLUserInfo.Brukernavn))
            {
                ViewBag.Notification = "This account has already existed";
                return View();
            }
            else
            {
                db.TBLUserInfoes.Add(tBLUserInfo);
                db.SaveChanges();

                Session["IdSS"] = tBLUserInfo.Id.ToString();
                Session["BrukernavnSS"] = tBLUserInfo.Brukernavn.ToString();
                return RedirectToAction("Index", "Home");
            }
            
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TBLUserInfo tBLUserInfo)
        {
            var checklogin = db.TBLUserInfoes.Where(x => x.Brukernavn.Equals(tBLUserInfo.Brukernavn)|| x.Passord.Equals(tBLUserInfo.Passord)).FirstOrDefault();
            if (checklogin != null)
            {
                Session["IdSS"] = tBLUserInfo.Id.ToString();
                Session["BrukernavnSS"] = tBLUserInfo.Brukernavn.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notifications = "Feil brukernavn eller passord";
            }
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Her kan man finne information om valget";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Find contact information";

            return View();
        }

        public ActionResult Kandidat()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (DBModel db = new DBModel())
            {
                List<Kandidat> kanList = db.APP2000DB.ToList<Kandidat>();
                return Json(new { data = kanList }, JsonRequestBehavior.AllowGet);
            }
        }


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
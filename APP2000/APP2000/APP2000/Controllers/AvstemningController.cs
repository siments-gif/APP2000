using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP2000.Controllers
{
    public class AvstemningController : Controller
    {
        // GET: Counter
        public ActionResult Avstemning()
        {
            return View();
        }
        public ActionResult Stemt()
        {
            return View();
        }
        public ActionResult Resultat()
        {
            return View();
        }
        public class Counter
        {
            public Counter(string name, int count)
            {
                Name = name;
                this.count = count;
            }
            public string Name { get; }

            public int count;

            public int GetCount()
            {
                return count;
            }
        }


        class Program
        {
            static void Main(String[] args)
            {
                var simen = new Counter("simen", 4);
                var ole = new Counter("ole", 4);
                var pole = new Counter("pole", 4);

                int total = simen.GetCount() + ole.GetCount() + pole.GetCount();

                Console.WriteLine($"Antall stemmer for simen: {simen.GetCount()}");
                Console.WriteLine($"Antall stemmer for ole: {ole.GetCount()}");
                Console.WriteLine($"Antall stemmer for pole: {pole.GetCount()}");
                Console.WriteLine($"Total antall stemmer: {total}");

                if (simen.GetCount() > ole.GetCount())
                {
                    if (simen.GetCount() > pole.GetCount())
                    {
                        Console.WriteLine($"Simen har vunnet");
                    }
                    else if (pole.GetCount() > simen.GetCount())
                    {
                        Console.WriteLine($"Pole har vunnet");
                    }
                    else
                    {
                        Console.WriteLine($"Uavgjort mellom Simen og Pole");
                    }
                }
                else if (ole.GetCount() > simen.GetCount())
                {
                    if (ole.GetCount() > pole.GetCount())
                    {
                        Console.WriteLine($"Ole har vunnet");
                    }
                    else if (pole.GetCount() > ole.GetCount())
                    {
                        Console.WriteLine($"Pole har vunnet");
                    }
                    else
                    {
                        Console.WriteLine($"Uavgjort mellom Ole og Pole");
                    }
                }
                else if (pole.GetCount() > simen.GetCount())
                {
                    Console.WriteLine($"Pole har vunnet");
                }
                else if (simen.GetCount() == pole.GetCount())
                {
                    Console.WriteLine($"Uavgjort mellom Simen, Ole og Pole");
                }
                else
                {
                    Console.WriteLine($"Uavgjort mellom Simen og Ole.");
                }
            }
        }
    }
}
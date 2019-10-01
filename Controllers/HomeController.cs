using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StarSubs.Models;

namespace StarSubs.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Sub sandwich)

        {
            //Number of meals
            string subNumber = sandwich.MealNumber;

            // "Name" Price
            double[] subPrice = new double[] { 1, 2, 3, 4, 5 };
            string subName = Enum.GetName(typeof(SubName), sandwich.SubName);
            double priceName = subPrice[(int)sandwich.SubName];

            //"Size" price
            double[] sizePrice = new double[] { 2, 3, 4, 5 };
            string size = Enum.GetName(typeof(Size), sandwich.Size);
            double priceSize = sizePrice[(int)sandwich.Size];

            //"Deal" Price
            double[] dealPrice = new double[] { 0, 1, 1.25 };
            string deal = Enum.GetName(typeof(Meal), sandwich.Meal);
            double priceDeal = dealPrice[(int)sandwich.Meal];

            //Calculate
            double unitPrice = (priceName * priceSize);
            double preTaxPrice = priceDeal + (priceName * priceSize);
            double taxPrice = preTaxPrice * 0.15;
            double totalPrice = preTaxPrice + taxPrice;

            double totalNumberPrice = totalPrice * Int32.Parse(subNumber);



            DateTime today = DateTime.Today;
            string date = today.ToLongDateString();

            // Send to Receipt
            String unitPrice2 = Convert.ToDecimal(unitPrice).ToString("C");
            String dealPrice2 = Convert.ToDecimal(priceDeal).ToString("C");
            String preTaxPrice2 = Convert.ToDecimal(preTaxPrice).ToString("C");
            String taxPrice2 = Convert.ToDecimal(taxPrice).ToString("C");
            String totalPrice2 = Convert.ToDecimal(totalPrice).ToString("C");
            String totalNumberPrice2 = Convert.ToDecimal(totalNumberPrice).ToString("C");


            ViewData["mealNumber"] = subNumber;
            ViewData["subName"] = subName;
            ViewData["subSize"] = size;
            ViewData["subDeal"] = deal;
            ViewData["UnitPrice"] = unitPrice2;
            ViewData["dealPrice"] = dealPrice2;
            ViewData["preTaxPrice"] = preTaxPrice2;
            ViewData["tax"] = taxPrice2;
            ViewData["totalPrice"] = totalPrice2;
            ViewData["date"] = date;
            ViewData["totalNumberPrice"] = totalNumberPrice2;

            sandwich.Id = DB.LastId++;
            List<Sub> orders = Session["orders"] as List<Sub>;
            DB.orders.Add(sandwich);
            Session["orders"] = orders;
            

            return View("Receipt");
        }
    }
}
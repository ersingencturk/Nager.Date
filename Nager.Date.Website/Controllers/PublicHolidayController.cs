﻿using Nager.Date.Website.Model;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Nager.Date.Website.Controllers
{
    public class PublicHolidayController : Controller
    {
        public ActionResult Country(string id)
        {
            CountryCode countryCode;
            if (!Enum.TryParse(id, true, out countryCode))
            {
                return View("NotFound");
            }

            ViewBag.Country = id;

            var publicHolidays = DateSystem.GetPublicHoliday(countryCode, DateTime.Now.Year);
            if (publicHolidays?.Count() > 0)
            {
                return View(publicHolidays);
            }

            return View("NotFound");
        }

        public ActionResult CountryJson(string id, int year)
        {
            CountryCode countryCode;
            if (!Enum.TryParse(id, true, out countryCode))
            {
                return Json("Not found");
            }

            var publicHolidays = DateSystem.GetPublicHoliday(countryCode, year);
            if (publicHolidays?.Count() > 0)
            {
                var items = publicHolidays.Select(o => new PublicHoliday(o));
                return Json(items, JsonRequestBehavior.AllowGet);
            }

            return Json("Not found");
        }
    }
}
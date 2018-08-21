using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarClient.Models;
using CarClient.Models.HomeViewModel;

namespace CarClient.Controllers
{
	public class HomeController : Controller
	{
		public async Task<IActionResult> Index()
		{
			List<Company> companies;
			try
			{
				companies = await Utils.Get<List<Company>>("api/Company");
			}
			catch (Exception e)
			{
				TempData["CustomError"] = "Ingen kontakt med servern! CarAPI måste startas innan CarClient kan köras!";
				return View(new HomeViewModel { Companies = new List<Company>()});
			}

			var allCars = await Utils.Get<List<Car>>("api/Car");
            foreach (var car in allCars)
            {
                car.Disabled = false; //Enable updates of Online/Offline
                await Utils.Put<Car>("api/Car/" + car.Id, car);
            }

            foreach (var company in companies)
			{
				var companyCars = allCars.Where(o => o.CompanyId == company.Id).ToList();
				company.Cars = companyCars;
			}
			var homeViewModel = new HomeViewModel { Companies = companies };
			return View(homeViewModel);
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarClient.Models;
using CarClient.Models.CarViewModel;

namespace CarClient.Controllers
{
	public class CarController : Controller
	{
		private readonly SignInManager<ApplicationUser> _signInManager;

		public CarController(SignInManager<ApplicationUser> signInManager)
		{
			_signInManager = signInManager;
		}

		// GET: Car
		public async Task<IActionResult> Index(string id)
		{
			if (!_signInManager.IsSignedIn(User)) return RedirectToAction("Index", "Home");
			var companies = await Utils.Get<List<Company>>("api/Company");

			if (companies.Any() && id == null)
				id = companies[0].Id.ToString();
			var selectList = new List<SelectListItem>
			{
				new SelectListItem
				{
					Text = "Choose company",
					Value = ""
				}
			};
			selectList.AddRange(companies.Select(company => new SelectListItem
			{
				Text = company.Name,
				Value = company.Id.ToString(),
				Selected = company.Id.ToString() == id
			}));
			var cars = new List<Car>();

			if (id != null)
			{
				cars = await Utils.Get<List<Car>>("api/Car");
				var companyId = new Guid(id);
				cars = cars.Where(o => o.CompanyId == companyId).ToList();
			}

			var carListViewModel = new CarListViewModel()
			{
				CompanySelectList = selectList,
				Cars = cars
			};

			ViewBag.CompanyId = id;
			return View(carListViewModel);
		}

		// GET: Car/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			var car = await Utils.Get<Car>("api/Car/" + id);
			var company = await Utils.Get<Company>("api/Company/" + car.CompanyId);
			ViewBag.CompanyName = company.Name;
			return View(car);
		}

		// GET: Car/Create
		public async Task<IActionResult> Create(string id)
		{
			var companyId = new Guid(id);
			var car = new Car
			{
				CompanyId = companyId,
			};
			var company = await Utils.Get<Company>("api/Company/" + companyId);
			ViewBag.CompanyName = company.Name;
			return View(car);
		}

		// POST: Car/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(
			[Bind("CompanyId,VIN,RegNr,Online")] Car car)
		{
			if (!ModelState.IsValid) return View(car);
			car.Id = Guid.NewGuid();
			await Utils.Post<Car>("api/Car/", car);

			return RedirectToAction("Index", new { id = car.CompanyId });
		}

		// GET: Car/Edit/5
		public async Task<IActionResult> Edit(Guid id)
		{
			var car = await Utils.Get<Car>("api/Car/" + id);
			car.Disabled = true; //Prevent updates of Online/Offline while editing
			await Utils.Put<Car>("api/car/" + id, car);
			var company = await Utils.Get<Company>("api/Company/" + car.CompanyId);
			ViewBag.CompanyName = company.Name;
			return View(car);
		}

		// POST: Car/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id, Online")] Car car)
		{
			if (!ModelState.IsValid) return View(car);
			var oldCar = await Utils.Get<Car>("api/Car/" + car.Id);
			oldCar.Online = car.Online;
			oldCar.Disabled = false; //Enable updates of Online/Offline when editing done
			await Utils.Put<Car>("api/Car/" + oldCar.Id, oldCar);

			return RedirectToAction("Index", new { id = oldCar.CompanyId });
		}

		// GET: Car/Delete/5
		public async Task<IActionResult> Delete(Guid id)
		{
			var car = await Utils.Get<Car>("api/Car/" + id);
			return View(car);
		}

		// POST: Car/Delete/5
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var car = await Utils.Get<Car>("api/Car/" + id);
			await Utils.Delete<Car>("api/Car/" + id);
			return RedirectToAction("Index", new { id = car.CompanyId });
		}

		public async Task<bool> RegNrAvailable(string regNr)
		{
			var cars = await Utils.Get<List<Car>>("api/Car");
			return cars.All(c => c.RegNr != regNr);
		}

		public async Task<bool> VinAvailable(string vin)
		{
			var cars = await Utils.Get<List<Car>>("api/Car");
			return cars.All(c => c.VIN != vin);
		}
	}
}
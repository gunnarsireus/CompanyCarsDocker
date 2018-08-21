using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CarClient.Models;
using CarClient.Models.CompanyViewModel;

namespace CarClient.Controllers
{
	public class CompanyController : Controller
	{
		private readonly SignInManager<ApplicationUser> _signInManager;

		public CompanyController(SignInManager<ApplicationUser> signInManager)
		{
			_signInManager = signInManager;
		}


		// GET: Company

		public async Task<IActionResult> Index()
		{
			if (!_signInManager.IsSignedIn(User)) return RedirectToAction("Index", "Home");
			var companies = await Utils.Get<List<Company>>("api/Company");

			foreach (var company in companies)
			{
				var cars = await Utils.Get<List<Car>>("api/Car");
				cars = cars.Where(c => c.CompanyId == company.Id).ToList();
				company.Cars = cars;
			}

			var companyViewModel = new CompanyViewModel { Companies = companies };

			return View(companyViewModel);
		}

		// GET: Company/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			var company = await Utils.Get<Company>("api/Company/" + id);

			return View(company);
		}

		// GET: Company/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Company/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Name,Address,CreationTime")] Company company)
		{
			if (!ModelState.IsValid) return View(company);
			company.Id = Guid.NewGuid();
			await Utils.Post<Company>("api/Company/", company);

			return RedirectToAction(nameof(Index));
		}

		// GET: Company/Edit/5
		public async Task<IActionResult> Edit(Guid? id)
		{
			var company = await Utils.Get<Company>("api/Company/" + id);
			return View(company);
		}

		// POST: Company/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,CreationTime, Name, Address")] Company company)
		{
			if (!ModelState.IsValid) return View(company);
			var oldCompany = await Utils.Get<Company>("api/Company/" + id);

			oldCompany.Name = company.Name;
			oldCompany.Address = company.Address;
			await Utils.Put<Company>("api/Company/" + oldCompany.Id, oldCompany);

			return RedirectToAction(nameof(Index));
		}

		// GET: Company/Delete/5
		public async Task<IActionResult> Delete(Guid? id)
		{
			var company = await Utils.Get<Company>("api/Company/" + id);
			return View(company);
		}

		// POST: Company/Delete/5
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			await Utils.Delete<Company>("api/Company/" + id);
			return RedirectToAction(nameof(Index));
		}

		private async Task<bool> CompanyExists(Guid id)
		{
			var companies = await Utils.Get<List<Company>>("api/Company");
			return companies.Any(e => e.Id == id);
		}
	}
}
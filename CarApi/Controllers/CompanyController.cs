using System;
using System.Collections.Generic;
using CarAPI.Data;
using CarAPI.DAL;
using CarAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarAPI.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {
	    private readonly CarUnitOfWork _unitOfWork;
		public CompanyController(CarApiContext context)
	    {
			_unitOfWork = new CarUnitOfWork(context);
		}
        // GET api/Company
        [HttpGet]
        public IEnumerable<Company> GetCompanies()
        {
	        return _unitOfWork.Companies.GetAll();
        }

        // GET api/Company/5
        [HttpGet("{id}")]
        public Company GetCompany(string id)
        {
	        return _unitOfWork.Companies.Get(new Guid(id));
		}

        // POST api/Company
        [HttpPost]
        public void AddCompany([FromBody] Company company)
        {
	        _unitOfWork.Companies.Add(company);
	        _unitOfWork.Complete();
		}

        // PUT api/Company/5
        [HttpPut("{id}")]
        public void UpdateCompany([FromBody] Company company)
        {
	        _unitOfWork.Companies.Update(company);
			_unitOfWork.Complete();
        }

        // DELETE api/Company/5
        [HttpDelete("{id}")]
        public void DeleteCompany(string id)
        {
	        var account = _unitOfWork.Companies.Get(new Guid(id));
			_unitOfWork.Companies.Remove(account);
	        _unitOfWork.Complete();
		}
    }
}
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CarApi.Data;
using CarApi.DAL;
using CarApi.Models;
using Microsoft.AspNetCore.Cors;

namespace CarApi.Controllers
{
    [Route("api/[controller]")]
    public class CarController : Controller
    {
	    private readonly CarUnitOfWork _unitOfWork;
		public CarController(CarApiContext context)
	    {
			_unitOfWork = new CarUnitOfWork(context);
		}
        // GET api/Car
        [HttpGet]
		public IEnumerable<Car> GetCars()
        {
	        return _unitOfWork.Cars.GetAll();
        }

        // GET api/Car/5
        [HttpGet("{id}")]
		public Car GetCar(string id)
        {
	        return _unitOfWork.Cars.Get(new Guid(id));
		}

        // POST api/Car
        [HttpPost]
		public void AddCar([FromBody] Car car)
        {
	        _unitOfWork.Cars.Add(car);
	        _unitOfWork.Complete();
		}

        // PUT api/Car/5
        [HttpPut("{id}")]
		public void UpdateCar([FromBody] Car car)
        {
	        _unitOfWork.Cars.Update(car);
			_unitOfWork.Complete();
        }

        // DELETE api/Car/5
        [HttpDelete("{id}")]
		public void DeleteCar(string id)
        {
	        var account = _unitOfWork.Cars.Get(new Guid(id));
			_unitOfWork.Cars.Remove(account);
	        _unitOfWork.Complete();
		}
    }
}
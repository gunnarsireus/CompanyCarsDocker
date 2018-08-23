using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using CarApi.Models;
using CarApi.Data;
using CarApi.DAL;

namespace CarAPI.Controllers
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
        [EnableCors("AllowAllOrigins")]
        public IEnumerable<Car> GetCars()
        {
            return _unitOfWork.Cars.GetAll();
        }

        // GET api/Car/5
        [HttpGet("{id}")]
        [EnableCors("AllowAllOrigins")]
        public Car GetCar(string id)
        {
            return _unitOfWork.Cars.Get(new Guid(id));
        }

        // POST api/Car
        [HttpPost]
        [EnableCors("AllowAllOrigins")]
        public void AddCar([FromBody] Car car)
        {
            _unitOfWork.Cars.Add(car);
            _unitOfWork.Complete();
        }

        // PUT api/Car/5
        [HttpPut("{id}")]
        [EnableCors("AllowAllOrigins")]
        public void UpdateCar([FromBody] Car car)
        {
            _unitOfWork.Cars.Update(car);
            _unitOfWork.Complete();
        }

        // DELETE api/Car/5
        [HttpDelete("{id}")]
        [EnableCors("AllowAllOrigins")]
        public void DeleteCar(string id)
        {
            var account = _unitOfWork.Cars.Get(new Guid(id));
            _unitOfWork.Cars.Remove(account);
            _unitOfWork.Complete();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CarAPI.Models;

namespace CarAPI.DAL
{
    public class CarDataAccess
    {
        private readonly DbContextOptionsBuilder<CarApiContext> _optionsBuilder =
            new DbContextOptionsBuilder<CarApiContext>();

        public CarDataAccess()
        {
            _optionsBuilder.UseSqlite("DataSource=App_Data/Car.db");
        }

	    public ICollection<Car> GetCars()
	    {
		    using (var context = new CarApiContext(_optionsBuilder.Options))
		    {
			    return context.Cars.ToList();
		    }
	    }

	    public Car GetCar(Guid id)
	    {
		    using (var context = new CarApiContext(_optionsBuilder.Options))
		    {
			    return context.Cars.SingleOrDefault(o => o.Id == id);
		    }
	    }

	    public void AddCar(Car car)
	    {
		    using (var context = new CarApiContext(_optionsBuilder.Options))
		    {
			    context.Cars.Add(car);
			    context.SaveChanges();
		    }
	    }

	    public void DeleteCar(Guid id)
	    {
		    using (var context = new CarApiContext(_optionsBuilder.Options))
		    {
			    var Car = GetCar(id);
			    context.Cars.Remove(Car);
			    context.SaveChanges();
		    }
	    }

	    public void UpdateCar(Car car)
	    {
		    using (var context = new CarApiContext(_optionsBuilder.Options))
		    {
			    context.Cars.Update(car);
			    context.SaveChanges();
		    }
	    }

		public ICollection<Company> GetCompanies()
        {
            using (var context = new CarApiContext(_optionsBuilder.Options))
            {
                return context.Companies.ToList();
            }
        }

        public Company GetCompany(Guid id)
        {
            using (var context = new CarApiContext(_optionsBuilder.Options))
            {
                return context.Companies.SingleOrDefault(o => o.Id == id);
            }
        }

        public void AddCompany(Company company)
        {
            using (var context = new CarApiContext(_optionsBuilder.Options))
            {
                context.Companies.Add(company);
                context.SaveChanges();
            }
        }

        public void DeleteCompany(Guid id)
        {
            using (var context = new CarApiContext(_optionsBuilder.Options))
            {
                var cars = GetCars().Where(c => c.CompanyId == id);
                foreach (var car in cars)
                {
                    context.Cars.Remove(car);
                }

                var company = GetCompany(id);
                context.Companies.Remove(company);
                context.SaveChanges();
            }
        }

        public void UpdateCompany(Company company)
        {
            using (var context = new CarApiContext(_optionsBuilder.Options))
            {
                context.Companies.Update(company);
                context.SaveChanges();
            }
        }
    }
}
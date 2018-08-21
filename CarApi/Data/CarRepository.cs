using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarAPI.DAL;
using CarAPI.Models;

namespace CarAPI.Data
{
	public class CarRepository : Repository<Car>, ICarRepository
	{
		public CarRepository(CarApiContext context) : base(context)
		{
		}

		public CarApiContext CarApiContext => Context as CarApiContext;

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarApi.DAL;
using CarApi.Models;

namespace CarApi.Data
{
	public class CarRepository : Repository<Car>, ICarRepository
	{
		public CarRepository(CarApiContext context) : base(context)
		{
		}

		public CarApiContext CarApiContext => Context as CarApiContext;

	}
}

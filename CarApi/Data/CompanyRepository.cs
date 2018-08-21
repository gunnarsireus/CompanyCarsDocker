using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CarApi.Data;
using CarApi.DAL;
using CarApi.Models;
using Microsoft.EntityFrameworkCore;


namespace CarApi.Data
{
	public class CompanyRepository : Repository<Company>, ICompanyRepository
	{
		public CompanyRepository(CarApiContext context) : base(context)
		{
		}

		public CarApiContext CarApiContext => Context as CarApiContext;

	}
}

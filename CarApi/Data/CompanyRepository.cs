using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CarAPI.Data;
using CarAPI.DAL;
using CarAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace CarAPI.Data
{
	public class CompanyRepository : Repository<Company>, ICompanyRepository
	{
		public CompanyRepository(CarApiContext context) : base(context)
		{
		}

		public CarApiContext CarApiContext => Context as CarApiContext;

	}
}

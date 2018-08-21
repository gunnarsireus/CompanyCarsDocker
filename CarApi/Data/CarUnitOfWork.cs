using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CarAPI.Data;
using CarAPI.DAL;

namespace CarAPI.Data
{
    public class CarUnitOfWork:ICarUnitOfWork
    {
	    private readonly CarApiContext _context;

	    public CarUnitOfWork(CarApiContext context)
	    {
		    _context = context;
		    Cars = new CarRepository(_context);
		    Companies = new CompanyRepository(_context);
		}

	    public void Dispose()
	    {
		   _context.Dispose();
	    }

	    public ICarRepository Cars { get; private set; }
	    public ICompanyRepository Companies { get; private set; }
		public int Complete()
		{
			return _context.SaveChanges();
		}
    }
}

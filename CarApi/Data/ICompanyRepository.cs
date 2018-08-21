using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarAPI.Models;

namespace CarAPI.Data
{
    public interface ICompanyRepository:IRepository<Company>
    {
	    //Todo, if more advanced filtering is needed
	}
}

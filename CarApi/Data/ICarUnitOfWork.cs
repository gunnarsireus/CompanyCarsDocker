using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarApi.Data
{
    interface ICarUnitOfWork: IDisposable
    {
	    ICarRepository Cars { get; }
		int Complete();
    }
}

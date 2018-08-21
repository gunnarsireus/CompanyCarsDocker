using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarClient.Services
{
    public interface IAspNetDbLocation
    {
	    Task<string> GetAspNetDbAsync();
    }
}

using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace CarApi.Controllers
{
    [Route("api/[controller]")]
	public class AspNetDbController : Controller
	{
		// GET api/Car
		[HttpGet]
		public string GetAspNetDb()
		{
            return Directory.GetCurrentDirectory() + "/App_Data/AspNet.db";
		}
	}
}
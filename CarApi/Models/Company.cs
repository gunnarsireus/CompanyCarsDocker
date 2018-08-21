using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CarAPI.Models
{
	public class Company
	{
		public Company()
		{
			CreationTime = DateTime.Now.ToString(new CultureInfo("se-SE"));
		}
		public Guid Id { get; set; }

		public string CreationTime { get; set; }
		public string Name { get; set; }

		public string Address { get; set; }

		public ICollection<Car> Cars { get; set; }
	}
}

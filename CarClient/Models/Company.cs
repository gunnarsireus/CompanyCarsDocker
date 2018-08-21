using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CarClient.Models
{
	public class Company
	{
		public Company()
		{
			CreationTime = DateTime.Now.ToString(new CultureInfo("en-US"));
		}
		public Guid Id { get; set; }

		[Display(Name = "Created date")]
		public string CreationTime { get; set; }
		[Display(Name = "Name")]
		public string Name { get; set; }

		[Display(Name = "Address")]
		public string Address { get; set; }

		public ICollection<Car> Cars { get; set; }
	}
}

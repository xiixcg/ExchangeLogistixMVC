using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExchangeLogistixMVC.Models
{
	public class Trailer
	{
		public int Id { get; set; }
		[Required]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }
		[Required]
		[Display(Name = "Chasis Size")]
		public int ChasisSize { get; set; }
		[Required]
		[Display(Name = "Load Size")]
		public int LoadSize { get; set; }
		[Required]
		[Display(Name = "Next Load's Location")]
		public string NextLoadLocation { get; set; }
		[Required]
		[Display(Name ="Current Load's Destination")]
		public string CurrentLoadDestination { get; set; }
		[Required]
		[Display(Name = "Current Load Drop-off Date/Time")]
		public DateTime CurrentLoadETA { get; set; }
	}
}
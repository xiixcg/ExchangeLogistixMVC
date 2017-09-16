using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExchangeLogistixMVC.Models
{
	public enum ChasisSize
	{
		[Description("20'")]
		TwentyFeet,
		[Description ("40'")]
		FourtyFeet,
		[Description ("45'")]
		FourtyFiveFeet
	}

	public enum Location
	{
		[Description("Los Angeles")]
		LosAngeles,
		[Description("Las Vegas")]
		LasVegas,
		[Description("Bakersfield")]
		Bakersield,
		[Description("San Francisco")]
		SanFrancisco
	}

	public class Trailer 
	{
		public int TrailerID { get; set; }
		public string ApplicationUserID { get; set; }
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }
		[Required]
		[Display(Name = "Chasis Size")]
		public ChasisSize ChasisSize { get; set; }
		[Required]
		[Display(Name = "Load Size")]
		public ChasisSize LoadSize { get; set; }
		[Required]
		[Display(Name = "Next Load's Location")]
		public Location NextLoadLocation { get; set; }
		[Required]
		[Display(Name ="Current Load's Destination")]
		public Location CurrentLoadDestination { get; set; }
		[Required]
		[Display(Name = "Current Load Drop-off Date/Time")]
		public DateTime CurrentLoadETA { get; set; }
		public DateTime CreatedDateTime { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; }
	}
}
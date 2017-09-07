using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ExchangeLogistixMVC.Models
{
	public class TrailerDBContext : DbContext
	{
		public TrailerDBContext() : base("TrailersData")
		{

		}
		public DbSet<Trailer> Trailer { get; set; }
	}
}
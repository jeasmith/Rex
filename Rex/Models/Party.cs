using System.Collections.Generic;
using System.Linq;
using System;

namespace Rex.Domain.Models
{
	public class Party
	{
		public Guid Id { get; set; }
		public IEnumerable<StakeHolder> StakeHolders { get; set; }

		public decimal GetBalance()
		{
			return StakeHolders.Sum(s => s.GetBalance());
		}
	}
}

using System.Collections.Generic;
using System.Linq;
using System;

namespace Rex.Domain.Models
{
	public class Party
	{
		public Guid Id { get; set; }
		public IEnumerable<Stakeholder> Stakeholders { get; set; }

		public decimal GetBalance()
		{
			return Stakeholders.Sum(s => s.GetBalance());
		}
	}
}

using System.Collections.Generic;
using System.Linq;

namespace Rex.Domain.Models
{
	public class Party
	{
		public IEnumerable<Stakeholder> Stakeholders { get; set; }

		public decimal GetBalance()
		{
			return Stakeholders.Sum(s => s.GetBalance());
		}
	}
}

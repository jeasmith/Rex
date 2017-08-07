using System.Collections.Generic;
using System.Linq;

namespace Rex.Domain.Models
{
	public class StakeHolder
	{
		public Contact Contact { get; set; }
		public IEnumerable<Expense> Expenses { get; set; }
		public decimal Debt { get; set; }

		public decimal GetBalance()
		{
			return Expenses.Sum(b => b.Cost);
		}
	}
}

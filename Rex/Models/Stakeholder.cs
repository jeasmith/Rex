using System.Collections.Generic;
using System.Linq;

namespace Rex.Domain.Models
{
	public class Stakeholder
	{
		public Contact Contact { get; set; }
		public IEnumerable<Expense> Expenses { get; set; }
		public decimal Debt { get; set; }
		public IEnumerable<Payment> Payments { get; set; }
		public decimal CachedReducedDebt { get; set; }

		public decimal GetBalance()
		{
			return Expenses.Sum(b => b.Cost);
		}
	}
}

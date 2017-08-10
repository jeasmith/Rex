using System;
using System.Collections.Generic;
using System.Linq;
using Rex.Domain.Models;
using Rex.Domain.Extensions;

namespace Rex.Domain.Services.Settlement
{
	public class SettlementService
	{
		public void CalculateDebts(Party party)
		{
			party.Stakeholders.ForEach(s =>
			{
				s.Debt = (party.GetBalance() / party.Stakeholders.Count()) - s.GetBalance();
			});
		}

		public void CalculatePaymentMappings(Party party)
		{
			party.Stakeholders.ForEach(s => s.CachedReducedDebt = s.Debt);

			while (party.Stakeholders.Any(s => s.CachedReducedDebt != 0M))
			{
				Stakeholder highestDebtStakeholder = GetHighestDebtStakeholder(party);
				Console.WriteLine($"Highest debt stakeholder: {highestDebtStakeholder.Contact.FirstName} {highestDebtStakeholder.Contact.LastName}");

				Stakeholder lowestDebtStakeholder = GetLowestDebtStakeholder(party);
				Console.WriteLine($"Lowest debt stakeholder: {lowestDebtStakeholder.Contact.FirstName} {lowestDebtStakeholder.Contact.LastName}");

				if (highestDebtStakeholder.Payments == null)
				{
					highestDebtStakeholder.Payments = new List<Payment>();
				}

				if (Math.Abs(highestDebtStakeholder.CachedReducedDebt) <= Math.Abs(lowestDebtStakeholder.CachedReducedDebt))
				{
					highestDebtStakeholder.Payments = highestDebtStakeholder.Payments.AddItem(new Payment
					{
						Ammount = highestDebtStakeholder.CachedReducedDebt,
						Payer = highestDebtStakeholder.Contact,
						Payee = lowestDebtStakeholder.Contact
					});

					lowestDebtStakeholder.CachedReducedDebt += highestDebtStakeholder.CachedReducedDebt;

					highestDebtStakeholder.CachedReducedDebt -= highestDebtStakeholder.CachedReducedDebt;
				}

				else if (Math.Abs(highestDebtStakeholder.CachedReducedDebt) > Math.Abs(lowestDebtStakeholder.CachedReducedDebt))
				{
					highestDebtStakeholder.Payments = highestDebtStakeholder.Payments.AddItem(new Payment
					{
						Ammount = Math.Abs(lowestDebtStakeholder.CachedReducedDebt),
						Payer = highestDebtStakeholder.Contact,
						Payee = lowestDebtStakeholder.Contact
					});

					highestDebtStakeholder.CachedReducedDebt += lowestDebtStakeholder.CachedReducedDebt;

					lowestDebtStakeholder.CachedReducedDebt -= lowestDebtStakeholder.CachedReducedDebt;
				}

				party.Stakeholders.ForEach(s => Console.WriteLine($"{s.Contact.FirstName} {s.Contact.LastName} - {s.CachedReducedDebt}"));
			}

			PrintMappings(party);
		}

		private Stakeholder GetHighestDebtStakeholder(Party party)
		{
			return party
				.Stakeholders
				.Aggregate
				(
					(currentMax, s) => (currentMax == null || (s.CachedReducedDebt) > currentMax.CachedReducedDebt ? s : currentMax)
				);
		}

		private Stakeholder GetLowestDebtStakeholder(Party party)
		{
			return party
				.Stakeholders
				.Aggregate
				(
					(currentMin, s) => (currentMin == null || (s.CachedReducedDebt) < currentMin.CachedReducedDebt ? s : currentMin)
				);
		}

		private void PrintMappings(Party party)
		{
			party
				.Stakeholders
				.ForEach(s =>
				{
					if (s.Payments != null)
					{
						s.Payments
					 	.ForEach(p =>
					 	{
						 	Console.WriteLine($"{p.Payer.FirstName} {p.Payer.LastName} => {p.Payee.FirstName} {p.Payee.LastName} - {p.Ammount}");
					 	});
					}
				}
			);
		}
	}
}

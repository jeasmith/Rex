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
			party.Stakeholders.ForEach(s => s.CachedCalculatedDebt = s.Debt);

			while (party.Stakeholders.Any(s => s.CachedCalculatedDebt != 0M))
			{
				Stakeholder highestDebtStakeholder = GetHighestDebtStakeholder(party);

				Stakeholder lowestDebtStakeholder = GetLowestDebtStakeholder(party);

				if (highestDebtStakeholder.Payments == null)
				{
					highestDebtStakeholder.Payments = new List<Payment>();
				}

				if (Math.Abs(highestDebtStakeholder.CachedCalculatedDebt) <= Math.Abs(lowestDebtStakeholder.CachedCalculatedDebt))
				{
					SettleHighestDebtStakeholder(highestDebtStakeholder, lowestDebtStakeholder);
				}

				else if (Math.Abs(highestDebtStakeholder.CachedCalculatedDebt) > Math.Abs(lowestDebtStakeholder.CachedCalculatedDebt))
				{
					SettleLowestDebtStakeholder(highestDebtStakeholder, lowestDebtStakeholder);
				}

				party.Stakeholders.ForEach(s => Console.WriteLine($"{s.Contact.FirstName} {s.Contact.LastName} - {s.CachedCalculatedDebt}"));
			}

			PrintMappings(party);
		}

		private Stakeholder GetHighestDebtStakeholder(Party party)
		{
			Stakeholder highestDebtStakeholder = party
				.Stakeholders
				.Aggregate
				(
					(currentMax, s) => (currentMax == null || (s.CachedCalculatedDebt) > currentMax.CachedCalculatedDebt ? s : currentMax)
				);

			Console.WriteLine($"Highest debt stakeholder: {highestDebtStakeholder.Contact.FirstName} {highestDebtStakeholder.Contact.LastName}");

			return highestDebtStakeholder;
		}

		private Stakeholder GetLowestDebtStakeholder(Party party)
		{
			Stakeholder lowestDebtStakeholder = party
				.Stakeholders
				.Aggregate
				(
					(currentMin, s) => (currentMin == null || (s.CachedCalculatedDebt) < currentMin.CachedCalculatedDebt ? s : currentMin)
				);

			Console.WriteLine($"Lowest debt stakeholder: {lowestDebtStakeholder.Contact.FirstName} {lowestDebtStakeholder.Contact.LastName}");

			return lowestDebtStakeholder;
		}

		private void SettleHighestDebtStakeholder(Stakeholder highestDebtStakeholder, Stakeholder lowestDebtStakeholder)
		{
			highestDebtStakeholder.Payments = highestDebtStakeholder.Payments.AddItem(new Payment
			{
				Ammount = highestDebtStakeholder.CachedCalculatedDebt,
				Payer = highestDebtStakeholder.Contact,
				Payee = lowestDebtStakeholder.Contact
			});

			lowestDebtStakeholder.CachedCalculatedDebt += highestDebtStakeholder.CachedCalculatedDebt;

			highestDebtStakeholder.CachedCalculatedDebt -= highestDebtStakeholder.CachedCalculatedDebt;
		}

		private void SettleLowestDebtStakeholder(Stakeholder highestDebtStakeholder, Stakeholder lowestDebtStakeholder)
		{
			highestDebtStakeholder.Payments = highestDebtStakeholder.Payments.AddItem(new Payment
			{
				Ammount = Math.Abs(lowestDebtStakeholder.CachedCalculatedDebt),
				Payer = highestDebtStakeholder.Contact,
				Payee = lowestDebtStakeholder.Contact
			});

			highestDebtStakeholder.CachedCalculatedDebt += lowestDebtStakeholder.CachedCalculatedDebt;

			lowestDebtStakeholder.CachedCalculatedDebt -= lowestDebtStakeholder.CachedCalculatedDebt;
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

using System;
using System.Collections.Generic;
using System.Linq;
using Rex.Domain.Extensions;
using Rex.Domain.Models;
using Rex.Domain.Services.Settlement;

namespace Rex.Domain
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Party party = PopulateParty();

			SettlementService settlementService = new SettlementService();

			settlementService.CalculateDebts(party);

			Console.WriteLine($"Total Party Balance: {party.GetBalance()}");
			Console.WriteLine($"Divised Party Balance: {party.GetBalance() / party.Stakeholders.Count()}");

			party.Stakeholders.ForEach(s =>
			{
				Console.WriteLine($"{s.Contact.FirstName} {s.Contact.LastName} - Debt: {s.Debt}");
			});

			settlementService.CalculatePaymentMappings(party);
		}

		static Party PopulateParty()
		{
			return new Party()
			{
				Stakeholders = new List<Stakeholder>
				{
					new Stakeholder()
					{
						Contact = new Contact()
						{
							FirstName = "Jamie",
							LastName = "Smith"
						},
						Expenses = new List<Expense>
						{
							new Expense()
							{
								Cost = 20.00M
							},
							new Expense()
							{
								Cost = 4.06M
							}
						}
					},

					new Stakeholder()
					{
						Contact = new Contact()
						{
							FirstName = "Peter",
							LastName = "Alexander"
						},
						Expenses = new List<Expense>
						{
							new Expense()
							{
								Cost = 15.60M
							},
							new Expense()
							{
								Cost = 11.56M
							}
						}
					},

					new Stakeholder()
					{
						Contact = new Contact()
						{
							FirstName = "Chris",
							LastName = "Smith"
						},
						Expenses = new List<Expense>
						{
							new Expense()
							{
								Cost = 50.03M
							},
							new Expense()
							{
								Cost = 14.04M
							}
						}
					},
					new Stakeholder()
					{
						Contact = new Contact()
						{
							FirstName = "Chris",
							LastName = "Walsh"
						},
						Expenses = new List<Expense>
						{
							new Expense()
							{
								Cost = 70.00M
							},
							new Expense()
							{
								Cost = 4.06M
							}
						}
					},

					new Stakeholder()
					{
						Contact = new Contact()
						{
							FirstName = "Luke",
							LastName = "Baker"
						},
						Expenses = new List<Expense>
						{
							new Expense()
							{
								Cost = 43.70M
							},
							new Expense()
							{
								Cost = 4.66M
							}
						}
					}
				}
			};
		}
	}
}

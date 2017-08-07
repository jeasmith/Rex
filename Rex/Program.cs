using System;
using System.Collections.Generic;
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

			party.StakeHolders.ForEach(s =>
			{
				Console.WriteLine($"{s.Contact.FirstName} {s.Contact.LastName} - Debt: {s.Debt}");
			});
		}

		static Party PopulateParty()
		{
			Party alexanderParty = new Party()
			{
				Id = new Guid("{5F896357-3213-4B1B-8F0D-492CEDB90381}"),
				StakeHolders = new List<StakeHolder>
				{
					new StakeHolder()
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

					new StakeHolder()
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

					new StakeHolder()
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
					new StakeHolder()
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

					new StakeHolder()
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

			return alexanderParty;
		}
	}
}

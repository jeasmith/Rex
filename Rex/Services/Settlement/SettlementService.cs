using Rex.Domain.Models;
namespace Rex.Domain.Settlement
{
	public class SettlementService
	{
		public void CalculateDebts(Party party)
		{
			party.StakeHolders.ForEach(s =>
			{
				s.Debt = party.GetBalance() - s.GetBalance();
			});
		}
	}
}

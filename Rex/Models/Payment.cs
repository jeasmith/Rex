namespace Rex.Domain.Models
{
	public class Payment
	{
		public decimal Ammount { get; set; }
		public Contact Payer { get; set; }
		public Contact Payee { get; set; }
	}
}

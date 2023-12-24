namespace Health_Insurance.Models
{
	public class ReportJoinTable
	{
        public UserSubscription subscribe { get; set; }
        public SahtakUser user { get; set; }
        public SahtakSubscription subs { get; set; }
        public SahtakPayment pay { get; set; }
    }
}

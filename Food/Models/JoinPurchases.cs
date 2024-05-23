namespace Food.Models
{
    public class JoinPurchases
    {
        public UserLogin? UserLogin { get; set; }
        public Category? Category { get; set; }
        public Purchase? Purchase { get; set; }
        public Recipe? Recipe { get; set; }
    }
}

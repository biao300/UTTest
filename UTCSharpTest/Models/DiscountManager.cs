namespace UTCSharpTest.Models
{
    // Question 5
    public static class DiscountManager
    {
        public static double ApplyDiscountPercentage(double price, double percentage)
        {
            if (percentage >= 100 || percentage < 0)
            {
                Console.WriteLine("percentage out of range, can't apply!");
                return Math.Round(price, 2);
            }
            else
            {
                return Math.Round(price * (100.0 - percentage) / 100.0, 2);
            }
        }

        public static double ApplyDiscountFixed(double price, double amount)
        {
            if (amount < 0 || (price - amount) <= 0)
            {
                Console.WriteLine("invalid discount amount, can't apply!");
                return Math.Round(price, 2);
            }
            else
            {
                return Math.Round(price - amount);
            }
        }
    }
}

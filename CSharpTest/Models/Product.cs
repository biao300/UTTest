﻿namespace UTCSharpTest.Models
{
    // Question 1
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public Product() {
            Name = string.Empty;
        }
    }
}

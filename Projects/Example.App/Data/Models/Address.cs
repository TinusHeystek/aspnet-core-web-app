﻿namespace Example.App.Data.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressInfo { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public int ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
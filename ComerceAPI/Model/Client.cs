﻿namespace ComerceAPI.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CPF { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}

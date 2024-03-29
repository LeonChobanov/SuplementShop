﻿namespace SuplementShop.Application.Requests
{
    using System.Collections.Generic;

    public class CreateProductRequest
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public List<string> Images { get; set; }
    }
}

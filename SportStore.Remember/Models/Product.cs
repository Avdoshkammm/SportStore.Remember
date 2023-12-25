using System;
using System.Collections.Generic;

namespace SportStore.Remember.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Cost { get; set; }

    public string Decription { get; set; } = null!;

    //public string currentProduct { get; set; }
}

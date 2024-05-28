using System.Collections.Generic;

namespace CustomMediator.Api.Models
{
    public static class ApplicationDbContext
    {
        public static List<Product> ProductList { get; set; } = new();
    }
}

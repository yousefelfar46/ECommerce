using ECommerce.Domain.Entities.Comon;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities.Products
{
    public class ProductBrand : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
    }
}

using System;
using System.Collections.Generic;

namespace Pharma.Model
{
    public class OrderResponseDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public IEnumerable<Order> Items { get; set; } = Array.Empty<Order>();
    }
}

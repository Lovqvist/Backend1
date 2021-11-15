using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend1_2.Model
{
    public class GetOrder
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; }
        public List<GetOrderLine> OrderLine { get; set; }
    }
}

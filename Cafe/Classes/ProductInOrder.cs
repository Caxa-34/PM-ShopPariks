using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Classes
{
    public class ProductInOrder : Product
    {
        public int Count { get; set; }
        public double Costing { get; set; }
        public double Sum { get; set; }
    }
}

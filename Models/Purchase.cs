using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace final_BS.Data.Models
{
    public class Purchase
    {
        public int Id { set; get; }
        public int BuyerId { set; get; }
        public int ProductId { set; get; }
        public DateTime Date { set; get; }
    }
}

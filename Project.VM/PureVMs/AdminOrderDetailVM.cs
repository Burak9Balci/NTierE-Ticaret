using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.PureVMs
{
    public class AdminOrderDetailVM
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DataStatus Status { get; set; }
        //Product Tarafı
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImagePath { get; set; }
        public short Amount { get; set; }
        public string CategoryName { get; set; }
        public string SupplierCompanyName { get; set; }

        //Order Tarafı
        public string ShippingAddress { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserName { get; set; }

    }
}

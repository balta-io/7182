using System;

namespace Store.Models {
    public class Order {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public int[] Products { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Total { get; set; }
    }
}
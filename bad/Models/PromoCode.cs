using System;

namespace Store.Models {
    public class PromoCode {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal Value { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
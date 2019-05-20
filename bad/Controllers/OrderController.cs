using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Store.Models;

namespace Store.Controllers
{
    public class OrderController : Controller
    {
        [Route("v1/orders")]
        [HttpPost]
        public async Task<string> Place(string customerId, string zipCode, string promoCode, int[] products)
        {
            // #1 - Recupera o cliente
            Customer customer = null;
            using (var conn = new SqlConnection("CONN_STRING"))
            {
                customer = conn.Query<Customer>
                    ("SELECT * FROM CUSTOMER WHERE ID=" + customerId)
                    .FirstOrDefault();
            }

            // #2 - Calcula o frete
            decimal deliveryFee = 0;
            var request = new HttpRequestMessage(HttpMethod.Get, "URL/" + zipCode);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            using (HttpClient client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    deliveryFee = await response.Content.ReadAsAsync<decimal>();
                }
                else
                {
                    // Caso não consiga obter a taxa de entrega o valor padrão é 5
                    deliveryFee = 5;
                }
            }

            // #3 - Calcula o total dos produtos
            decimal subTotal = 0;
            for (int p = 0; p < products.Length; p++)
            {
                var product = new Product();
                using (var conn = new SqlConnection("CONN_STRING"))
                {
                    product = conn.Query<Product>
                        ("SELECT * FROM PRODUCT WHERE ID=" + products[p])
                        .FirstOrDefault();
                }
                subTotal += product.Price;
            }

            // #4 - Aplica o cupom de desconto
            decimal discount = 0;
            using (var conn = new SqlConnection("CONN_STRING"))
            {

                var promo = conn.Query<PromoCode>
                    ("SELECT * FROM PROMO_CODES WHERE CODE=" + promoCode)
                    .FirstOrDefault();
                if (promo.ExpireDate > DateTime.Now)
                {
                    discount = promo.Value;
                }
            }

            // #5 - Gera o pedido
            var order = new Order();
            order.Code = Guid.NewGuid().ToString().ToUpper().Substring(0, 8);
            order.Date = DateTime.Now;
            order.DeliveryFee = deliveryFee;
            order.Discount = discount;
            order.Products = products;
            order.SubTotal = subTotal;

            // #6 - Calcula o total
            order.Total = subTotal - discount + deliveryFee;

            // #7 - Retorna
            return $"Pedido {order.Code} gerado com sucesso!";
        }
    }
}
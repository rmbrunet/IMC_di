using IMC.Domain;
using System.Text.Json.Serialization;

namespace IMC.TaxJarTaxCalculator.Models {
    public class TaxJarTax {
        [JsonPropertyName("amount_to_collect")]
        public decimal AmountToCollect { get; set; }
        [JsonPropertyName("breakdown")]
        public TaxJarTaxBreakdown Breakdown { get; set; }
        [JsonPropertyName("freight_taxable")]
        public bool FreightTaxable { get; set; }
        [JsonPropertyName("has_nexus")]
        public bool HasNexus { get; set; }
        [JsonPropertyName("order_total_amount")]
        public decimal OrderTotalAmount { get; set; }
        [JsonPropertyName("rate")]
        public decimal Rate { get; set; }
        [JsonPropertyName("shipping")]
        public decimal Shipping { get; set; }
        [JsonPropertyName("tax_source")]
        public string TaxSource { get; set; }
        [JsonPropertyName("taxable_amount")]
        public decimal TaxableAmount { get; set; }
        public TaxJarJurisdictions Jurisdictions { get; set; }

        public OrderTax MapToOrderTax() {
            return new() {
                AmountToCollect = AmountToCollect,
                FreightTaxable = FreightTaxable,
                Rate = Rate,
                OrderTotalAmount = OrderTotalAmount,
                Shipping = Shipping,
                TaxableAmount = TaxableAmount
            };
        }
    }
}

using Newtonsoft.Json;

namespace CodingTest
{
    public class Fee
    {
        [JsonProperty("minAmount")]
        public double MinAmount { get; set; }
        [JsonProperty("maxAmount")]
        public double MaxAmount { get; set; }
        [JsonProperty("feeAmount")]
        public double FeeAmount { get; set; }


    }


}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTest
{
    public class Data
    {
        [JsonProperty("fees")]
        public List<Fee> Fees { get; set; }
    }
}

using Newtonsoft.Json;

namespace Employee_Management_System.Entity
{
    public class Address_
    {
        [JsonProperty(PropertyName = "addressLine1", NullValueHandling = NullValueHandling.Ignore)]
        public string AddressLine1 { get; set; }

        [JsonProperty(PropertyName = "addressLine2", NullValueHandling = NullValueHandling.Ignore)]
        public string AddressLine2 { get; set; }

        [JsonProperty(PropertyName = "city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty(PropertyName = "state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty(PropertyName = "zipcode", NullValueHandling = NullValueHandling.Ignore)]
        public string Zipcode { get; set; }

        [JsonProperty(PropertyName = "country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }
    }
}

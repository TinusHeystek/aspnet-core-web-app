using Newtonsoft.Json;

namespace Example.App.Shared.Models.View
{
    public class FakeName
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public decimal Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public decimal Longitude { get; set; }

        [JsonProperty(PropertyName = "birth_data")]
        public string DateOfBirthUtc { get; set; }

        [JsonProperty(PropertyName = "phone_h")]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "company")]
        public string Company { get; set; }

        [JsonProperty(PropertyName = "height")]
        public decimal Height { get; set; }

        [JsonProperty(PropertyName = "weight")]
        public decimal Weight { get; set; }
        [JsonProperty(PropertyName = "eye")]
        public string EyeColor { get; set; }

        [JsonProperty(PropertyName = "sport")]
        public string Sport { get; set; }

        [JsonProperty(PropertyName = "pict")]
        public string Pict { get; set; }
    }
}

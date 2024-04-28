using Hackaton.Data.Enums;

namespace Hackaton.Models.Advertisement
{
    public class AdvertisementData
    {
        public int Id { get; set; }

        public string UserId { get; set; } = "Helper";
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }
}

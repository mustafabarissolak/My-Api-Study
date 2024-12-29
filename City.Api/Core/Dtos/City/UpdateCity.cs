namespace City.Api.Core.Dtos.City
{
    public class UpdateCity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Populasyon { get; set; }
        public RgbCity Class { get; set; }
    }
}

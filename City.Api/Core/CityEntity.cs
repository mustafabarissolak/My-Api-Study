namespace City.Api.Core
{
    public class CityEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Populasyon { get; set; }
        public RgbCity Class { get; set; } = RgbCity.Anadolu;

        public UserEntity? User { get; set; }
    }
}

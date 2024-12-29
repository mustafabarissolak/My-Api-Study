using City.Api.Core;
using City.Api.Core.Dtos.City;

namespace City.Api.Services.CityService
{
    public interface ICityService
    {
        Task<ServiceResponse<List<GetCity>>> GetAllCities();
        Task<ServiceResponse<GetCity>> GetByIdCity(int id);
        Task<ServiceResponse<List<GetCity>>> AddCity(AddCity newCity);
        Task<ServiceResponse<GetCity>> UpdateCity(UpdateCity updateCity);
        Task<ServiceResponse<List<GetCity>>> DeleteCity(int id);
    }
}

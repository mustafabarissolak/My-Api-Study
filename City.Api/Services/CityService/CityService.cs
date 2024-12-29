using AutoMapper;
using City.Api.Core;
using City.Api.Core.Dtos.City;
using City.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace City.Api.Services.CityService
{


    public class CityService : ICityService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CityService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetCity>>> AddCity(AddCity newCity)
        {
            var serviceResponse = new ServiceResponse<List<GetCity>>();

            var city = _mapper.Map<CityEntity>(newCity);
            city.User = await _context.UserEntities.FirstOrDefaultAsync(x => x.Id == GetUserId());
            _context.CityEntities.Add(city);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.CityEntities
                .Where(x => x.User.Id == GetUserId())
                .Select(x => _mapper.Map<GetCity>(x))
                .ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCity>>> GetAllCities()
        {
            var response = new ServiceResponse<List<GetCity>>();
            var dbCityEntities = await _context.CityEntities
                .Where(x => x.User.Id == GetUserId())
                .ToListAsync();
            response.Data = dbCityEntities.Select(x => _mapper.Map<GetCity>(x)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetCity>> GetByIdCity(int id)
        {
            var serviceResponse = new ServiceResponse<GetCity>();
            var dbCity = await _context.CityEntities.FirstOrDefaultAsync(x => x.Id == id && x.User.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetCity>(dbCity);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCity>> UpdateCity(UpdateCity updateCity)
        {
            ServiceResponse<GetCity> serviceResponse = new ServiceResponse<GetCity>();
            try
            {
                var city = await _context.CityEntities
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.Id == updateCity.Id);

                if (city.User.Id == GetUserId())
                {
                    city.Name = updateCity.Name;
                    city.Populasyon = updateCity.Populasyon;
                    city.Class = updateCity.Class;
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetCity>(city);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Şehir bulunamadı";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetCity>>> DeleteCity(int id)
        {
            ServiceResponse<List<GetCity>> serviceResponse = new ServiceResponse<List<GetCity>>();
            try
            {
                var city = await _context.CityEntities.FirstOrDefaultAsync(x => x.Id == id && x.User.Id == GetUserId());
                if (city != null)
                {
                    _context.CityEntities.Remove(city);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _context.CityEntities
                        .Where(x => x.User.Id == GetUserId())
                        .Select(x => _mapper.Map<GetCity>(x)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Şehir bulunamadı";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

    }

}
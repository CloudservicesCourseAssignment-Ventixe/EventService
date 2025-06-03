using Business.Models;

namespace Business.Interfaces;

public interface IEventAddressService
{
    Task<ServiceResponse> Create(string arena, string city, string state);
    Task<ServiceResponse<EventAddress?>> GetOneByIdAsync(int id);
    Task<ServiceResponse<EventAddress?>> GetOneByNameAsync(string arena);
}

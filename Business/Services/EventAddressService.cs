using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;
public class EventAddressService(IEventAddressRepository eventAddressRepository) : IEventAddressService
{
    private readonly IEventAddressRepository _eventAddressRepository = eventAddressRepository;


    public async Task<ServiceResponse> Create(string arena, string city, string state)
    {
        if (string.IsNullOrWhiteSpace(arena))
            return new ServiceResponse { Succeeded = false, Error = "Arena is required" };

        var exists = await _eventAddressRepository.ExistsAsync(e => e.Arena == arena);
        if (exists.Succeeded)
            return new ServiceResponse { Succeeded = false, Error = "Event address already exists" };

        var entity = new EventAddressEntity
        {
            Arena = arena,
            City = city,
            State = state
        };

        var result = await _eventAddressRepository.AddAsync(entity);
        return result.Succeeded
            ? new ServiceResponse { Succeeded = true }
            : new ServiceResponse { Succeeded = false, Error = result.Error ?? "Failed to add event address" };
    }

    public async Task<ServiceResponse<EventAddress?>> GetOneByIdAsync(int id)
    {
        var result = await _eventAddressRepository.GetOneAsync(x => x.Id == id);
        if (!result.Succeeded || result.Result == null)
            return new ServiceResponse<EventAddress?> { Succeeded = false, Error = "Event address not found" };
        var eventAddressEntity = result.Result;
        var currentEventAddress = new EventAddress
        {
            Id = eventAddressEntity.Id,
            Arena = eventAddressEntity.Arena,
            City = eventAddressEntity.City,
            State = eventAddressEntity.State
        };
        return new ServiceResponse<EventAddress?> { Succeeded = true, Result = currentEventAddress };
    }

    public async Task<ServiceResponse<EventAddress?>> GetOneByNameAsync(string arena)
    {
        var result = await _eventAddressRepository.GetOneAsync(x => x.Arena == arena);
        if (!result.Succeeded || result.Result == null)
            return new ServiceResponse<EventAddress?> { Succeeded = false, Error = "Event address not found" };
        var eventAddressEntity = result.Result;
        var currentEventAddress = new EventAddress
        {
            Id = eventAddressEntity.Id,
            Arena = eventAddressEntity.Arena,
            City = eventAddressEntity.City,
            State = eventAddressEntity.State
        };
        return new ServiceResponse<EventAddress?> { Succeeded = true, Result = currentEventAddress };
    }

}

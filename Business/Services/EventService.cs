using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;
public class EventService(IEventRepository eventRepository, IEventAddressRepository eventAddressRepository) : IEventservice
{
    private readonly IEventRepository _eventRepository = eventRepository;
    private readonly IEventAddressRepository _eventAddressRepository = eventAddressRepository;

    public async Task<ServiceResponse> CreateEvent(CreateEventRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Name))
            return new ServiceResponse { Succeeded = false, Error = "Request is invalid" };

        var existingEvent = await _eventRepository.ExistsAsync(e => e.Name == request.Name);
        if (existingEvent.Succeeded)
            return new ServiceResponse { Succeeded = false, Error = "Event already exists" };

        // Get or create an event address
        var eventAddressResult = await _eventAddressRepository.GetOneAsync(e => e.Arena == request.Arena);
        EventAddressEntity eventAddressEntity;

        if (!eventAddressResult.Succeeded || eventAddressResult.Result == null)
        {
            eventAddressEntity = new EventAddressEntity
            {
                Arena = request.Arena,
                City = request.City,
                State = request.State
            };

            var addResult = await _eventAddressRepository.AddAsync(eventAddressEntity);
            if (!addResult.Succeeded)
                return new ServiceResponse { Succeeded = false, Error = "Failed to create event address" };
        }
        else
        {
            eventAddressEntity = eventAddressResult.Result!;
        }

        // Create event
        try
        {
            var eventEntity = new EventEntity
            {
                Name = request.Name,
                Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description,
                ImagePath = request.ImagePath,
                EventDate = request.EventDate,
                EventAddressId = eventAddressEntity.Id
            };

            var eventResult = await _eventRepository.AddAsync(eventEntity);
            return eventResult.Succeeded
                ? new ServiceResponse { Succeeded = true }
                : new ServiceResponse { Succeeded = false, Error = eventResult.Error };
        }
        catch (Exception ex)
        {
            return new ServiceResponse { Succeeded = false, Error = ex.Message };
        }
    }


    public async Task<ServiceResponse<Event?>> GetOneEventAsync(string id)
    {
        var result = await _eventRepository.GetOneAsync(x => x.Id == id);

        if (!result.Succeeded || result.Result == null)
            return new ServiceResponse<Event?> { Succeeded = false, Error = "Event not found" };

        var eventEntity = result.Result;

        var currentEvent = new Event
        {
            Id = eventEntity.Id,
            Name = eventEntity.Name,
            Description = eventEntity.Description,
            ImagePath = eventEntity.ImagePath,
            EventDate = eventEntity.EventDate,
            EventAddress = new EventAddress
            {
                Id = eventEntity.EventAddress.Id,
                Arena = eventEntity.EventAddress.Arena,
                City = eventEntity.EventAddress.City,
                State = eventEntity.EventAddress.State
            },
            Packages = eventEntity.Packages.Select(ep => new Package
            {
                Id = ep.Package.Id,
                Name = ep.Package.Name,
                SeatingArrangement = ep.Package.SeatingArrangement,
                Placement = ep.Package.Placement,
                Price = ep.Package.Price,
                Currency = ep.Package.Currency
            }).ToList()
        };

        return new ServiceResponse<Event?> { Result = currentEvent, Succeeded = true };
    }


    public async Task<ServiceResponse<IEnumerable<Event>>> GetAllEventsAsync()
    {
        var result = await _eventRepository.GetAllAsync();

        if (!result.Succeeded || result.Result == null)
            return new ServiceResponse<IEnumerable<Event>> { Succeeded = false, Error = "No events found" };

        var events = result.Result.Select(x => new Event
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            ImagePath = x.ImagePath,
            EventDate = x.EventDate,
            EventAddress = new EventAddress
            {
                Id = x.EventAddress.Id,
                Arena = x.EventAddress.Arena,
                City = x.EventAddress.City,
                State = x.EventAddress.State
            },
            Packages = x.Packages.Select(ep => new Package
            {
                Id = ep.Package.Id,
                Name = ep.Package.Name,
                SeatingArrangement = ep.Package.SeatingArrangement,
                Placement = ep.Package.Placement,
                Price = ep.Package.Price,
                Currency = ep.Package.Currency
            }).ToList()
        });

        return new ServiceResponse<IEnumerable<Event>> { Result = events, Succeeded = true };
    }

}

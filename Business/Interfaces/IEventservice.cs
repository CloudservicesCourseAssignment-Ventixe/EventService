using Business.Models;

namespace Business.Interfaces;

public interface IEventservice
{
    Task<ServiceResponse> CreateEvent(CreateEventRequest request);
    Task<ServiceResponse<IEnumerable<Event>>> GetAllEventsAsync();
    Task<ServiceResponse<Event?>> GetOneEventAsync(string id);
}

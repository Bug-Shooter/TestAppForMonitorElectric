using TestAppForMonitorElectric.Models;

namespace TestAppForMonitorElectric.Interfaces
{
    public interface IManufactorersService
    {
        Task<InternalResult<List<Manufacturer>>> GetAll();
        Task<InternalResult<Manufacturer>> Get(Guid Id);
        Task<InternalResult<List<Manufacturer>>> Get(string Name);
        Task<InternalResult<Manufacturer>> Post(Manufacturer manufacturer);
        Task<InternalResult<Manufacturer>> Delete(Guid Id);
    }
}

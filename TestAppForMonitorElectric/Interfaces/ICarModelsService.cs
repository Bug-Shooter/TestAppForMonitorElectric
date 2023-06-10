using TestAppForMonitorElectric.Models;

namespace TestAppForMonitorElectric.Interfaces
{
    public interface ICarModelsService
    {
        Task<InternalResult<List<CarModel>>> GetByManufactorer(Guid Id);
        Task<InternalResult<CarModel>> Get(Guid Id);
        Task<InternalResult<List<CarModel>>> Get(string Name);
        Task<InternalResult<CarModel>> Post(CarModel carModel);
        Task<InternalResult<CarModel>> Delete(Guid Id);
    }
}

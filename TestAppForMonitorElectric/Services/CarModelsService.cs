using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using TestAppForMonitorElectric.Data;
using TestAppForMonitorElectric.Interfaces;
using TestAppForMonitorElectric.Models;

namespace TestAppForMonitorElectric.Services
{
    public class CarModelsService : ICarModelsService
    {
        private readonly AppDbContext DbContext;
        private readonly ILogger<CarModelsService> _logger;
        public CarModelsService(ILogger<CarModelsService> logger, AppDbContext dbContext)
        {
            _logger = logger;
            DbContext = dbContext;
        }

        public async Task<InternalResult<CarModel>> Get(Guid Id)
        {
            var result = await DbContext.CarModels.FirstOrDefaultAsync(x => x.Id == Id);
            if (result is null)
                return new InternalResult<CarModel> { IsSuccess = false, ErrorMessage = $"CarModel with id-{Id} not found", Model = null };
            else
                return new InternalResult<CarModel> { Model = result};
        }

        public async Task<InternalResult<List<CarModel>>> Get(string Name)
        {
            var result = await DbContext.CarModels.Where(x => x.Name.ToLower() == Name.ToLower()).ToListAsync();
            if (result.Any())
                return new InternalResult<List<CarModel>> { Model = result };
            else
                return new InternalResult<List<CarModel>> { IsSuccess= false, ErrorMessage = $"CarModels with name-{Name} not found" };
        }

        public async Task<InternalResult<List<CarModel>>> GetByManufactorer(Guid Id)
        {
            var result = await DbContext.CarModels.Where(x => x.ManufacturerID == Id).ToListAsync();
            if (result is null)
                return new InternalResult<List<CarModel>> {IsSuccess = false, ErrorMessage = $"CarModel for id-{Id} not found", Model = null };
            else
                return new InternalResult<List<CarModel>> { Model = result };
        }

        public async Task<InternalResult<CarModel>> Post(CarModel carModel)
        {
            if (DbContext.Manufacturers.Any(x => x.Id == carModel.ManufacturerID))
                DbContext.Add(carModel);
            else
                return new InternalResult<CarModel>{IsSuccess = false, ErrorMessage = "Car Model's manufactorer id does not exist", Model = null};

            try
            {
                await DbContext.SaveChangesAsync();
                _logger.LogInformation($"CarModel with ID {carModel.Id} added to DB");
                return new InternalResult<CarModel> { Model = carModel};
            }
            catch (DbUpdateException ex)
            {
                _logger.LogInformation(ex.Message);
                return new InternalResult<CarModel> { IsSuccess = false, ErrorMessage = "Error ocured during adding Car Model to DB", Model = null};
            }
        }
        public async Task<InternalResult<CarModel>> Delete(Guid Id)
        {
            var result = await DbContext.CarModels.FirstOrDefaultAsync(x => x.Id == Id);
            if (result == null)
                return new InternalResult<CarModel> { IsSuccess = false, ErrorMessage = "Manufacturer's id not found. Request canceled", Model = null };
            try
            {
                DbContext.CarModels.Remove(result);
                await DbContext.SaveChangesAsync();
                return new InternalResult<CarModel> { Model = result };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogInformation(ex.Message);
                return new InternalResult<CarModel> { IsSuccess = false, ErrorMessage = "Internal Server Error", Model = null };
            }
        }
    }
}

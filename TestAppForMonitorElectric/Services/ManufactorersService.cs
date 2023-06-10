using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using TestAppForMonitorElectric.Data;
using TestAppForMonitorElectric.Interfaces;
using TestAppForMonitorElectric.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestAppForMonitorElectric.Services
{
    public class ManufactorersService : IManufactorersService
    {
        private readonly ILogger<ManufactorersService> _logger;
        private readonly AppDbContext DbContext;

        public ManufactorersService(ILogger<ManufactorersService> logger, AppDbContext dbContext)
        {
            _logger = logger;
            DbContext = dbContext;
        }

        public async Task<InternalResult<Manufacturer>> Get(Guid Id)
        {
            var result = await DbContext.Manufacturers.FirstOrDefaultAsync(x => x.Id == Id);
            if (result is null)
                return new InternalResult<Manufacturer> { IsSuccess = false, ErrorMessage = $"Manufacturer for id-{Id} was not found" };
            else
                return new InternalResult<Manufacturer> { Model = result };
        }

        public async Task<InternalResult<List<Manufacturer>>> Get(string Name)
        {
            var result = await DbContext.Manufacturers.Where(x => x.Name.ToLower() == Name.ToLower()).ToListAsync();
            if (result.Count == 0)
                return new InternalResult<List<Manufacturer>> { IsSuccess = false, ErrorMessage = $"Manufacturer for Name-{Name} was not found", Model = null };
            else
                return new InternalResult<List<Manufacturer>> { Model = result };
        }

        public async Task<InternalResult<List<Manufacturer>>> GetAll() =>
            new InternalResult<List<Manufacturer>> { Model = await DbContext.Manufacturers.ToListAsync() };

        public async Task<InternalResult<Manufacturer>> Post(Manufacturer manufacturer)
        {
            try
            {
                DbContext.Add(manufacturer);
                await DbContext.SaveChangesAsync();
                return new InternalResult<Manufacturer> { Model = manufacturer };
            }
            catch (DbUpdateException exc)
            {
                _logger.LogError(exc.Message);
                return new InternalResult<Manufacturer> { IsSuccess = false, ErrorMessage = "Error occured during aading to DB", Model = null };
            }
        }
        public async Task<InternalResult<Manufacturer>> Delete(Guid Id)
        {
            var result = await DbContext.Manufacturers.FirstOrDefaultAsync(x => x.Id == Id);
            if (result == null)
                return new InternalResult<Manufacturer> { IsSuccess = false, ErrorMessage = "Manufacturer's id not found. Action canceled", Model = null };

            try
            {
                DbContext.Manufacturers.Remove(result);
                await DbContext.SaveChangesAsync();
                _logger.LogInformation($"Manufacturer with id {result.Id} succesfully deleted");
                return new InternalResult<Manufacturer> { Model = result};
            }
            catch (DbUpdateException exc)
            {
                _logger.LogError(exc.Message);
                return new InternalResult<Manufacturer> { IsSuccess = false, ErrorMessage = "Error occured during removing" };
            }
        }
    }
}

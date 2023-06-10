using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TestAppForMonitorElectric.Data;
using TestAppForMonitorElectric.Interfaces;
using TestAppForMonitorElectric.Models;
using TestAppForMonitorElectric.Services;

namespace TestAppForMonitorElectric.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarModelsController : Controller
    {
        private readonly ICarModelsService _carService;
        public CarModelsController(ICarModelsService carModelsService)
        {
            _carService = carModelsService;
        }

        /// <summary>
        /// Предоставляет список моделей по ID производителя
        /// </summary>
        /// <param name="Id">Id производителя</param>
        [HttpGet("GetByManufacturer/{Id}")]
        public async Task<ObjectResult> GetByManufacturer(Guid Id)
        {
            var result = await _carService.GetByManufactorer(Id);
            if (result.IsSuccess)
                return StatusCode((int)HttpStatusCode.OK, result.Model);
            else
                return StatusCode((int)HttpStatusCode.OK, result.ErrorMessage);
        }

        /// <summary>
        /// Предоставляет модель по Id
        /// </summary>
        /// <param name="Id">Id модели</param>
        [HttpGet]
        public async Task<ObjectResult> Get(Guid Id)
        {
            var result = await _carService.Get(Id);
            if(result.IsSuccess)
                return StatusCode((int)HttpStatusCode.OK, result.Model);
            else    
                return StatusCode((int)HttpStatusCode.OK, result.ErrorMessage);

        }

        /// <summary>
        /// Предоставляет список моделей по имени
        /// </summary>
        /// <param name="Name">Имя модели</param>
        [HttpGet("GetByName/{Name}")]
        public async Task<ObjectResult> GetByName(string Name)
        {
            var result = await _carService.Get(Name);
            if (result.IsSuccess)
                return StatusCode((int)HttpStatusCode.OK, result.Model);
            else
                return StatusCode((int)HttpStatusCode.OK, result.ErrorMessage);
        }

        /// <summary>
        /// Создает новую модель машины
        /// </summary>
        /// <param name="carModel">Модель машины</param>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        public async Task<ObjectResult> Post(CarModel carModel)
        {
            if (!ModelState.IsValid)
                return StatusCode((int)HttpStatusCode.BadRequest,"CarModel model is invalid");

            var result = await _carService.Post(carModel);
            if (result.IsSuccess)
                return StatusCode((int)HttpStatusCode.OK, new { model = result.Model, message = $"CarModel with ID {carModel.Id} added to DB" });
            else
                return StatusCode((int)HttpStatusCode.BadRequest, result.ErrorMessage);
        }

        /// <summary>
        /// Удаляет модель машины
        /// </summary>
        /// <param name="guid">Id машины</param>
        /// <response code="400">Bad Request</response>
        [HttpDelete]
        public async Task<ObjectResult> Delete(Guid guid)
        {
            var result = await _carService.Delete(guid);
            if (result.IsSuccess)
                return StatusCode((int)HttpStatusCode.OK, new { model = result.Model, message = $"CarModel {result.Model?.Name} succesfully deleted" });
            else
                return StatusCode((int)HttpStatusCode.BadRequest, result.ErrorMessage);
        }
    }
}

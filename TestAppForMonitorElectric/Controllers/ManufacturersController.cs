using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TestAppForMonitorElectric.Data;
using TestAppForMonitorElectric.Interfaces;
using TestAppForMonitorElectric.Models;

namespace TestAppForMonitorElectric.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManufacturersController : Controller
    {
        private readonly IManufactorersService _manufactorersService;
        public ManufacturersController(IManufactorersService manufactorersService)
        {
            _manufactorersService = manufactorersService;
        }

        /// <summary>
        /// Возвращает всех производителей из БД
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ObjectResult> GetAll() => StatusCode((int)HttpStatusCode.OK, (await _manufactorersService.GetAll()).Model);
        

        /// <summary>
        /// Возвращает конкретного производителя по ID
        /// </summary>
        /// <param name="Id">Id компании</param>
        [HttpGet]
        public async Task<ObjectResult> Get(Guid Id)
        {
            var result = await _manufactorersService.Get(Id);
            if(result.IsSuccess)
                return StatusCode((int)HttpStatusCode.OK, result.Model);
            else    
                return StatusCode((int)HttpStatusCode.OK,result.ErrorMessage);
        }

        /// <summary>
        /// Ищет производителя по названию
        /// </summary>
        /// <param name="Name">Название производителя</param>
        [HttpGet("GetByName/{Name}")]
        public async Task<ObjectResult> GetByName(string Name)
        {
            var result = await _manufactorersService.Get(Name);
            if (result.IsSuccess)
                return StatusCode((int)HttpStatusCode.OK, result.Model);
            else
                return StatusCode((int)HttpStatusCode.OK, result.ErrorMessage);
        }

        /// <summary>
        /// Создает нового производителя В БД
        /// </summary>
        /// <param name="manufacturer">Производитель</param>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        public async Task<ObjectResult> Post(Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
                return StatusCode((int)HttpStatusCode.BadRequest,"Manufacturer model is invalid");
            
            var result = await _manufactorersService.Post(manufacturer);
            if(result.IsSuccess)
                return StatusCode((int)HttpStatusCode.OK, new {model = result.Model, message = $"Manufacturer {result.Model?.Name} created" });
            else
                return StatusCode((int)HttpStatusCode.BadRequest, result.ErrorMessage);

        }

        /// <summary>
        /// Удаляет производителя из БД (со всеми моделями машин)
        /// </summary>
        /// <param name="guid">Id Производителя</param>
        /// <response code="400">Bad Request</response>
        [HttpDelete]
        public async Task<ObjectResult> Delete(Guid guid)
        {

            var result = await _manufactorersService.Delete(guid);
            if (result.IsSuccess)
                return StatusCode((int)HttpStatusCode.OK, new { model = result.Model, message = $"Manufacturer {result.Model?.Name} deleted" });
            else
                return StatusCode((int)HttpStatusCode.BadRequest, result.ErrorMessage);
        }
    }
}

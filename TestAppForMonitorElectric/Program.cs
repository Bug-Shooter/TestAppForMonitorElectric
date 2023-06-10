
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestAppForMonitorElectric.Data;
using TestAppForMonitorElectric.Interfaces;
using TestAppForMonitorElectric.Services;

namespace TestAppForMonitorElectric
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            
            //����������� ��
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            // ����������� Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //����������� ��������
            builder.Services.AddScoped<ICarModelsService, CarModelsService>();
            builder.Services.AddScoped<IManufactorersService, ManufactorersService>();

   
            var app = builder.Build();


            // ��������� HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
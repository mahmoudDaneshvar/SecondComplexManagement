using Microsoft.EntityFrameworkCore;
using SecondComplexManagement.PersistanceEF;
using SecondComplexManagement.PersistanceEF.Blocks;
using SecondComplexManagement.PersistanceEF.Complexes;
using SecondComplexManagement.PersistanceEF.Units;
using SecondComplexManagement.PersistanceEF.UsageTypes;
using SecondComplexManagement.Services.Blocks;
using SecondComplexManagement.Services.Blocks.Contracts;
using SecondComplexManagement.Services.Complexes;
using SecondComplexManagement.Services.Complexes.Contracts;
using SecondComplexManagement.Services.Contracts;
using SecondComplexManagement.Services.Units;
using SecondComplexManagement.Services.Units.Contracts;
using SecondComplexManagement.Services.UsageTypes;
using SecondComplexManagement.Services.UsageTypes.Contracts;

namespace SecondComplexManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<UnitOfWork, EFUnitOfWork>();
            builder.Services.AddScoped<ComplexRepository, EFComplexRepository>();
            builder.Services.AddScoped<ComplexService, ComplexAppService>();
            builder.Services.AddScoped<BlockService, BlockAppService>();
            builder.Services.AddScoped<BlockRepository, EFBlockRepository>();
            builder.Services.AddScoped<UnitRepository, EFUnitRepository>();
            builder.Services.AddScoped<UnitService, UnitAppService>();
            builder.Services.AddScoped<UsageTypeRepository, EFUsageTypeRepository>();
            builder.Services.AddScoped<UsageTypeService, UsageTypeAppService>();

            builder.Services.AddDbContext<EFDataContext>(_ => 
            _.UseSqlServer("Server=.;Database=SecondComplexManagementDB;Trusted_Connection=True;")
            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
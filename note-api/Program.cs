
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using note_api.Data;
using note_api.Repositories;
using note_api.Services;

namespace note_api
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

            var connection = builder.Configuration.GetConnectionString("DataBase");
            builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connection));

            // Configuração do serviço
            var serviceProvider = CreateServices();

            // Realiza a migração
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }

            builder.Services.AddScoped<NoteService>();

            builder.Services.AddScoped<NoteRepository>();

            builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

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

        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString("Server=localhost;Port=5432;Database=note-api;User Id=postgres;Password=123456;")
                    .ScanIn(typeof(Program).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
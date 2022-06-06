using SozlukProject.Infrastructure;
using SozlukProject.Persistence;
using SozlukProject.Service;

var builder = WebApplication.CreateBuilder(args);

//// Service Registrations
builder.Services.ImplementInfrastructureServices();
// MsSQL Connection
builder.Services.ImplementPersistenceServices(builder.Configuration.GetConnectionString("MsSQL"));
builder.Services.ImplementServiceServices();

// Adding CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("SozlukProject",
        builder =>
        {
            //builder.WithOrigins("http://localhost:3000",
            //                           "http://localhost:3001")
            //                           .AllowAnyHeader()
            //                           .AllowAnyMethod()
            //                           .AllowCredentials();
            builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost").AllowAnyHeader()
                                                                                                           .AllowAnyMethod()
                                                                                                           .AllowCredentials();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

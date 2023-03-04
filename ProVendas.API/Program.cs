using Microsoft.EntityFrameworkCore;
using ProVendas.API.Configurations;
using ProVendas.Data.DataContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();/*.AddJsonOptions(options  => {
    options.JsonSerializerOptions.Converters.Add(new DateConverter());
});*/


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ResolveDependences();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseGlobalizationConfig();

app.MapControllers();

app.UseCors(access => access.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin()
);

app.Run();

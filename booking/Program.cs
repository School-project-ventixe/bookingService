using booking.Repositories;
using booking.Services;
using Microsoft.EntityFrameworkCore;
using booking.Data.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContexts>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("bookingConnection")));

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();

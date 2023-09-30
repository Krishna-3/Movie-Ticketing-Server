using Microsoft.EntityFrameworkCore;
using MovieTicketingApp.Data;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;
using MovieTicketingApp.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://google.com",
                              "null");
                      });
});

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<ILocationRepository,LocationRepository>();
builder.Services.AddScoped<IMovieRepository,MovieRepository>();
builder.Services.AddScoped<ITheatreRepository,TheatreRepository>();
builder.Services.AddScoped<ISeatRepository,SeatRepository>();
builder.Services.AddScoped<ITicketRepository,TicketRepository>();
builder.Services.AddScoped<IMovieLocationRepository,MovieLocationRepository>();
builder.Services.AddScoped<IMovieTheatreRepository,MovieTheatreRepository>();
builder.Services.AddSingleton<IStateRepository,StateRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<State>();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

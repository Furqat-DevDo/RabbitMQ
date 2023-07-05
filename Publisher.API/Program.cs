using CoreApp.Data;
using CoreApp.Services;
using CoreApp.Services.Abstraction;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPublisher,CoreApp.Services.Publisher>();
builder.Services.AddScoped<IFileService,FileService>();
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

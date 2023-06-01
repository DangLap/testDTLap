using demo.WebApplication.CukCuk.BL.BaseBL;
using demo.WebApplication.CukCuk.BL.FoodBL;
using demo.WebApplication.CukCuk.BL.FoodCustomBL;
using demo.WebApplication.CukCuk.BL.ImageBL;
using demo.WebApplication.CukCuk.BL.ServiceHobbieBL;
using demo.WebApplication.CukCuk.DTLap.DL.BaseDL;
using demo.WebApplication.CukCuk.DTLap.DL.FoodCustomDL;
using demo.WebApplication.CukCuk.DTLap.DL.FoodDL;
using demo.WebApplication.CukCuk.DTLap.DL.ImageDL;
using demo.WebApplication.CukCuk.DTLap.DL.ServiceHobbieDL;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
            policy =>
            {
                policy.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
            });
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddScoped<IServiceHobbieDL, ServiceHobbieDL>();
builder.Services.AddScoped<IServiceHobbieBL, ServiceHobbieBL>();


builder.Services.AddScoped<IFoodCustomBL, FoodCustomBL>();
builder.Services.AddScoped<IFoodCustomDL, FoodCustomDL>();

builder.Services.AddScoped<IFoodDL, FoodDL>();
builder.Services.AddScoped<IFoodBL, FoodBL>();

builder.Services.AddScoped<IImageBL, ImageBL>();
builder.Services.AddScoped<IImageDL, ImageDL>();


builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

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

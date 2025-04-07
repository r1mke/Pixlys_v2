using Microsoft.EntityFrameworkCore;
using Pixly.Services.Database;
using Mapster;
using Pixly.Services.Interfaces;
using Pixly.Services.Services;
using CloudinaryDotNet;
using Pixly.Services.Cloudinary;
using Microsoft.Extensions.Options;
using Pixly.Services.ProizvodiStateMachine;
using Pixly.Services.PhotoStateMachine;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IPhotoService, PhotoService>();
builder.Services.AddTransient<BasePhotoState>();
builder.Services.AddTransient<InitialPhotoState>();
builder.Services.AddTransient<DraftPhotoState>();
builder.Services.AddTransient<PendingPhotoState>();
builder.Services.AddTransient<ApprovedPhotoState>();
builder.Services.AddTransient<HiddenPhotoState>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));
builder.Services.AddMapster();

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddSingleton<Cloudinary>(serviceProvider =>
{
    var cloudinarySettings = serviceProvider.GetRequiredService<IOptions<CloudinarySettings>>().Value;
    var account = new Account(cloudinarySettings.CloudName, cloudinarySettings.ApiKey, cloudinarySettings.ApiSecret);
    return new Cloudinary(account);
});



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

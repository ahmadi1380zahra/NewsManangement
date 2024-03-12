using Microsoft.EntityFrameworkCore;
using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Persistance.EF;
using NewspaperManangment.Persistance.EF.Categories;
using NewspaperManangment.Persistance.EF.Tags;
using NewspaperManangment.Services.Catgories;
using NewspaperManangment.Services.Catgories.Contracts;
using NewspaperManangment.Services.Tags;
using NewspaperManangment.Services.Tags.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("appsettings.json");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EFDataContext>(
    options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<EFDataContext>();
builder.Services.AddScoped<UnitOfWork, EFUnitOfWork>();
builder.Services.AddScoped<CategoryService, CategoryAppService>();
builder.Services.AddScoped<CategoryRepository, EFCategoryRepository>();
builder.Services.AddScoped<TagService, TagAppService>();
builder.Services.AddScoped<TagRepository, EFTagRepository>();
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

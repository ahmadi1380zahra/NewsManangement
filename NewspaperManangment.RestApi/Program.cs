using Microsoft.EntityFrameworkCore;
using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Persistance.EF;
using NewspaperManangment.Persistance.EF.Authors;
using NewspaperManangment.Persistance.EF.Categories;
using NewspaperManangment.Persistance.EF.NewspaperCategories;
using NewspaperManangment.Persistance.EF.Newspapers;
using NewspaperManangment.Persistance.EF.Tags;
using NewspaperManangment.Persistance.EF.TheNews;
using NewspaperManangment.Services.Authors;
using NewspaperManangment.Services.Authors.Contracts;
using NewspaperManangment.Services.Catgories;
using NewspaperManangment.Services.Catgories.Contracts;
using NewspaperManangment.Services.NewspaperCategories;
using NewspaperManangment.Services.NewspaperCategories.Contracts;
using NewspaperManangment.Services.Newspapers;
using NewspaperManangment.Services.Newspapers.Contracts;
using NewspaperManangment.Services.Tags;
using NewspaperManangment.Services.Tags.Contracts;
using NewspaperManangment.Services.TheNews;
using NewspaperManangment.Services.TheNews.Contracts;

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
builder.Services.AddScoped<AuthorService, AuthorAppService>();
builder.Services.AddScoped<AuthorRepository,EFAuthorRepository>();
builder.Services.AddScoped<NewspaperService, NewspaperAppService>();
builder.Services.AddScoped<NewspaperRepository, EFNewspaperRepository>();
builder.Services.AddScoped<NewspaperCategoryService, NewspaperCategoryAppService>();
builder.Services.AddScoped<NewspaperCategoryRepository, EFNewspaperCategoryRepository>();
builder.Services.AddScoped<TheNewService, TheNewAppService>();
builder.Services.AddScoped<TheNewRepository, EFTheNewRepository>();
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

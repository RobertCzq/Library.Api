using Library.Api.Infrastructure.Models;
using Library.Api.Infrastructure.Repository;
using Library.Api.Infrastructure.Repository.Interfaces;
using Library.Api.Services;
using Library.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGenericRepository<Book>, GenericRepository<Book>>();
builder.Services.AddScoped<IGenericRepository<Book>, BooksRepository>();
builder.Services.AddScoped<IGenericRepository<Member>, GenericRepository<Member>>();
builder.Services.AddScoped<IGenericRepository<Member>, MembersRepository>();
builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IBorrowTransactionsService, BorrowTransactionsService>();
builder.Services.AddScoped<IMembersService, MembersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

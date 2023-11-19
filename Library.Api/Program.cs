using Library.Api.Infrastructure.Interfaces;
using Library.Api.Infrastructure.Models;
using Library.Api.Infrastructure.Repository;
using Library.Api.Infrastructure.Services;
using Library.Api.Services;
using Library.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDbHelperService, DbHelperService>();
builder.Services.AddScoped<IGenericRepository<Book>, GenericRepository<Book>>();
builder.Services.AddScoped<IGenericRepository<Book>, BooksRepository>();
builder.Services.AddScoped<IGenericRepository<Member>, GenericRepository<Member>>();
builder.Services.AddScoped<IGenericRepository<Member>, BorrowTransactionRepository>();
builder.Services.AddScoped<IBorrowTransactionsRepository, BorrowTransactionsRepository>();
builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IBorrowTransactionsService, BorrowTransactionsService>();
builder.Services.AddScoped<IMembersService, MembersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
}

app.UseAuthorization();

app.MapControllers();

app.Run();

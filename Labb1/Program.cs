using Labb1.Data;
using Labb1.Models;
using Labb1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepository<Book>, BookRepository>();
builder.Services.AddDbContext<BookDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/books", async (IRepository<Book> bookRepo) =>
{
    return Results.Ok(await bookRepo.GetAll());
});

app.MapGet("/books/{id:int}", async (IRepository<Book> bookRepo, int id) =>
{
    return Results.Ok(await bookRepo.GetById(id));
});

app.MapPost("/books", async (IRepository<Book> bookRepo, Book book) =>
{
    if (book != null)
    {
        await bookRepo.Create(book);
        return Results.Ok(book);
    }
    return Results.BadRequest(book);
});

app.Run();
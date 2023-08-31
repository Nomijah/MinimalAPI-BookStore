using Labb1;
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
builder.Services.AddAutoMapper(typeof(MapperConfig));

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
    return Results.BadRequest();
});

app.MapPut("/books", async (IRepository<Book> bookRepo, Book book) =>
{
    if (await bookRepo.GetById(book.Id) != null) 
    {
        await bookRepo.Update(book);
        return Results.Ok(book);
    }
    return Results.NotFound("Book not Found");
});

app.MapDelete("/books/{id:int}", async (IRepository<Book> bookRepo, int id) =>
{
    if (await bookRepo.GetById(id) != null)
    {
        await bookRepo.Delete(id);
        return Results.Ok();
    }
    return Results.NotFound("Book not Found");
});

app.Run();
using AutoMapper;
using FluentValidation;
using Labb1;
using Labb1.Data;
using Labb1.Models;
using Labb1.Models.DTOs;
using Labb1.Models.Validations;
using Labb1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
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
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

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
    APIResponse response = new APIResponse();

    response.Result = await bookRepo.GetAll();
    response.IsSuccess = true;
    response.StatusCode = System.Net.HttpStatusCode.OK;

    return Results.Ok(response);
}).WithName("GetAllBooks").Produces(200);

app.MapGet("/books/{id:int}", async (IRepository<Book> bookRepo, int id) =>
{
    APIResponse response = new APIResponse() 
    { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

    response.Result = await bookRepo.GetById(id);
    if (response.Result == null)
    {
        response.ErrorMessages.Add($"Book with ID {id} not found");
        response.StatusCode = System.Net.HttpStatusCode.NotFound;
        return Results.NotFound(response);
    }
    response.IsSuccess = true;
    response.StatusCode = System.Net.HttpStatusCode.OK;

    return Results.Ok(response);
}).WithName("GetBook").Produces(200).Produces(404);

app.MapGet("/books/author/{author}", 
    async (IRepository<Book> bookRepo, string author) =>
{
    APIResponse response = new APIResponse()
    { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

    response.Result = await bookRepo.GetByAuthor(author);
    if (response.Result == null)
    {
        response.ErrorMessages.Add($"Book with author {author} not found");
        response.StatusCode = System.Net.HttpStatusCode.NotFound;
        return Results.NotFound(response);
    }
    response.IsSuccess = true;
    response.StatusCode = System.Net.HttpStatusCode.OK;

    return Results.Ok(response);
}).WithName("GetBooksByAuthor").Produces(200).Produces(404);

app.MapGet("/books/title/{title}",
    async (IRepository<Book> bookRepo, string title) =>
    {
        APIResponse response = new APIResponse()
        { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

        response.Result = await bookRepo.GetByTitle(title);
        if (response.Result == null)
        {
            response.ErrorMessages.Add($"Book with title {title} not found");
            response.StatusCode = System.Net.HttpStatusCode.NotFound;
            return Results.NotFound(response);
        }
        response.IsSuccess = true;
        response.StatusCode = System.Net.HttpStatusCode.OK;

        return Results.Ok(response);
    }).WithName("GetBooksByTitle").Produces(200).Produces(404);

app.MapPost("/books", async(
    IValidator<BookCreateDTO> _validator,
    IMapper _mapper,
    IRepository<Book> bookRepo, 
    BookCreateDTO book_C_DTO) =>
{
    APIResponse response = new() 
    { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

    _validator = new BookPostValidation();
    var validationResult = await _validator.ValidateAsync(book_C_DTO);
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(response);
    }

    var bookList = await bookRepo.GetAll();
    bool alreadyExists = false;
    foreach( var book in bookList)
    {
        alreadyExists = BookComparer.Compare(book, book_C_DTO);
    }

    if (alreadyExists)
    {
        response.ErrorMessages.Add("Book already exists in database");
        return Results.BadRequest(response);
    }
    
    Book bookToAdd = _mapper.Map<Book>(book_C_DTO);

    await bookRepo.Create(bookToAdd);

    response.Result = bookToAdd;
    response.IsSuccess = true;
    response.StatusCode = System.Net.HttpStatusCode.Created;
    return Results.Ok(response);
    
}).WithName("AddBook").Produces(201).Produces(400);

app.MapPut("/books", async (
    IValidator<Book> _validator,
    IMapper _mapper, 
    IRepository<Book> bookRepo, 
    Book book) =>
{
    APIResponse response = new() 
    { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

    _validator = new BookUpdateValidation();
    var validationResult = await _validator.ValidateAsync(book);
    if (!validationResult.IsValid)
    {
        if (await bookRepo.GetById(book.Id) == null)
        {
            response.ErrorMessages.Add($"Book with ID {book.Id} not found");
            response.StatusCode = System.Net.HttpStatusCode.NotFound;
            return Results.NotFound(response);
        }
        return Results.BadRequest(response);
    }

    await bookRepo.Update(book);
    response.Result = book;
    response.IsSuccess = true;
    response.StatusCode = System.Net.HttpStatusCode.Created;
    return Results.Ok(response);
}).WithName("UpdateBook").Produces(200).Produces(404);

app.MapDelete("/books/{id:int}", async (IRepository<Book> bookRepo, int id) =>
{
    APIResponse response = new()
    { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

    if (await bookRepo.GetById(id) == null)
    {
        response.ErrorMessages.Add($"Book with ID {id} not found");
        response.StatusCode = System.Net.HttpStatusCode.NotFound;
        return Results.NotFound(response);
    }

    await bookRepo.Delete(id);
    response.IsSuccess= true;
    response.StatusCode = System.Net.HttpStatusCode.NoContent;
    return Results.Ok(response);
}).WithName("DeleteBook").Produces(204).Produces(404);

app.Run();
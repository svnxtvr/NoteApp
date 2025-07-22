using NoteApp.DB.Context;
using NoteApp.DB.Entities;
using NoteApp.DB.Repository;
var builder = WebApplication.CreateBuilder();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddScoped<IRepository<Note>, PostgreSQLNoteRepository>();
builder.Services.AddScoped<IRepository<Reminder>, PostgreSQLReminderRepository>();
builder.Services.AddScoped<IRepository<Tag>, PostgreSQLTagRepository>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
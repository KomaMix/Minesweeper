using Minesweeper.Filters;
using Minesweeper.Repositories;
using Minesweeper.Services;
using Newtonsoft.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Для возможности отработки сервера (добавление возможности прослушивания)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://minesweeper-test.studiotg.ru")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// Добавление возможности сериализации двумерных массивов
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// Добавление стиля snake case
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<SnakeCaseSchemaFilter>();
});


// Так как мы не храним все в базе данных, то у нас только
// один экземпляр репозитория на все приложение
builder.Services.AddSingleton<IGameRepository, InMemoryGameRepository>();

// Также нам достаточно одного экземпляра GameService, так как в нем нет асинхронных методов
builder.Services.AddSingleton<IGameService, GameService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

// Разрешение
app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();


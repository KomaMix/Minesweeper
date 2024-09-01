using Minesweeper.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Так как мы не храним все в базе данных, то у нас только
// один экземпляр репозитория на все приложение
builder.Services.AddSingleton<IGameRepository, InMemoryGameRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Не забудь потом убрать!!!!!!!!!!!!!!!!!!!
    app.UseDeveloperExceptionPage();
}


app.UseHttpsRedirection();

app.MapControllers();

app.Run();


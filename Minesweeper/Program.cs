using Minesweeper.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// ��� ��� �� �� ������ ��� � ���� ������, �� � ��� ������
// ���� ��������� ����������� �� ��� ����������
builder.Services.AddSingleton<IGameRepository, InMemoryGameRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // �� ������ ����� ������!!!!!!!!!!!!!!!!!!!
    app.UseDeveloperExceptionPage();
}


app.UseHttpsRedirection();

app.MapControllers();

app.Run();


using Respuestas_HTTTP.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddSingleton<IPeopleService, PeopleServiceImpl>();
// ahora en .net8 con key podemos diferenciar las implementaciones de interfazes atraves de las key
builder.Services.AddKeyedSingleton<IPeopleService, PeopleServiceImpl>("peopleService");
builder.Services.AddKeyedSingleton<IPeopleService, People2ServiceImpl>("people2Service");

builder.Services.AddKeyedSingleton<IRandomService, RandomService>("randomSingleton");
// En clientes diferentes tendran un valor distinto (Postman y web)
builder.Services.AddKeyedScoped<IRandomService, RandomService>("randomScoped");

builder.Services.AddKeyedTransient<IRandomService, RandomService>("randomTransient");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

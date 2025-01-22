using API.Services;

var builder = WebApplication.CreateBuilder(args);
var myAngularAppUrl = "http://localhost:4200";

builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowMyAngularApp", p =>
    {
        p.WithOrigins(myAngularAppUrl).AllowAnyHeader().AllowAnyMethod();
    }
    );
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient<HackerNewsService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowMyAngularApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

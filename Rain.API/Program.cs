var builder = WebApplication.CreateBuilder(args);

// Adding swagger gen
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Using swagger if env is development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.Run();

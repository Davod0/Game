

using Webapi;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddConnections();

builder.Services.AddScoped<LogicController>();
builder.Services.AddDbContext<MyDbContext>();



var app = builder.Build();





// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
app.MapControllers();
app.UseHttpsRedirection();


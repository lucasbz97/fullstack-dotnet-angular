using ManagementUsers.BLL.Configuration;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddConfiguration();
builder.AddDataContext();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(Configuration.CorsPolicyName);
app.MapControllers();

app.Run();

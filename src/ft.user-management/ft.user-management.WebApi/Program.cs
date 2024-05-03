using System;
using System.IO;
using System.Reflection;
using Microsoft.OpenApi.Models; 
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using ft.user_management.Persistence;
using ft.user_management.Application;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();

var info = new OpenApiInfo()
{
    Title = "User Management API Documentation",
    Version = "v1",
    Description =
        "The User Administration API for our financial monitoring application" +
        " improves user management by simplifying registration, authentication," +
        " and profile management. It provides secure access and strong financial" +
        "data protection, making it perfect for applications of all sizes." +
        "\n This API enables easy management of user data while complying to strict" +
        " data protection standards, so enabling both growth and compliance requirements.",
    Contact = new OpenApiContact()
    {
        Name = "Leuel Asfaw",
        Email = "leuela1993@gmail.com"
    }
};

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", info);

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigurePersistenceServices(configuration);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(u => { u.RouteTemplate = "swagger/{documentName}/swagger.json"; });
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger";
        c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "User Management API Documentation");
    }); 
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
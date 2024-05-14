using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ft.user_management.Persistence;
using ft.user_management.Application;
using ft.user_management.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

#region Controller Documentation Setup Swagger

// var info = new OpenApiInfo()
// {
//     Title = "User Management API Documentation",
//     Version = "v1",
//     Description =
//         "The User Administration API for our financial monitoring application" +
//         " improves user management by simplifying registration, authentication," +
//         " and profile management. It provides secure access and strong financial" +
//         "data protection, making it perfect for applications of all sizes." +
//         "\n This API enables easy management of user data while complying to strict" +
//         " data protection standards, so enabling both growth and compliance requirements.",
//     Contact = new OpenApiContact()
//     {
//         Name = "Leuel Asfaw",
//         Email = "leuela1993@gmail.com"
//     }
// };
//
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", info);
//
//     var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//     var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//     c.IncludeXmlComments(xmlPath);
// });

#endregion

#region Register Services

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(configuration);
builder.Services.ConfigurePersistenceServices(configuration);

#endregion

#region Add Jwt Authentication Details

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:AccessTokenKey"]!))
    };
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger(u => { u.RouteTemplate = "swagger/{documentName}/swagger.json"; });
    // app.UseSwaggerUI(c =>
    // {
    //     c.RoutePrefix = "swagger";
    //     c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "User Management API Documentation");
    // });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
using ETradeAPI.API.Configurations.ColumnWriters;
using ETradeAPI.Application;
using ETradeAPI.Application.Validators.Products;
using ETradeAPI.Infrastructure;
using ETradeAPI.Infrastructure.Filters;
using ETradeAPI.Infrastructure.Services.Storage.Local;
using ETradeAPI.Persistance;
using ETradeAPI.Signalr;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // Specify the allowed origin
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials(); // Allow credentials in the CORS request
        });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddStorage<LocalStorage>();
builder.Services.AddPersistanceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddAplicationServices();
builder.Services.AddSignalrServices();

Logger log = new LoggerConfiguration()
                 .WriteTo.Console()
                 .WriteTo.File("logs/log.txt")
                 .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"),"logs",
                 needAutoCreateTable:true,
                 columnOptions:new Dictionary<string, ColumnWriterBase>
                 {
                     {"message",new RenderedMessageColumnWriter()},
                     {"message_template", new MessageTemplateColumnWriter() },
                     {"level",new LevelColumnWriter()},
                     {"time_stap",new TimestampColumnWriter() },
                     {"exception",new ExceptionColumnWriter()},
                     {"log_event",new LogEventSerializedColumnWriter()},
                     {"user_name" , new UserNameColumnWriter()}
                 })
                 .Enrich.FromLogContext()
                 .MinimumLevel.Information()
                 .CreateLogger();
builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("MyResponseHeader");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});             

builder.Services.AddControllers(opt=>opt.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(opt=>opt.SuppressModelStateInvalidFilter=true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, //oluþturulacak token deðerinin hangi siteler kullanýr --site
            ValidateIssuer = true, //oluþturulacak tokený kimin daðýttýðýný ifade edeceðimiz alan__bizim api
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true, //üretilecek token deðerini uyg ait deðer olduðunu ifade eden key

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore,expires,securityToken,validationParameters)=>expires !=null ? expires>DateTime.UtcNow:false,
            NameClaimType=ClaimTypes.Name
        };
    });
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpLogging(); 
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name", username);
    await next();
});
app.UseSerilogRequestLogging();


app.MapControllers();
app.MapHubs();

app.Run();

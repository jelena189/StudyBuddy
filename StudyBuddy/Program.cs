using Autofac.Extensions.DependencyInjection;
using StudyBuddy.Services.Infrastructure;
using StudyBuddy.WebApi;
using System.Reflection;
using Autofac;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Services.Mapper;
using StudyBuddy.Repositories;
using StudyBuddy.Services.Services;
using StudyBuddy.WebApi.Extensions;
using StudyBuddy.Repositories.Repositories;
using StudyBuddy.WebApi.Middlewares;
using StudyBuddy.Services.Validation;
using FluentValidation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add hosted services to the container
builder.Host.ConfigureServices(services =>
{
    services.AddHostedService<NotificationService>();
});

var jwtSettings = builder.Configuration.GetSection(typeof(JwtSettings).Name);
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthPolicy();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddValidatorsFromAssembly(typeof(UserSignUpValidator).Assembly);
builder.Services.Configure<NotificationSettings>(builder.Configuration.GetSection(typeof(NotificationSettings).Name));
builder.Services.Configure<JwtSettings>(jwtSettings);
builder.Services.AddAuth(jwtSettings.Get<JwtSettings>());
builder.Services.AddDbContext<StudyBuddyContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(BaseService<,,>)))
                                            .Where(t => t.Name.EndsWith("Service") && t.Name != "NotificationService")
                                            .AsImplementedInterfaces().InstancePerLifetimeScope();

    builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(BaseRepository<,>)))
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces().InstancePerLifetimeScope();
});

var app = builder.Build();
app.ApplyMigrations();

app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
app.UseMiddleware(typeof(RequestLoggingMiddleware));
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("EnableCORS");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
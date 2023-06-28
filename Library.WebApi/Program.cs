using Library.Persistence;
using Library.Application.Interfaces;
using System.Reflection;
using Library.Application.Common.Mapping;
using Library.Application;
using AutoMapper;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(ILibraryDbContext).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration.GetConnectionString("MsSqlExpress"));
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    DbInitializer.Initialize(db);

    var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
}



app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();



//using (var scope = host.Services.CreateScope())
//{
//    var serviceProvider = scope.ServiceProvider;
//    try
//    {
//        var context = serviceProvider.GetRequiredService<LibraryDbContext>();
//        DbInitializer.Initialize(context);
//    }
//    catch (Exception exception)
//    {
//        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
//        logger.LogError(exception, "An error occurred while app initialization");
//    }
//}

//        host.Run();


//    IHostBuilder CreateHostBuilder(string[] args) =>
//            Host.CreateDefaultBuilder(args)
//                .ConfigureWebHostDefaults(webBuilder =>
//                {
//                    webBuilder.UseStartup<Startup>();
//                });

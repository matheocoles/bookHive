using BookHive;
using FastEndpoints;
using FastEndpoints.Swagger;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


builder.Services.AddFastEndpoints().SwaggerDocument(options => 
{
    options.ShortSchemaNames = true;
});

builder.Services.AddDbContext<BookHiveDbContext>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.UseFastEndpoints(options =>
{
    options.Endpoints.RoutePrefix = "API"; 
    options.Endpoints.ShortNames = true;
}).UseSwaggerGen();

app.Run();
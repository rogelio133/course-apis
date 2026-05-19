using cursoApis.MIddlewares;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddLogging();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new()
        {
            Title = "Mi API 133",
            Version = "v1",
            Description = "Aqui va una descripcion de API"
        };
        return Task.CompletedTask;
    });
});

builder.Services.AddSwaggerGen(options => {
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var myAllowedOrigins = "myAllowedOrigins";

builder.Services.AddCors(opts => {
    opts.AddPolicy(
        name: myAllowedOrigins,
        p =>
        {
            p.AllowAnyHeader();
            //p.AllowAnyOrigin();
            p.WithOrigins(["http://127.0.0.1:5500"]);
        }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "API v1");
    });
}



app.UseHttpsRedirection();

app.UseCors(myAllowedOrigins);

app.UseAuthorization();

app.UseRequestLogging();


app.MapControllers();

app.Run();

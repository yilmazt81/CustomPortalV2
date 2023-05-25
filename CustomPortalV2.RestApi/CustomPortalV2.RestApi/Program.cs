using CustomPortalV2.RestApi;
using CustomPortalV2.DataAccessLayer;
using CustomPortalV2.Business;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services); 
/*builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));*/
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(
                options => options.WithOrigins("http://localhost:3000", 
                "http://127.0.0.1:3000", 
                "http://localhost", 
                "http://127.0.0.1").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
            );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<JwtMiddleware>();

app.Run();

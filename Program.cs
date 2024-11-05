using connect.Reviews.Models;
using connect.Reviews.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder.
            AllowAnyOrigin().
            AllowAnyHeader().
            AllowAnyMethod().
            WithOrigins("http://localhost:8080");
    });
});

builder.Services.Configure<AppDatabaseSettings>(
    builder.Configuration.GetSection("AppDatabase"));

builder.Services.AddSingleton<ReviewService>();
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors("CORSPolicy");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

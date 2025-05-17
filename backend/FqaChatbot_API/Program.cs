using FqaChatbot_API.Models; // 確保引入 Models 命名空間
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // API 端點探索器，Swagger所需
builder.Services.AddSwaggerGen();  // Swagger 生成器，Swagger所需

// 配置 CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
        policy =>
        {
            policy.WithOrigins("http://127.0.0.1:5500") 
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 使用 CORS
app.UseCors("AllowMyOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run(); // 開始監聽來自用戶端的 HTTP 請求
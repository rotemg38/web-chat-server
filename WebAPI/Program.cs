using Services;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<ChatsService>();
builder.Services.AddScoped<MessagesService>();
builder.Services.AddScoped<MsgInChatService>();

builder.Services.AddSignalR();


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromHours(24);
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "myAllowSpecificOrigins",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("myAllowSpecificOrigins");

app.UseRouting();

app.UseSession();

app.MapControllers();

app.UseEndpoints(endpoints => { endpoints.MapHub<MsgHub>("/hubs/msgs"); });

app.Run();

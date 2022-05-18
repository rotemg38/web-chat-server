using Services;
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

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromHours(24);
});

builder.Services.AddHttpContextAccessor();

/*builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5000").WithMethods("PUT", "DELETE","GET");
                          
                      });
});*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSession();

app.MapControllers();

app.Run();

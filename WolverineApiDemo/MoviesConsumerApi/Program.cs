using Wolverine;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWolverine(x =>
{
    x.ListenToRabbitQueue("movies-queue").UseForReplies();
    x.UseRabbitMq(c =>
    {
        c.HostName = "localhost";
    }).AutoProvision();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.Run();

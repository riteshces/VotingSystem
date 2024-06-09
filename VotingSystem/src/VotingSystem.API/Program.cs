using MongoDB.Driver;
using VotingSystem.Application;
using VotingSystem.Application.Contracts;
using VotingSystem.Core;
using VotingSystem.Core.Contracts;
using VotingSystem.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<AppDbContext>(provider =>
{
    var mongoClient = provider.GetService<IMongoClient>();
    return new AppDbContext(mongoClient);
});

builder.Services.AddSingleton<IVotingPollFactory,VotingPollFactory>();
builder.Services.AddSingleton<IVotingCounterManager, VotingCounterManager>();
builder.Services.AddSingleton<IVotingSystemPersistance, VotingSystemPersistance>();
builder.Services.AddSingleton<IStatisticsInteractor, StatisticsInteractor>();
builder.Services.AddSingleton<IVotingInteractor, VotingInteractor>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

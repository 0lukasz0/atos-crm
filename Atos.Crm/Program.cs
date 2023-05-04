using Atos.Crm.Abstractions;
using Atos.Crm.Abstractions.DataAccess;
using Atos.Crm.Domain.Customers.Commands;
using Atos.Crm.Domain.Customers.Queries;
using Atos.Crm.Infrastructure.Commands;
using Atos.Crm.Infrastructure.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(ICommandHandler<CreateCustomerCommand, int>), typeof(CreateCustomerCommandHandler));
builder.Services.AddScoped(typeof(ICommandHandler<DeleteCustomerCommand, bool>), typeof(DeleteCustomerCommandHandler));
builder.Services.AddScoped<ICommandBus, CommandBus>();
builder.Services.AddScoped<ICustomerListQuery, CustomerListQuery>();

// note: very very bad, not production code!, testing only to avoid static
builder.Services.AddSingleton(typeof(IDbContext<>), typeof(DbContext<>));


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

using Bogus;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR().AddAzureSignalR();

builder.Services.AddDbContext<IntelligentHouseDbContext>(o => 
{
    string cnxString = builder.Configuration.GetConnectionString("SqlHouse") ?? 
                            throw new ArgumentNullException("Connection string not found");                                        
    o.UseSqlServer(cnxString);    
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapPost("/createdb", async (IntelligentHouseDbContext dbContext) =>
{
    try
    {
        await dbContext.Database.EnsureCreatedAsync();
        return Results.Ok("Database and tables created successfully");
    }
    catch (Exception ex)
    {        
        if (app.Environment.IsDevelopment())
        {
            return Results.Problem(detail: ex.Message, title: "Server error occurred while creating database");
        }
        return Results.Problem(title: "Server error occurred while creating database");
    }


})
.WithName("CreateDatabase")
.WithOpenApi();

app.MapPost("/houses", async (IntelligentHouseDbContext dbContext, int numberOfHouses) =>
{
    var faker = new Faker("en");

    var houses = new List<House>();
    var random = new Random();
    for (int i = 0; i < numberOfHouses; i++)
    {
        var house = new House
        {
            Name = faker.Lorem.Word(),
            Address = new Address
            {
                CivicNumber = faker.Address.BuildingNumber(),
                Street = faker.Address.StreetAddress(),
                City = faker.Address.City(),
                State = faker.Address.State(),
                ZipCode = faker.Address.ZipCode()
            },
            UserId = Guid.NewGuid().ToString()
        };
        house.Thermostats = new List<Thermostat>
        {
            new Thermostat
            {
                Name = faker.Lorem.Word(),
                Temperature = random.Next(15, 30)
            }
        };
        houses.Add(house);
    }
    await dbContext.Houses.AddRangeAsync(houses);
    await dbContext.SaveChangesAsync();
    return Results.Created($"House created: ", houses.Count());
})
.WithName("GenerateHousesWithThermostats")
.WithOpenApi();

app.MapDelete("/houses", async (IntelligentHouseDbContext dbContext) =>
{
    dbContext.Houses.RemoveRange(dbContext.Houses);
    await dbContext.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("DeleteAllHouses")
.WithOpenApi();

app.MapGet("/houses/{houseId:int}/thermostats", async (IntelligentHouseDbContext dbContext, int houseId) =>
{
    var house = await dbContext.Houses.Include(h => h.Thermostats).FirstOrDefaultAsync(h => h.Id == houseId);
    if (house == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(house.Thermostats);
})
.WithName("GetThermostatsByHouseId")
.WithOpenApi();

app.MapHub<HouseHub>("/househub");
app.Run();
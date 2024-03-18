using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace IntelligentHouse;

public class HouseHub : Hub 
{
    private readonly IntelligentHouseDbContext _repository;

    public HouseHub(IntelligentHouseDbContext repository)
    {
        _repository = repository;
    }

    public async Task GetThermostat(string userId)
    {

        var thermostat = await _repository.Houses
                                          .Where(x => x.UserId == userId)
                                          .Select(x => x.Thermostats)
                                          .SingleOrDefaultAsync();   

        await Clients.Caller.SendAsync("ReceiveThermostat", thermostat);
    }    
}
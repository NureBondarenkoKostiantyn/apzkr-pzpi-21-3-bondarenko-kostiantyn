using TrainSmart.Domain.Entities;

namespace TrainSmart.Domain.AggregateRoots;

public class Team: BaseAggregateRoot
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? CountryName { get; set; }
    public Guid SportId { get; private set; }
    
    private readonly HashSet<TeamAthlete> _athletes = [];

    public IReadOnlyCollection<TeamAthlete> Athletes => _athletes;
    
    public Team(
        string name, 
        string? description,
        string? countryName, 
        Guid sportId) : base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
        CountryName = countryName;
        SportId = sportId;
    }

    public void AddAthlete(TeamAthlete athlete)
    {
        if (_athletes.All(x => x.Id != athlete.Id))
        {
            _athletes.Add(athlete);
        }
    }
    
    public void RemoveAthlete(TeamAthlete athlete)
    {
        var teamAthlete = _athletes.FirstOrDefault(x => x.Id == athlete.Id);
        if (teamAthlete is not null)
        {
            _athletes.Remove(teamAthlete);
        }
    }
}
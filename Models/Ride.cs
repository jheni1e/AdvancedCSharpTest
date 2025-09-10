namespace RideClub.Models;

public class Ride
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public int CreatorID { get; set; }
    public User Creator { get; set; }

    public ICollection<Point> Points { get; set; } = [];
}
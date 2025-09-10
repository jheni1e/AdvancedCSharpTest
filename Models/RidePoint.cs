namespace RideClub.Models;

public class RidePoint
{
    public int ID { get; set; }

    public int RideID { get; set; }
    public int PointID { get; set; }

    public Ride Ride { get; set; }
    public Point Point { get; set; }
}
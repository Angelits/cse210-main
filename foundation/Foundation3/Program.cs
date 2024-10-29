using System;
using System.Collections.Generic;


public abstract class Activity
{
    protected DateTime date;
    protected int duration;

    public Activity(DateTime date, int duration)
    {
        this.date = date;
        this.duration = duration;
    }

    public virtual double GetDistance()
    {
        return 0; 
    }

    public virtual double GetSpeed()
    {
        return 0;
    }

    public virtual double GetPace()
    {
        double distance = GetDistance();
        return distance > 0 ? (duration / distance) : 0; 
    }

    public virtual string GetSummary()
    {
        return $"{date:dd MMM yyyy} - Duration: {duration} min";
    }
}


public class Running : Activity
{
    private double distance; 

    public Running(DateTime date, int duration, double distance) : base(date, duration)
    {
        this.distance = distance;
    }

    public override double GetDistance()
    {
        return distance;
    }

    public override double GetSpeed()
    {
        return (distance / duration) * 60; 
    }

    public override string GetSummary()
    {
        return base.GetSummary() + $" Running - Distance: {distance} miles, Speed: {GetSpeed():F1} mph, Pace: {GetPace():F2} min per mile";
    }
}


public class Cycling : Activity
{
    private double speed; 

    public Cycling(DateTime date, int duration, double speed) : base(date, duration)
    {
        this.speed = speed;
    }

    public override double GetDistance()
    {
        return (speed * duration) / 60; 
    }

    public override string GetSummary()
    {
        return base.GetSummary() + $" Cycling - Distance: {GetDistance():F1} miles, Speed: {speed} mph, Pace: {60 / speed:F2} min per mile";
    }
}


public class Swimming : Activity
{
    private int laps;

    public Swimming(DateTime date, int duration, int laps) : base(date, duration)
    {
        this.laps = laps;
    }

    public override double GetDistance()
    {
        return (laps * 50) / 1000.0 * 0.62; 
    }

    public override double GetSpeed()
    {
        return (GetDistance() / duration) * 60; 
    }

    public override string GetSummary()
    {
        return base.GetSummary() + $" Swimming - Distance: {GetDistance():F1} miles, Speed: {GetSpeed():F1} mph, Pace: {GetPace():F2} min per mile";
    }
}


class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2023, 10, 3), 30, 3.0),
            new Cycling(new DateTime(2023, 11, 4), 45, 12.0),
            new Swimming(new DateTime(2023, 12, 5), 30, 20)
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}

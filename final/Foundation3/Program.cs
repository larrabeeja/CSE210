using System;
using System.Collections.Generic;

public class Address
{
    private string streetAddress;
    private string city;
    private string state;
    private string country;

    public Address(string streetAddress, string city, string state, string country)
    {
        this.streetAddress = streetAddress;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public override string ToString()
    {
        return $"{streetAddress}, {city}, {state}, {country}";
    }
}

public abstract class Event
{
    protected string title;
    protected string description;
    protected DateTime date;
    protected string time;
    protected Address address;

    public Event(string title, string description, DateTime date, string time, Address address)
    {
        this.title = title;
        this.description = description;
        this.date = date;
        this.time = time;
        this.address = address;
    }

    public virtual string GetStandardDetails()
    {
        return $"Title: {title}\nDescription: {description}\nDate: {date.ToShortDateString()}\nTime: {time}\nAddress: {address}";
    }

    public abstract string GetFullDetails();
    public abstract string GetShortDescription();
}

public class Lecture : Event
{
    private string speaker;
    private int capacity;

    public Lecture(string title, string description, DateTime date, string time, Address address, string speaker, int capacity)
        : base(title, description, date, time, address)
    {
        this.speaker = speaker;
        this.capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nType: Lecture\nSpeaker: {speaker}\nCapacity: {capacity}";
    }

    public override string GetShortDescription()
    {
        return $"Lecture: {title} on {date.ToShortDateString()}";
    }
}

public class Reception : Event
{
    private string rsvpEmail;

    public Reception(string title, string description, DateTime date, string time, Address address, string rsvpEmail)
        : base(title, description, date, time, address)
    {
        this.rsvpEmail = rsvpEmail;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nType: Reception\nRSVP Email: {rsvpEmail}";
    }

    public override string GetShortDescription()
    {
        return $"Reception: {title} on {date.ToShortDateString()}";
    }
}

public class OutdoorGathering : Event
{
    private string weatherForecast;

    public OutdoorGathering(string title, string description, DateTime date, string time, Address address, string weatherForecast)
        : base(title, description, date, time, address)
    {
        this.weatherForecast = weatherForecast;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nType: Outdoor Gathering\nWeather Forecast: {weatherForecast}";
    }

    public override string GetShortDescription()
    {
        return $"Outdoor Gathering: {title} on {date.ToShortDateString()}";
    }
}

class Program
{
    static void Main()
    {
        // Create Address instances
        var address1 = new Address("123 Main St", "Springfield", "IL", "USA");
        var address2 = new Address("456 Elm St", "Metropolis", "NY", "USA");
        var address3 = new Address("789 Oak St", "Gotham", "NJ", "USA");

        // Create Event instances
        var lecture = new Lecture("Tech Talk", "A lecture on the latest in tech", new DateTime(2024, 9, 15), "10:00 AM", address1, "Dr. Smith", 100);
        var reception = new Reception("Networking Event", "An evening of networking", new DateTime(2024, 10, 20), "6:00 PM", address2, "rsvp@example.com");
        var outdoorGathering = new OutdoorGathering("Community Picnic", "A fun day in the park", new DateTime(2024, 8, 5), "12:00 PM", address3, "Sunny with a chance of clouds");

        // Create a list of events
        var events = new List<Event> { lecture, reception, outdoorGathering };

        // Iterate through the list of events and display information
        foreach (var evt in events)
        {
            Console.WriteLine(evt.GetStandardDetails());
            Console.WriteLine();
            Console.WriteLine(evt.GetFullDetails());
            Console.WriteLine();
            Console.WriteLine(evt.GetShortDescription());
            Console.WriteLine();
        }
    }
}

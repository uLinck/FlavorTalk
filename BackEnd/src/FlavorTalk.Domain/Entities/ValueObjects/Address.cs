using Microsoft.EntityFrameworkCore;

namespace FlavorTalk.Domain.Entities.ValueObjects;

[Owned]
public class Address
{

    public required string Street { get; set; }

    public required string Number { get; set; }

    public string? Complement { get; set; }

    public string? Neighborhood { get; set; }

    public required string City { get; set; }

    public required string State { get; set; }

    public required string ZipCode { get; set; }

    public required string Country { get; set; }

    public string? Reference { get; set; }

    protected Address() { }

    public Address
    (
        string street, string number, string? complement,
        string? neighborhood, string city, string state,
        string zipCode, string country, string? reference
    )
    {
        Street = street;
        Number = number;
        Complement = complement;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
        Reference = reference;
    }
}

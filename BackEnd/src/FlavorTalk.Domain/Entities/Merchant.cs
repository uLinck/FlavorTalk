namespace FlavorTalk.Domain.Entities;
public class Merchant : UserBaseEntity
{
    public required string Name { get; set; }

    // TO-DO Catalog

    // TO-DO Review

    public decimal? RatingAverage { get; set; }

    public long Likes { get; set; } = 0;

    public required string ContactNumber { get; set; }

    public required Address Address { get; set; }
}

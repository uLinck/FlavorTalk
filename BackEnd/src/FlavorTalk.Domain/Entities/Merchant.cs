﻿using FlavorTalk.Domain.Entities.ValueObjects;

namespace FlavorTalk.Domain.Entities;
public class Merchant : BaseEntity
{
    public required string Name { get; set; }

    public Catalog Catalog { get; set; } = new([]);

    public required List<Review> Reviews { get; set; } = [];

    public float? RatingAverage { get; set; }

    public long Likes { get; set; } = 0;

    public required string ContactNumber { get; set; }

    public required Address Address { get; set; }
}

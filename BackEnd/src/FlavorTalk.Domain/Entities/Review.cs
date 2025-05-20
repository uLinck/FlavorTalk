using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlavorTalk.Domain.Entities;
public class Review : BaseEntity
{
    [Key]
    public required User Author { get; set; }

    public required string Description { get; set; }

    public decimal? Rating { get; set; }

    public bool Recommends { get; set; } = false;

    public int Like { get; set; } = 0;

    public int Dislike { get; set; } = 0;
}


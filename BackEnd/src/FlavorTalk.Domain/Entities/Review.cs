using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlavorTalk.Domain.Entities;
public class Review : BaseEntity
{
    public Guid AuthorId { get; set; }

    [ForeignKey(nameof(AuthorId))]
    public required User Author { get; set; }

    [Required]
    public required string Description { get; set; }

    public Guid MerchantId { get; set; }

    [ForeignKey(nameof(MerchantId))]
    public required Merchant Merchant { get; set; }

    public Guid? PlateId { get; set; }

    [ForeignKey(nameof(PlateId))]
    public Plate? Plate { get; set; }

    public decimal? Rating { get; set; }

    public bool Recommends { get; set; } = false;

    public int Like { get; set; } = 0;

    public int Dislike { get; set; } = 0;
}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Adoption
{
    public int Id { get; set; }

    [Required]
    public int PetId { get; set; }

    [ForeignKey("PetId")]
    public Pet Pet { get; set; }

    [Required]
    public int AdopterId { get; set; }

    [ForeignKey("AdopterId")]
    public Adopter Adopter { get; set; }

    public DateTime AdoptionDate { get; set; }
}

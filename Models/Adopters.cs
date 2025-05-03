using System.ComponentModel.DataAnnotations;

public class Adopter
{
    public int Id { get; set; }

    [Required]
    public string FullName { get; set; }

    public ICollection<Adoption> Adoptions { get; set; }
}

using System.ComponentModel.DataAnnotations;

public class Pet
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Range(0, 100)]
    public int Age { get; set; }

    [Required]
    public string Type { get; set; }

    public bool IsAdopted { get; set; }

    public Adoption? Adoption { get; set; }
}

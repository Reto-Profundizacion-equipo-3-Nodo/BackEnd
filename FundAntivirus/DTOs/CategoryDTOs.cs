namespace FundAntivirus.DTOs
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryCreateDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MinLength(3, ErrorMessage = "El nombre debe tener al menos 3 caracteres")]
        [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        public string Name { get; set; } = string.Empty; // Evita valores nulos en la serialización

        public string? Description { get; set; }
    }

    public class CategoryUpdateDTO
    {
        [Required(ErrorMessage = "El ID de la categoría es obligatorio")]
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "El nombre debe tener al menos 3 caracteres")]
        [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        public string Name { get; set; } = string.Empty; // Evita valores nulos

        public string? Description { get; set; }
    }

    public class CategoryResponseDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; } = string.Empty; // Garantiza que no sea null en respuestas

        public string? Description { get; set; }
    }
}





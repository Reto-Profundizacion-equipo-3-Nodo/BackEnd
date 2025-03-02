using System.ComponentModel.DataAnnotations;

namespace FundacionAntivirus.Dto
{
    public class UsersResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string Rol { get; set; } = null!;

    }

    public class UsersRequestDto
    {
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        public string Password { get; set; } = null!;

        public string Rol { get; set; } = null!;
    }
}
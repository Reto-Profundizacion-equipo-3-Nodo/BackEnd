using System.ComponentModel.DataAnnotations;

namespace FundacionAntivirus.Dtos
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Celular { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string Rol { get; set; } = null!;

    }

    public class UserRequestDto
    {
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "El rol es obligatorio")]
        [RegularExpression("^(admin|user)$", ErrorMessage = "El rol no es válido")]
        public string Rol { get; set; } = null!;
        [Required(ErrorMessage = "El número de celular es obligatorio")]
        [Phone(ErrorMessage = "El número de celular no es válido")]
        public string Celular { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        private DateTime _fechaNacimiento;
        public DateTime FechaNacimiento
        {
            get => _fechaNacimiento;
            set => _fechaNacimiento = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
    }
}
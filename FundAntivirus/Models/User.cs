// namespace FundAntivirus.Models

// {
//     public class User
//     {
//         public required int Id{get; set;}
//         public required string UserName{get; set;}

//         public required string Email{get; set;}

//         public required string PasswordHash{get; set;}
        
//         public required string Role{get; set;} //Admin, User

//     }
// }
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundAntivirus.Models
{
    /// <summary>
    /// Representa un usuario dentro del sistema con credenciales y roles de acceso.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identificador único del usuario (Clave primaria, generado automáticamente).
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Generado automáticamente por la BD
        public int Id { get; set; }

        /// <summary>
        /// Nombre de usuario, requerido y no puede estar vacío.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario. Debe ser un email válido.
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Hash de la contraseña del usuario. Se almacena en formato seguro.
        /// </summary>
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Rol del usuario (Admin o User).
        /// </summary>
        [Required]
        public string Role { get; set; } = "User";
    }

    /// <summary>
    /// Enumeración de roles disponibles dentro del sistema.
    /// </summary>
    public enum UserRole
    {
        Admin,
        User
    }
}
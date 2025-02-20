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

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Rol { get; set; } = null!;
    }
}
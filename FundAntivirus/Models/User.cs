namespace FundAntivirus.Models

{
    public class User
    {
        public required int Id{get; set;}
        public required string UserName{get; set;}

        public required string Email{get; set;}

        public required string PasswordHash{get; set;}
        
        public required string Role{get; set;} //Admin, User

    }
}
using System;

namespace FundacionAntivirus.Services
{
    // Excepción personalizada para conflictos, como un email ya registrado
    public class CustomConflictException : Exception
    {
        public CustomConflictException(string message) : base(message)
        {
        }
    }
}

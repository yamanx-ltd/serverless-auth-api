namespace Domain.Services;

public interface ICryptoService 
{
    string HashPassword(string password);
}
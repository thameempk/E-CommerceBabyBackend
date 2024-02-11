namespace BabyBackend.JwtVerification
{
    public interface IJwtServices
    {
        int GetUserIdFromToken(string token);
    }
}

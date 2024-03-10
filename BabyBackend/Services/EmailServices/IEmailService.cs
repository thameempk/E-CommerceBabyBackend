namespace BabyBackend.Services.EmailServices
{
    public interface IEmailService
    {
        Task<bool> sendOtp(string email);
        bool verifyOtp(string email, string otp);

    }
}

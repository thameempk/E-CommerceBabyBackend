using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace BabyBackend.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private static Dictionary<string, string> otpDictionary = new Dictionary<string, string>();

        public async Task<bool> sendOtp(string email)
        {
            try
            {
                string otp = GenerateOTP();

                await SendEmail(email, otp);
                otpDictionary[email] = otp;
                return true;
            }catch (Exception ex)
            {
                return false;
            }
           

        }

       
        public bool verifyOtp(string email, string otp)
        {
            if (otpDictionary.ContainsKey(email) && otpDictionary[email] == otp)
            {
                otpDictionary.Remove(email);
                return true;
            }
            else
            {
                return false;
            }
        }

        private string GenerateOTP()
        {
            // Generate a 6-digit OTP
            Random rnd = new Random();
            int otp = rnd.Next(100000, 999999);
            return otp.ToString();
        }

        public async Task SendEmail(string toAddress, string otp)
        {

            
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587; 
            smtpClient.Credentials = new NetworkCredential("thameempk292@gmail.com", "xnuo hihq cfaw mkkg");
            smtpClient.EnableSsl = true; 

            
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("thameempk292@gmail.com");
            mailMessage.To.Add(toAddress);
            mailMessage.Subject = "OTP Verification";
            mailMessage.Body = "Your OTP for email verification is: " + otp;
            
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}

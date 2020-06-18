using System;
using System.Threading.Tasks;

namespace PlaneBuilder.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}

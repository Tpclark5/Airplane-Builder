using System;
using System.Threading.Tasks;

namespace PlaneBuilder.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}

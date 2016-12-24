using System.Threading.Tasks;

namespace AspNetInterop.UI.Core.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}

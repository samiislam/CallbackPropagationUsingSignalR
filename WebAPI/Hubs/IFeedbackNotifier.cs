using System.Threading.Tasks;

namespace WebAPI.Hubs
{
    public interface IFeedbackNotifier
    {
        Task ReceiveFeedback(string feedback);
    }
}
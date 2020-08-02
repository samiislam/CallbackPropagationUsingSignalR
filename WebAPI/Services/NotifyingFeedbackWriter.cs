using Microsoft.Extensions.Logging;
using WebAPI.Hubs;

namespace WebAPI.Services
{
    public class NotifyingFeedbackWriter : INotifyingFeedbackWriter
    {
        private ILogger<NotifyingFeedbackWriter> Logger { get; }
        private IFeedbackNotifier Notifier { get; set; }
        
        public NotifyingFeedbackWriter(ILogger<NotifyingFeedbackWriter> logger) => Logger = logger;
        
        public void SetNotifier(IFeedbackNotifier notifier) => Notifier = notifier;

        public void Write(string feedback)
        {
            if (Notifier is null)
            {
                Logger.LogError("Client not connected");
            }
            else
            {
                Notifier.ReceiveFeedback(feedback);
            }
        }
    }
}
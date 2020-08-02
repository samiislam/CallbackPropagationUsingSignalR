using Microsoft.AspNetCore.SignalR;
using SimpleDLL;
using WebAPI.Hubs;

namespace WebAPI.Services
{
    public class SimpleDLLFeedbackProxy : ISimpleDLLFeedbackProxy
    {
        private INotifyingFeedbackWriter NotifyingFeedbackWriter { get; }

        private IHubContext<FeedbackNotifierHub, IFeedbackNotifier> HubContext { get; }

        private ISimpleDLLFeedback SimpleDLLFeedback { get; }

        public SimpleDLLFeedbackProxy(INotifyingFeedbackWriter notifyingFeedbackWriter,
                                      IHubContext<FeedbackNotifierHub, IFeedbackNotifier> hubContext,
                                      ISimpleDLLFeedback simpleDLLFeedback)
        {
            NotifyingFeedbackWriter = notifyingFeedbackWriter;
            HubContext = hubContext;
            SimpleDLLFeedback = simpleDLLFeedback;
        }

        public ISimpleDLLFeedback Get(string subid)
        {
            NotifyingFeedbackWriter.SetNotifier(HubContext.Clients.Group(subid));
            return SimpleDLLFeedback;
        }
    }
}
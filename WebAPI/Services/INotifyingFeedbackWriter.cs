using WebAPI.Hubs;

namespace WebAPI.Services
{
    public interface INotifyingFeedbackWriter : IWriter
    {
        void SetNotifier(IFeedbackNotifier notifier);
    }
}
using SimpleDLL;

namespace WebAPI.Services
{
    public interface ISimpleDLLFeedbackProxy
    {
        ISimpleDLLFeedback Get(string subid);
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace WebAPI.Hubs
{
    public class FeedbackNotifierHub : Hub<IFeedbackNotifier>
    {
        private readonly string subscriberIdTag = "subid";
        private ILogger<FeedbackNotifierHub> Logger { get; }

        public FeedbackNotifierHub(ILogger<FeedbackNotifierHub> logger) => Logger = logger;

        public override async Task OnConnectedAsync()
        {
            var subscriberID = Context.GetHttpContext().Request.Query[subscriberIdTag];
            Context.Items[subscriberIdTag] = subscriberID;
            await Groups.AddToGroupAsync(Context.ConnectionId, subscriberID);

            Logger.LogInformation($"{nameof(OnConnectedAsync)} - Client connected: SubscriberID: {subscriberID}\n");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (exception == null)
            {
                Logger.LogWarning($"{nameof(OnDisconnectedAsync)} - Client disconnected: SubscriberID: {Context.Items[subscriberIdTag]}\n");
            }
            else
            {
                Logger.LogError(exception, 
                                $"{nameof(OnDisconnectedAsync)} - Client disconnected: SubscriberID: {Context.Items[subscriberIdTag]}\n");
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.Items[subscriberIdTag].ToString());
        }
    }
}
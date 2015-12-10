using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ProjectFood.Domain.Entities;

namespace ProjectFood.WebUI.Hubs
{
    public class CommentsHub : Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<CommentsHub>();

        public string CurrentConnectionId
        {
            get { return Context.ConnectionId; }
        }
        public void BroadCastComment()
        {
            Clients.Others.UpdateComments();
        }

        public static void StaticBroadCastComment(Comment comment, string id)
        {
            hubContext.Clients.AllExcept(new[] {id}).UpdateComments(comment);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_api.Models;

namespace ecommerce_api.Services.NotificationService
{
    public interface INotificationService
    {

        public Task SendNotification(string userId, string title, string content);
        public Task MarkAsRead(int notificationId);

        public Task MarkAllAsRead(string userId);

        public Task<List<UserNotification>> GetUserNotifications(string userId);

    }
}
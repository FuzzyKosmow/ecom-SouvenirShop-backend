using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_api.Models;

namespace ecommerce_api.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _context;
        public NotificationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task SendNotification(string userId, string title, string content)
        {
            var notification = new UserNotification
            {
                UserId = userId,
                Title = title,
                Content = content,
                IsRead = false,
                CreatedAt = DateTime.Now
            };

            _context.UserNotifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task MarkAsRead(int notificationId)
        {
            var notification = await _context.UserNotifications.FindAsync(notificationId);
            notification.IsRead = true;
            await _context.SaveChangesAsync();
        }
        public async Task MarkAllAsRead(string userId)
        {
            var notifications = _context.UserNotifications.Where(n => n.UserId == userId).ToList();
            notifications.ForEach(n => n.IsRead = true);
            await _context.SaveChangesAsync();
        }
        public async Task<List<UserNotification>> GetUserNotifications(string userId)
        {
            var notifications = _context.UserNotifications.Where(n => n.UserId == userId).ToList();
            return notifications;
        }
    }
}
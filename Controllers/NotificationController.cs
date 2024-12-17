using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_api.Services.NotificationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        ///  Get user notifications. Typically is sent once every 3 sec to update.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        ///     200: The list of notifications
        /// </returns>
        [HttpGet("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserNotifications(string userId)
        {
            var notifications = await _notificationService.GetUserNotifications(userId);
            // Sort by date
            notifications = notifications.OrderByDescending(n => n.CreatedAt).ToList();
            return Ok(notifications);
        }
        /// <summary>
        /// Mark a notification as read
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns>
        ///      200: Success
        ///      404: Notification not found
        /// </returns>
        [HttpPatch("{notificationId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> MarkAsRead(int notificationId)
        {
            try
            {
                await _notificationService.MarkAsRead(notificationId);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(new { message = e.Message });
            }
        }
        /// <summary>
        ///     Mark all notifications as read
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        ///     200: Success
        ///     404: User not found
        /// </returns>
        [HttpPatch("all/{userId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> MarkAllAsRead(string userId)
        {
            try
            {
                await _notificationService.MarkAllAsRead(userId);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(new { message = e.Message });
            }
        }
    }
}
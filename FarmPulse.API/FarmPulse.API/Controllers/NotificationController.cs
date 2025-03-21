using AutoMapper;
using FarmPulse.API.DTOs;
using FarmPulse.API.Errors;
using FarmPulse.Core;
using FarmPulse.Core.Interfaces;
using FarmPulse.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmPulse.API.Controllers
{
	/// <summary>
	/// Controller to manage chicken-related operations.
	/// Notifications operations including fetching, marking as read, and deletion.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationsController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly INotificationRepository _notificationRepository;
		private readonly IMapper _mapper;


		/// <summary>
		/// Initializes a new instance of the <see cref="NotificationsController"/> class.
		/// </summary>
		/// <param name="unitOfWork">Unit of work for managing database operations.</param>
		/// <param name="notificationRepository">Repository for notifications.</param>
		/// <param name="mapper">AutoMapper instance for mapping entities to DTOs.</param>
		public NotificationsController(IUnitOfWork unitOfWork, INotificationRepository notificationRepository, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_notificationRepository = notificationRepository;
			_mapper = mapper;
		}

		/// <summary>
		/// Retrieves all notifications.
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// GET /api/Notifications/GetAllNotifications
		/// </remarks>
		/// <response code="200">Returns a list of notifications ordered by creation date.</response>
		/// <response code="404">If no notifications are found.</response>
		/// <response code="500">If there is an error while fetching notifications.</response>
		[HttpGet("GetAllNotifications")]
		[ProducesResponseType(typeof(IEnumerable<NotificationDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiRespons), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ApiExceptionResponse), StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<IEnumerable<NotificationDto>>> GetNotifications()
		{
			try
			{
				var notificationRepo = _unitOfWork.Repository<Notification>();
				var notifications = await notificationRepo.GetAllAsync();

				if (notifications == null || !notifications.Any())
					return NotFound(new ApiRespons(404, "No notifications found"));

				var notificationsDto = _mapper.Map<IEnumerable<NotificationDto>>(notifications);
				return Ok(notificationsDto.OrderByDescending(n => n.CreatedAt));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiExceptionResponse(500, "Error fetching notifications", ex.Message));
			}
		}


		/// <summary>
		/// Retrieves all unread notifications.
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// GET /api/Notifications/GetAllUnreadNotifications
		/// </remarks>
		/// <response code="200">Returns a list of unread notifications ordered by creation date.</response>
		/// <response code="404">If no unread notifications are found.</response>
		/// <response code="500">If there is an error while fetching unread notifications.</response>
		[HttpGet("GetAllUnreadNotifications")]
		[ProducesResponseType(typeof(IEnumerable<NotificationDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiRespons), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ApiExceptionResponse), StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<IEnumerable<NotificationDto>>> GetUnreadNotifications()
		{
			try
			{
				var unreadNotifications = await _notificationRepository.GetUnreadNotificationsAsync();

				if (unreadNotifications == null || !unreadNotifications.Any())
					return NotFound(new ApiRespons(404, "No unread notifications found"));

				var unreadNotificationsDto = _mapper.Map<IEnumerable<NotificationDto>>(unreadNotifications);
				return Ok(unreadNotificationsDto.OrderByDescending(n => n.CreatedAt));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiExceptionResponse(500, "Error fetching unread notifications", ex.Message));
			}
		}


		/// <summary>
		/// Marks a specific notification as read.
		/// </summary>
		/// <param name="id">The ID of the notification to mark as read.</param>
		/// <remarks>
		/// Sample request:
		/// PUT /api/Notifications/MarkAsRead/{id}
		/// </remarks>
		/// <response code="204">Notification marked as read successfully.</response>
		/// <response code="404">If the notification with the specified ID is not found.</response>
		/// <response code="500">If there is an error while updating the notification.</response>
		[HttpPut("MarkAsRead/{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(ApiRespons), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ApiExceptionResponse), StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult> MarkAsRead(int id)
		{
			try
			{
				var notificationRepo = _unitOfWork.Repository<Notification>();
				var notification = await notificationRepo.GetByIdAsync(id);

				if (notification == null)
					return NotFound(new ApiRespons(404, "Notification not found"));

				notification.IsRead = true;
				notificationRepo.Update(notification);
				await _unitOfWork.SaveAsync();

				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiExceptionResponse(500, "Error marking notification as read", ex.Message));
			}
		}


		/// <summary>
		/// Deletes a specific notification.
		/// </summary>
		/// <param name="id">The ID of the notification to delete.</param>
		/// <remarks>
		/// Sample request:
		/// DELETE /api/Notifications/DeleteNotification/{id}
		/// </remarks>
		/// <response code="204">Notification deleted successfully.</response>
		/// <response code="404">If the notification with the specified ID is not found.</response>
		/// <response code="500">If there is an error while deleting the notification.</response>
		[HttpDelete("DeleteNotification/{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(ApiRespons), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ApiExceptionResponse), StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult> DeleteNotification(int id)
		{
			try
			{
				var notificationRepo = _unitOfWork.Repository<Notification>();
				var notification = await notificationRepo.GetByIdAsync(id);

				if (notification == null)
					return NotFound(new ApiRespons(404, "Notification not found"));

				notificationRepo.Delete(notification);
				await _unitOfWork.SaveAsync();

				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiExceptionResponse(500, "Error deleting notification", ex.Message));
			}
		}
	}
}

using FarmPulse.Core.Interfaces;
using FarmPulse.Core.Models;
using FarmPulse.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmPulse.Repository.Implementation
{
	public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
	{
		private readonly FarmContext _context;

		public NotificationRepository(FarmContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Notification>> GetUnreadNotificationsAsync()
		{
			return await _context.Notification.Where(n => !n.IsRead).ToListAsync();
		}


	}
}

//using FarmPulse.Service.Interfaces;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FarmPulse.Service
//{
//	public class BackgroundTaskService : IHostedService, IDisposable
//	{
//		private Timer _timer;
//		private readonly TimeSpan _dueTime = TimeSpan.FromMinutes(2); // وقت التأخير في البداية (على سبيل المثال 5 دقائق)
//		private readonly TimeSpan _interval = TimeSpan.FromMinutes(2); // تنفيذ المهمة كل 24 ساعة

//		private readonly IServiceProvider _serviceProvider;

//		public BackgroundTaskService(IServiceProvider serviceProvider)
//		{
//			_serviceProvider = serviceProvider;
//		}

//		public Task StartAsync(CancellationToken cancellationToken)
//		{
//			// تنفيذ المهمة بعد _dueTime ثم تكرارها كل _interval
//			_timer = new Timer(ExecuteTask, null, _dueTime, _interval);
//			return Task.CompletedTask;
//		}

//		private void ExecuteTask(object state)
//		{
//			_ = ExecuteTaskAsync();
//		}

//		private async Task ExecuteTaskAsync()
//		{
//			using (var scope = _serviceProvider.CreateAsyncScope())
//			{
//				var chickenService = scope.ServiceProvider.GetRequiredService<IChickenService>();

//				await chickenService.UpdateAllChickensStatus();
//			}
//		}

//		public Task StopAsync(CancellationToken cancellationToken)
//		{
//			_timer?.Change(Timeout.Infinite, 0);
//			return Task.CompletedTask;
//		}

//		public void Dispose()
//		{
//			_timer?.Dispose();
//		}
//	}

//}
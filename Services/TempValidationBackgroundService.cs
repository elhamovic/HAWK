using HAWK_v.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HAWK_v.Services
{
    public class TempValidationBackgroundService : BackgroundService
    {
        private readonly ILogger<TempValidationBackgroundService> _logger;
        private Timer timer;
        private DateTime date;
        public TempValidationBackgroundService(ILogger<TempValidationBackgroundService> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                HAWKDB hdb = new HAWKDB();
                List<int> departmentList = hdb.GetDepartments();
                for (int i = 0; i < departmentList.Count; i++)
                {
                    List<TempModel> temps = hdb.GetAllTemp(departmentList[i]);
                    for (int j = 0; j < temps.Count; j++)
                    {
                        date = DateTime.Today;
                        if (temps[j].Id != 0)
                        {
                            DateTime TED = DateTime.Parse(temps[j].PEndDate);
                            if (date > TED)
                            {
                                hdb.DeleteTemp(temps[j].Id);
                            }
                        }

                    }
                }
                
                _logger.LogInformation("BackBround service running", DateTime.Now);
                await Task.Delay(TimeSpan.FromMilliseconds(86400), stoppingToken);
            }

        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("BackBround service stop", DateTime.Now);
            return base.StopAsync(stoppingToken);
        }

    }
}

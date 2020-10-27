using Hangfire;
using Hangfire.Storage;
using Hangfire.Storage.Monitoring;
using Maersk.Sorting.Api.Data;
using Maersk.Sorting.Api.Wrapper;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Maersk.Sorting.Api
{
    public class SortJobProcessor : ISortJobProcessor
    {
        private readonly ILogger<SortJobProcessor> _logger;
        private IRepositoryWrappercs _repoWrapper;

        public SortJobProcessor(ILogger<SortJobProcessor> logger, IRepositoryWrappercs repositoryWrappercs)
        {
            _logger = logger;
            _repoWrapper = repositoryWrappercs;
        }

        public async Task<SortJob> Process(SortJob job)
        {
            _logger.LogInformation("Processing job with ID '{JobId}'.", job.SortID);

            var stopwatch = Stopwatch.StartNew();

            var output = job.Input.OrderBy(n => n).ToArray();
            await Task.Delay(5000); // NOTE: This is just to simulate a more expensive operation

            var duration = stopwatch.Elapsed;

            _logger.LogInformation("Completed processing job with ID '{JobId}'. Duration: '{Duration}'.", job.SortID, duration);

            SortJob sj = new SortJob();
            sj.Id = job.Id;
            sj.SortID = job.SortID;
                sj.Status = SortJobStatus.Completed.ToString();
                sj.Duration = duration;
                sj.Input = job.Input;
                sj.Output= job.Output;
            await  _repoWrapper.SortJobRepository.Update(sj);
            _repoWrapper.Save();
            
            return sj;
        }

        public async Task<Task> Enqueue(Expression<Action> methodCall)
        {
            string jobId = BackgroundJob.Enqueue(methodCall);
            Task checkJobState = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    IMonitoringApi monitoringApi = JobStorage.Current.GetMonitoringApi();
                    JobDetailsDto jobDetails = monitoringApi.JobDetails(jobId);
                    string currentState = jobDetails.History[0].StateName;
                    if (currentState != "Enqueued" && currentState != "Processing")
                    {
                        break;
                    }
                    await Task.Delay(100); // adjust to a coarse enough value for your scenario
                }
            });
            return checkJobState;
        }
    }
}

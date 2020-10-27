using Hangfire;
using Maersk.Sorting.Api.Data;
using Maersk.Sorting.Api.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maersk.Sorting.Api.Controllers
{
    [ApiController]
    
    [Route("sort")]
 
    public class SortController : BaseController<SortJob>

    {
        private IRepositoryWrappercs _repoWrapper;
        private readonly ISortJobProcessor _sortJobProcessor;
      
        
        public SortController(ISortJobProcessor sortJobProcessor, IRepositoryWrappercs repositoryWrappercs)
        {
            _sortJobProcessor = sortJobProcessor;
            _repoWrapper = repositoryWrappercs;

           
        }

        [HttpPost("run")]
        [Obsolete("This executes the sort job asynchronously. Use the asynchronous 'EnqueueJob' instead.")]
        public async Task<ActionResult<SortJob>> EnqueueAndRunJob(int[] values)
        {
            var pendingJob = new SortJob()
            {
                SortID = Guid.NewGuid(),
                Status = SortJobStatus.Pending.ToString(),
                Duration = null,
                Input = string.Join(",", values.ToArray()),
                Output = null
            };

            var completedJob = await _sortJobProcessor.Process(pendingJob);

            return Ok(completedJob);
           
        }
        [Route("EnqueAndRun")]
        [HttpPost]
        public async Task<ActionResult<SortJob>> EnqueueJob(int[] values)
        {
            // TODO: Should enqueue a job to be processed in the background.
            var pendingJob = new SortJob()
            {
                Id = Guid.NewGuid(),
                SortID = Guid.NewGuid(),
                Status = SortJobStatus.Pending.ToString(),
                Duration = null,
                Input = string.Join(",", values.ToArray()),
                Output = string.Empty
            };

           

            //return  CreatedAtAction("Get", new { id = pendingJob.Id }, pendingJob);
            if (await _repoWrapper.SortJobRepository.Get(pendingJob.SortID) != null)
            {
                return new ConflictResult();
            }
            else
            {
                // Update the status as pending 
                SortJob completedJob = new SortJob();
                await _repoWrapper.SortJobRepository.Add(pendingJob);
                _repoWrapper.Save();
                await _sortJobProcessor.Enqueue(() => _sortJobProcessor.Process(pendingJob));
                //SortJob completedJob = await _sortJobProcessor.Process(pendingJob);
                // await _repository.Update<SortJob>(completedJob);
   
            }

            return new NoContentResult();


        }
        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable< SortJob>>> GetJobs()
        {
            // TODO: Should return all jobs that have been enqueued (both pending and completed).
            //return await _sortJobDbContext.SortJobC.ToListAsync();
            return await _repoWrapper.SortJobRepository.GetAll();
            
        }

        [HttpGet("{jobId}")]
        public async Task<ActionResult<SortJob>> GetJob(Guid jobId)
        {
            // TODO: Should return a specific job by ID.
            var sortJob = await _repoWrapper.SortJobRepository.Get(jobId);
            if (sortJob == null)
            {
                return NotFound();
            }
            return sortJob;
            //return new NoContentResult();
        }
    }

}

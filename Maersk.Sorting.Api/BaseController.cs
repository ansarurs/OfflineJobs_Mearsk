using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maersk.Sorting.Api.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Maersk.Sorting.Api
{
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
       
       
        
        //public BaseController(T tobj)
        //{
        //    local_obj = tobj;

        //}
        //[Route("GetAll")]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<T>>> GetJobs()
        //{
        //    // TODO: Should return all jobs that have been enqueued (both pending and completed).
        //    //return await _sortJobDbContext.SortJobC.ToListAsync();
        //    return await _repository.GetAll();
            
        //}
        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maersk.Sorting.Api.Data
{
    //This is user class for the repository
    public class SortJobRepositorycs:EfCoreRepository<SortJob>,ISortJobRepository
    {
        public SortJobRepositorycs(SortJobDbContext repositoryContext): base(repositoryContext)
        {

        }
    }
}

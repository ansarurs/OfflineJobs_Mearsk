using Maersk.Sorting.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maersk.Sorting.Api.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrappercs
    {
        private SortJobDbContext _repoContext;
        private ISortJobRepository _SortRepo;


        public ISortJobRepository SortJobRepository
        {
            get
            {
                if (_SortRepo == null)
                {
                    _SortRepo = new SortJobRepositorycs(_repoContext);
                }
                return _SortRepo;
            }
          

        }
        public RepositoryWrapper(SortJobDbContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}

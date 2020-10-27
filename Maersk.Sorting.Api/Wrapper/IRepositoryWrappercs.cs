using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maersk.Sorting.Api.Data;
namespace Maersk.Sorting.Api.Wrapper
{
    // Wrapper is useed when we need logic from multiple classes
    public interface IRepositoryWrappercs
    {
        ISortJobRepository SortJobRepository { get; }
        void Save();
    }
}

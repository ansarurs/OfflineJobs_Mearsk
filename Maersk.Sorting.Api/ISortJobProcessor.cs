using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Maersk.Sorting.Api
{
    public interface ISortJobProcessor
    {
        Task<SortJob> Process(SortJob job);
        Task<Task> Enqueue(Expression<Action> methodCall);
    }
}
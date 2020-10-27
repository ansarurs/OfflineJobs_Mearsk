using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maersk.Sorting.Api.Data
{
    public interface IEntity
    {
       public Guid Id { get; set; }
    }

}

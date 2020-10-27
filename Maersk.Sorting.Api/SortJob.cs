using Maersk.Sorting.Api.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Maersk.Sorting.Api
{
    [Table("SortJob")]
    public class SortJob : IEntity
    {
        //public SortJob(Guid id, SortJobStatus status, TimeSpan? duration, string input, string output)
        //{
        //    Id = id;
        //    Status = status;
        //    Duration = duration;
        //    Input = input;
        //    Ouput = output;
        //}
      
        public Guid SortID { get; set; }
        public string Status { get; set; }
        public TimeSpan? Duration { get; set; }
        public string? Input { get; set; }
        public string? Output { get; set; }
       

        [NotMapped]
        public IReadOnlyCollection<int> Input_data
        {
            get
            {
                return  (string.IsNullOrEmpty( Input)) ? null : Input.Split(',').Select(s => Convert.ToInt32(s)).ToArray();
            }
            set
            {
                Input = string.Join(",", value.ToArray());
            }
        }
        [NotMapped]
        public IReadOnlyCollection<int> Output_data
        {
            get
            {
                return (string.IsNullOrEmpty(Output)) ? null : Output.Split(',').Select(s => Convert.ToInt32(s)).ToArray();
            }
            set
            {
                Output = value !=null? string.Join(",", value.ToArray()): string.Empty;
            }
        }

        public Guid Id { get; set; }



        //int IEntity.Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    }
}

using Maersk.Sorting.Api.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Maersk.Sorting.Api.Entities.Models
{
    [Table("SortJob")]
    public class SortJob : IEntity
    {
        [Required(ErrorMessage = "GUID required")]
        public Guid SortID { get; set; }
       
        public string Status { get; set; }
       
        public TimeSpan Duration { get; set; }

        public string Input { get; set; }
        public string Ouput { get; set; }

        [NotMapped]
        public IReadOnlyCollection<int> Input_data { 
            get
            { 
                return Input.Split(',').Select(s => Convert.ToInt32(s)).ToArray(); 
            } 
            set {
                Input = string.Join(",", value.ToArray()); 
            } 
        }
        [NotMapped]
        public IReadOnlyCollection<int> Output_data
        {
            get
            {
                return Ouput.Split(',').Select(s => Convert.ToInt32(s)).ToArray();
            }
            set
            {
                Ouput = string.Join(",", value.ToArray());
            }
        }

        public Guid Id { get; set; }
    }
}

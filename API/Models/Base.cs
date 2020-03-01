using API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Base : IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime DeleteDate { get; set; }
        public bool IsDeleted { get; set; } = false;

        public void Update()
        {
            UpdateDate = DateTime.Now.ToLocalTime();
        }
        public void Delete()
        {
            IsDeleted = true;
            DeleteDate = DateTime.Now.ToLocalTime();
        }
       
    }
}

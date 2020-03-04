using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class PlacementVM
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        public string Site { get; set; }
        public string JoinDate { get; set; }
        public string Quantity { get; set; }
        public string Name { get; set; }
        public string NIK { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}

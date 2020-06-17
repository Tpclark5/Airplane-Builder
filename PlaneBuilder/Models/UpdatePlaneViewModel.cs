using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaneBuilder.Models
{
    public class UpdatePlaneViewModel
    {
        public string Oldiatacode { get; set; }
        public string OldName { get; set; }
        public int OldEngine_Count { get; set; }
        public string OldEngine_Type { get; set; }
        public double OldAge { get; set; }
        public string OldDescription { get; set; }
        public bool OldDoes_Exist { get; set; }
        public bool OldHave_Ridden { get; set; }
        public string OldRating { get; set; }
        public string OldEmailAddress { get; set; }
        public string OldPlane_Status { get; set; }
        public string OldPicture { get; set; }

        public string Newiatacode { get; set; }
        public string NewName { get; set; }
        public int NewEngine_Count { get; set; }
        public string NewEngine_Type { get; set; }
        public double NewAge { get; set; }
        public string NewDescription { get; set; }
        public bool NewDoes_Exist { get; set; }
        public bool NewHave_Ridden { get; set; }
        public string NewRating { get; set; }
        public string NewEmailAddress { get; set; }
        public string NewPlane_Status { get; set; }
        public string NewPicture { get; set; }

        public int PlaneId { get; set; }
    }
}

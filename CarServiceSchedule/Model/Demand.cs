using System;
namespace CarServiceSchedule.Model
{
    public class Demand
    {
        public int Id { get; set; }
        public DateTime PickUpTime { get; set; }//Earliest
        public DateTime DropOffTime { get; set; }//Latest
        public int PickUpLocation { get; set; }
        public int DropOffLocation { get; set; }
        public virtual CarFeature CarFeature { get; set; }
        public int CarFeatureId { get; set; }
        public virtual User User { get; set; }//?
        public int? UserId { get; set; }
         public DemandStatusType DemandStatus { get; set; }
    }
        public enum DemandStatusType
    {
        Pending,
        Rejected,
        Approved,
        InServe,
        Finished
    }
}
namespace CarServiceSchedule.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public virtual Demand Demand { get; set; }
        public int? DemandId { get; set; }

        public virtual Car Car { get; set; }

        public int? CarId { get; set; }

    }

}
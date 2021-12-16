namespace CarServiceSchedule.Model
{
    public class Car
    {//All props should have derived from separated tables
        public int Id { get; set; }

        // [Key]
        // public int CarId { get; set; } // Id demek yerine bunu diyeceksen Key demen gerekiyor
        public int CurrentLocation { get; set; }

        public int CarFeatureId { get; set; }

        public virtual CarFeature CarFeature { get; set; }

        public CarStatus CarStatus { get; set; }

    }

    public enum CarStatus
    {
        Available,
        InServe
    }

}
namespace CarServiceSchedule.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
    }

    public enum GenderType
    {
        Male,
        Female,
        Other,
    }
}
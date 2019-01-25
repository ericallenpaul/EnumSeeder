namespace EnumSeeder.Models
{
    public interface IEmployee
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        Department Department { get; set; }
    }
}
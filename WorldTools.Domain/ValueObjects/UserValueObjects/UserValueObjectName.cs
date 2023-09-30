
namespace WorldTools.Domain.ValueObjects.UserValueObjects
{
    public class UserValueObjectName
    {
        public UserValueObjectName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        
    }
}

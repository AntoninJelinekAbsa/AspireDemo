using AspireDemo.Models.Interfaces;

namespace AspireDemo.Models.Entities
{
    public class Actor : ISelectableItem
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Value
        {
            get => $"{FirstName} {LastName}";
            set { }
        } 
    }
}

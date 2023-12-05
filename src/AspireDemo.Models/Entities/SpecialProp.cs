using AspireDemo.Models.Interfaces;

namespace AspireDemo.Models.Entities
{
    public class SpecialProp : ISelectableItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Value
        {
            get => Name;
            set { }
        }
    }
}

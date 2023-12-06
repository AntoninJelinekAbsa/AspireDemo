using AspireDemo.Models.Entities;

namespace AspireDemo.Frontend.Models
{
    public class ActorWithIndex
    {
        public Actor Actor { get; set; }
        public int Index { get; set; }
        public bool IsEmpty { get; set; }
    }
}

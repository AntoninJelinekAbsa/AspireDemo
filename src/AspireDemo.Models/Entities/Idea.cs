namespace AspireDemo.Models.Entities
{
    public class Idea
    {
        public int Id { get; set; }
        public string WorkingTitle { get; set; }
        public string Actors { get; set; }
        public string Genre { get; set; }
        public string SpecialProps { get; set; }
        public string Plot { get; set; }
        public bool GreenlightFromBoss { get; set; }
        public string BossNotes { get; set; }
        public string BossReviewResult => GreenlightFromBoss ? "Love it" : "Hate it";
    }
}

namespace Film_Arsiv.Models
{
    public class Comments
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FilmID { get; set; }
        public Film Film { get; set; }
    }
}

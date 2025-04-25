﻿namespace Film_Arsiv.Models
{
    public class Film
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Plot { get; set; }
        public string Poster { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string imdbID { get; set; }
        public ICollection<Comments> Comments { get; set; }
    }
}

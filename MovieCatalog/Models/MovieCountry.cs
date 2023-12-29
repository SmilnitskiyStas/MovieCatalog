﻿namespace MovieCatalog.Models
{
    public class MovieCountry
    {
        public int MovieId { get; set; }
        public int CountryId { get; set; }
        public Movie Movie { get; set; }
        public Country Country { get; set; }
    }
}

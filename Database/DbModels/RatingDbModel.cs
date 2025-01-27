﻿namespace Database.DbModels
{
    public class RatingDbModel
    {
        public int Id { get; set; }
        public UserDbModel User { get; set; }
        public FilmDbModel Film { get; set; }
        public int GeneralImpression { get; set; }
        public int ActorPlay { get; set; }
        public int Scenario { get; set; }
        public int Filming{ get; set; }
    }
}
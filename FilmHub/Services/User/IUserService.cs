﻿using System.Collections.Generic;
using Database.DbModels;
using FilmHub.Models;

namespace FilmHub.Services.User
{
    public interface IUserService
    {
        List<Database.User.User> GetAllUsers();
        int Add(Database.User.User model);
        int Count();
        bool Find(Database.User.User logInModel);
        int CurrentUser_Id(Database.User.User current_user);
        Database.User.User FindById(int id);
        void AddToFavourite(string image, int id);
        public List<Database.Film.Film> RecommendedFilmsDirector(int id);
        public List<Database.Film.Film> RecommendedFilmsGenre(int id);
        void EditProfile(string firstName, string lastName, string email, string dateOfBirth,
            string country, Database.User.User currentUser);
        public void ChangeUserPassword(int id, string newPassword);
        public List<Database.User.User> SimilarUsers(int currentUserId);
        public Database.User.User FindByEmail(string userEmail);
        bool IsExpert(int currentUserId);
        public int ChangedPasswordIsCorrect(int id, string oldPassword, string newPassword, string newPasswordRepeat);
        void AddToAdvised(int currentFilmId, int userIdToAdvise);
        public void MakeAnExpert(int currentUserId);

        static string ErrorMessage;
        
    }
}
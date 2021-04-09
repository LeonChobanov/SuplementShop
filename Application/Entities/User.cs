﻿namespace SuplementShop.Application.Entities
{
    public class User
    {
        public int Id { get; private set; }

        public string Email { get; private set; }

        public string Name { get; private set; }

        public string Password { get; private set; }

        public static User Create(int id, string email, string name, string password)
        {
            return new User
            {
                Id = id,
                Email = email,
                Name = name,
                Password = password
            };
        }
    }
}
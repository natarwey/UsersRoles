﻿namespace UsersRoles.Requests
{
    public class CreateNewUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

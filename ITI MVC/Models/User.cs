﻿namespace ITI_MVC.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public virtual List<Role>? Roles { get; set; }
    }
}

﻿namespace AccessManagement.Models
{
    public class User
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string username { get; set; }

        public string password { get; set; }

        public string role { get; set; }
    }
}

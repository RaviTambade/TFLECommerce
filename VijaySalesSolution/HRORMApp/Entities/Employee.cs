﻿namespace HRORMApp.Entities
{
    //mapped to employees table
    //Step 1: Entity Class
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
    }
}

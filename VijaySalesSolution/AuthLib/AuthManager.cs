﻿namespace AuthLib
{
    public class AuthManager
    {
        public bool Login(string username, string password)
        {
            bool status = false;
            if(username== "ravi" &&  password=="ravi")
            status= true;
            return status;
        }
        public bool Register(string username, string password, string email, string contact)
        {
            //
            return false;
        }
    }
}

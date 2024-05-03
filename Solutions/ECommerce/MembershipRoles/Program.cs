// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


AuthController
       Login
       Register
       ForgotPassword
       ChangePassowrd

Entities
       Claim
             loginame
             password

Services
IAuthService
AuthService
       Authenticate{}
       InsertNewUserCredential
       ResetPassword

Repositories
IMembershipRepo
MembershipRepo
        Authenticate
       InsertNewUserCredential
       ResetPassword
   
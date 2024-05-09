namespace Transflower.Entities;

public class Employee
{
    public int  Id {get;set;}
    public string  FirstName {get;set;}
    public string LastName {get;set;}
    public string Email {get;set;}
    public string Contact {get;set;}

    override public string ToString(){
    return ("id"+Id+" "+FirstName + " "+ LastName + " "+ Contact + " "+ Email);
}
}
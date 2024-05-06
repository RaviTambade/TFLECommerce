namespace Transflower.HR;

//POCO Object: Plain Old CLR Object
public class Employee{
    public int Id { get; set;}
    public string FirstName{get;set;}
    public string LastName{get;set;}
    public string Contact{get;set;}
 
    public string Email{get;set;}

override public string ToString(){
    return ""+Id+" "+FirstName + " "+ LastName + " "+ Contact + " "+ Email;
}
}

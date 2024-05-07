namespace Transflower.ECommerce.HR.Entities;

//POCO Object: Plain Old CLR Object
public class Member{
    public int Id { get; set;}
    public string FirstName{get;set;}
    public string LastName{get;set;}
    public string Contact{get;set;}
 
    public string Email{get;set;}

override public string ToString(){
    return (""+Id+" "+FirstName + " "+ LastName + " "+ Contact + " "+ Email);
}
}

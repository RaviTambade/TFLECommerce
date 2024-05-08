
//simple comment
/*
Programmer: Ravi Tamade
Company: Transflower
email:ravi.tambade@transflower.in
contact:9881735801
 */

using Transflower.Entities;
using Transflower.Helpers;
using Transflower.Utility.Maths;

int count = 56;
bool status=false;
char key = 'O';
string fullName = "Ravi Tambade";
double  unitPrice =56.67;
 
var person = new Person();

person.Id = 56;
person.FirstName = "Manoj";
person.LastName = "Patil";
person.Contact = "9883456783";
person.Email = "manoj.patil@gmail.com";


Person person2=HRManager.GetPerson(45);

const string name = "Transflower";

MathEngine algebra = new MathEngine("Baskaraycharya");


int num1 = 56;
int num2 = 100;
Console.WriteLine(num1 +"  " + num2);
algebra.Swap(ref num1, ref num2);
Console.WriteLine(num1 + "  " + num2);

int radius = 10;
double area, circumference;
algebra.Calculate(radius, out area, out circumference);

Console.WriteLine(area + "  " + circumference);









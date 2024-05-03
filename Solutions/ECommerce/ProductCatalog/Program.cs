
//Entry Point Main Function
using System.Collections.Generic;
using System.Reflection;
using Transflower.ECommerce.ProductCatalog.Entities;
using Transflower.ECommerce.ProductCatalog.Repositories;
using Transflower.ECommerce.ProductCatalog.Repositories.Interfaces;
using  Transflower.ECommerce.ProductCatalog.Services.Interfaces;
using  Transflower.ECommerce.ProductCatalog.Services;
using Transflower.ECommerce.ProductCatalog.Controllers;
int count=45;
string name= "Transflower";
bool status= false;
float Price= 0.0f;


int [] marks={45,46,46,76,544};
string [] names={ "India", "China", "USA", "UK"};

List<string> cities = new List<string>();
cities.Add("Pune");
cities.Add("Mumbai");
cities.Add ("Chennai");


Console.WriteLine( name, count);
for(int i=0; i<marks.Length; i++){
    Console.WriteLine(marks[i]);
}

Product product= new Product();
product.Id = 20;
product.Title = "Car";

Product product1 = new Product(25 , "Car", "Truck", "four wheeler", 200000, 100);

Console.WriteLine("Id: "+ product.Id + "title :" + product.Title + " description : " + product.Description + " price : " + product.UnitPrice + "catagory :" + product.Category + "StockAvailable :" + product.StockAvailable);

Console.WriteLine("Id: "+ product1.Id + "title :" + product1.Title + " description : " + product1.Description + " price : " + product1.UnitPrice + "catagory :" + product1.Category + "StockAvailable :" + product1.StockAvailable);

//Design Pattern:
//MVC Architecture, Framework, Design Pattern,

//Logic------Controller
//data--------Model
//presentation----View

//Creating repository
IProductRepository repo=new MobilePhoneRepository();
//IProductRepository repo=new FlowerRepository();
IProductService  service=new ProductService(repo);

//fetch logic
//List<Product> allProducts=repo.GetAll();

Console.WriteLine("Please check delay now...");
//List<Product> allProducts=await service.GetAll();
Console.WriteLine("Now result is achieved......");

// delegate to Controller
ProductController controller = new ProductController(service);
List<Product> allProducts = await controller.List();

//Presentation logic
foreach( Product p in allProducts){
    Console.WriteLine(p.Id + "" + p.Title + " "+ p.Description);
}

//functions:
//blocking call :  Synchronous function
//non blocking call : ASynchronus Function
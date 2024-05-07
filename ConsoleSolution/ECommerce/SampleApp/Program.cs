using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.Entities;
using HR.Operations;

namespace SampleApp
{
    internal class Program
    {
        //SOC: Separation of Concern

       //Entry Point function
        static void Main(string[] args)
        {
            string name = "Transflower";
            Console.WriteLine(name);

            Person person = new Person();//
            //person----->refence pointing object
            //Person---->a Class defined state and behaviour 

            Console.WriteLine(person); //will automatically invoke ToString() method
            
            HRManager manager = new HRManager();

            manager.Insert();
            manager.Update();
            manager.Remove();   
            Console.ReadLine();
        }
    }
}

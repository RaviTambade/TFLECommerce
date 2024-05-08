using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transflower.Utility.Maths
{
    public  class MathEngine
    {

        private readonly  string mentor;

        public MathEngine(string author)
        {
            mentor = author;

        }

        //public  void Swap(int x, int y)  //pass by Value
        public void Swap(ref int x, ref int y)  //pass by reference
        {
            int temp=x; 
            x=y;
            y=temp;
        }

        public int Add(int x, int y)
        {
            return x + y;
        }
        public int Subract(int x, int y)
        {
            return x - y;
        }

        public int Multiply(int x, int y)
        {
            return x*y;
        }
        public int Divide(int x, int y)
        {
            return x /y;
        }

        public void  Calculate(int radious, out double  area, out double circumference)
        {
             area = 3.14 * radious * radious;
             circumference = 3.14 * radious * 2;
        }


    }
}

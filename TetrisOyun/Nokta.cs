using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisOyun
{
    class Nokta
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Nokta()
        {
        }
        public Nokta(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadReconstruction
{
    internal class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public override string ToString()
        {
            return $"{First} {Second}";
        }
    }
}

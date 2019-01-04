using System.Collections.Generic;
using System.Linq;

namespace NewsServer.Classes
{
    public class DoubleValueArrayComparer : IEqualityComparer<double[]>
    {
        public bool Equals(double[] x, double[] y)
        {
            return x.SequenceEqual(y);
        }

        public int GetHashCode(double[] obj)
        {
            return string.Join(",", obj).GetHashCode();
        }
    }
}
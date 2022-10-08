using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericMorris
{
    public interface IPointManager
    {
        List<string> GetListofValidPoints();
        List<List<string>> GetAllPossibleMills(string point);
        List<string> GetAdjacentPoints(string point);
    }
}

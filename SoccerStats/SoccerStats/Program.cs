using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SoccerStats
{
    class Program
    {
        static void Main(string[] args)
        {

            string currentDircetory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDircetory);

            var files = directory.GetFiles();

            foreach (var file in files)
            {
                Console.WriteLine(file.Name);
            }
            Console.Read();
        }
    }
}

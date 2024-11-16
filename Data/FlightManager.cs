using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHybridApp.Components.Pages
{
    public class FlightManager
    {

        private void test()
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(FileSystem.Current.AppDataDirectory, "T_T.csv"), append: true))
            {
                Debug.WriteLine("##: ##: Using StreamWriter! Ready to append to: " + Path.Combine(FileSystem.Current.AppDataDirectory, "T_T.csv"));
                writer.WriteLine("asdf");
            }
        }
    }

    
}

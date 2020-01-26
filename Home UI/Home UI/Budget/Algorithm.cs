using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home_UI.Budget
{
    class Algorithm
    {
        public Algorithm()
        {
            GetOnComputers();
        }

        private void GetOnComputers()
        {
            Dictionary<int, string> compStats = new Dictionary<int, string>();

            for (int count = 1; count <= 100; count++)
            {
                if (count % 2 == 0)
                {
                    compStats.Add(count, "Off");
                }
                else
                {
                    compStats.Add(count, "On");
                }
            }

            foreach (KeyValuePair<int, string> kvp in compStats)
            {
                if ((kvp.Key - 4) % 3 == 0)
                {
                    if (compStats.Where(k => k.Key == kvp.Key).FirstOrDefault().Value == "On")
                    {
                        compStats[kvp.Key] = "Off";
                    }
                    else
                    {
                        compStats[kvp.Key] = "On";
                    }
                }
            }

            foreach (KeyValuePair<int, string> kvp in compStats.Where(k => k.Value == "On"))
            {
                Console.WriteLine(kvp.Key.ToString() + " : " + kvp.Value);
            }
            
        }
    }


}

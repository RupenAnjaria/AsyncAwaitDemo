using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo
{
    public class weatherResult
    {
        public string Temperature { get; set; }
        public string Condition { get; set; }
        public int Rain { get; set; }
        public string Wind { get; set; }

        public override string ToString()
        {
            return $"Temperature: {Temperature}\nCondition: {Condition}\nWind: {Wind}\nRain: {Rain.ToString()}";
        }
    }

    public class weatherResultWithAlert : weatherResult
    {
        public string Alert { get; set; }

        public override string ToString()
        {
            string baseString = base.ToString();
            return string.IsNullOrEmpty(Alert) ? baseString : $"{baseString}\nAlert: {Alert}";
        }
    }
}

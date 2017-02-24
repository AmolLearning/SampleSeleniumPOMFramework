using System;
using System.Collections.Generic;
using System.Linq;
using LumenWorks.Framework.IO.Csv;
using System.IO;
using NUnit.Framework;

namespace SampleSeleniumPOMFramework.Common
{
     public class DataSources
    {

    
          
        private IEnumerable<String[]> SearchTermsFromCSV()
        {
            using (var csv = new CsvReader(new StreamReader(@"Data\SearchTerms.csv"), true))
            {
                while (csv.ReadNextRecord())
                {
                    string location = csv[0].ToString();
                    string checkinDate = csv[1].ToString();
                    string checkOutDate = csv[2].ToString();
                    string NumOfAdults = csv[3].ToString();

                    yield return new[] { location, checkinDate,checkOutDate,NumOfAdults };
                }

            }
        }
    
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NUnit.Framework;

namespace SampleSeleniumPOMFramework.Common
{
    class Logger
    {



        public static NLog.Logger Log

        {


            get
            {
                return LogManager.GetLogger(TestContext.CurrentContext.Test.Name);



            }

        }
    }

}

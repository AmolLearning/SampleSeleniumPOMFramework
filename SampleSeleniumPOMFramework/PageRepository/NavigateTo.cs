using SampleSeleniumPOMFramework.PageRepository;
using SampleSeleniumPOMFramework.Common;

namespace SampleSeleniumPOMFramework.PageRepository
{
    public static class NavigateTo
    {


        public static AppPage AppPage
        {
            get
            {
                var AppPg = new AppPage(DriverUtil.driver);
                return AppPg;
            }
        }

        public static AppPage1 AppPage1
        {
            get
            {
                var Page1 = new AppPage1(DriverUtil.driver);
                return Page1;
            }
        }

        public static AppPage2 AppPage2
        {
            get
            {
                var Page2 = new AppPage2(DriverUtil.driver);
                return Page2;
            }
        }


        public static HotelsPage HotelPg
        {
            get
            {
                var _Hotelpg = new HotelsPage(DriverUtil.driver);
                return _Hotelpg;
            }
        }


    }

}


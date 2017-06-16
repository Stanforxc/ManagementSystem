using ManagementSystem.App_Start;
using System.Web.Http;

namespace ManagementSystem
{
    public class BootStrapper
    {
        public static void Run()
        {
            AutoFacWithWebApi.Initialize(GlobalConfiguration.Configuration);
        }

    }
}
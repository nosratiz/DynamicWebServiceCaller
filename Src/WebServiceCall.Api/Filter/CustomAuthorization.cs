using Hangfire.Dashboard;

namespace WebServiceCall.Api.Filter
{
    public class CustomAuthorization : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}
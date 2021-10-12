using Microsoft.Extensions.DependencyInjection;

namespace HotelMgt.API.Policies
{
    public static class AuthorizationMiddleWare
    {
        public static void AddPolicyAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(configure =>
            {
                configure.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                configure.AddPolicy(Policies.HotelManager, Policies.HotelManagerPolicy());
                configure.AddPolicy(Policies.Customer, Policies.CustomerPolicy());
                configure.AddPolicy(Policies.AdminAndHotelManager, Policies.AdminAndHotelManagerPolicy());
                configure.AddPolicy(Policies.HotelManagerAndCustomer, Policies.HotelManagerAndCustomerPolicy());
            });
        }
    }
}

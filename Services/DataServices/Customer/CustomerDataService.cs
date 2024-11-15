using Core.DataService;
using Services.DataContext;
using Services.Domain.Customer;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataServices.Customer
{
    public class CustomerDataService : EntityContextDataService<CustomerDataModel>
    {
        public CustomerDataService(AppDbContext dbContext) : base(dbContext)
        {
            DbContext.Database.CommandTimeout = SiteSettings.DbTimeOut;
        }
    }
}

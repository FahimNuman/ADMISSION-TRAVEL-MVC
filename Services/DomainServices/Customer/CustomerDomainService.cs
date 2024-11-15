using Core.DomainService;
using Services.DataServices.Customer;
using Services.Domain.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainServices
{
    public class CustomerDomainService : DomainService<CustomerDataModel, long>
    {
        private readonly CustomerDataService _customerDataService;
        public CustomerDomainService(CustomerDataService customerDataService) : base(customerDataService)
            {
            _customerDataService = customerDataService;
        }

    }
}

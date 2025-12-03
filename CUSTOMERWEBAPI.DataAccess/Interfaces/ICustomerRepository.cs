using CUSTOMERWEBAPI.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMERWEBAPI.DataAccess.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>>GetAllCustomersAsync();
        Task<Customer?>GetCustomerByIdAsync (int id);
        Task<int>AddCustomerAsync (Customer customer);
        Task<bool>UpdateCustomerAsync (Customer customer);
        Task<bool>DeleteCustomerAsync (int id);
    }
}

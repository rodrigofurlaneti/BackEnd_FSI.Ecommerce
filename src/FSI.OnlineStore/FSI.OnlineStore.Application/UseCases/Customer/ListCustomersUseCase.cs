using FSI.OnlineStore.Application.Dtos.Customer;
using FSI.OnlineStore.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.OnlineStore.Application.UseCases.Customer
{
    public sealed class ListCustomersUseCase
    {
        private readonly ICustomerRepository _customerRepository;

        public ListCustomersUseCase(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IReadOnlyCollection<CustomerResponse>> ExecuteAsync(CancellationToken ct)
        {
            var customers = await _customerRepository.ListAsync(ct);

            var result = customers
                .Select(c => new CustomerResponse
                {
                    CustomerId = c.CustomerId,
                    CustomerTypeId = c.CustomerTypeId,
                    FullName = c.FullName,
                    EmailAddress = c.EmailAddress,
                    CpfNumber = c.CpfNumber,
                    IsActive = c.IsActive,
                    Company = null // lista não traz detalhe da empresa, só o básico
                })
                .ToList()
                .AsReadOnly();

            return result;
        }
    }
}
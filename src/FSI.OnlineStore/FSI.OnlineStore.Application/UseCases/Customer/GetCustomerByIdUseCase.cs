using FSI.OnlineStore.Application.Dtos.Customer;
using FSI.OnlineStore.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.OnlineStore.Application.UseCases.Customer
{
    public sealed class GetCustomerByIdUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICompanyCustomerRepository _companyCustomerRepository;

        public GetCustomerByIdUseCase(
            ICustomerRepository customerRepository,
            ICompanyCustomerRepository companyCustomerRepository)
        {
            _customerRepository = customerRepository;
            _companyCustomerRepository = companyCustomerRepository;
        }

        public async Task<CustomerResponse?> ExecuteAsync(uint customerId, CancellationToken ct)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId, ct);
            if (customer is null)
            {
                return null;
            }

            var company = await _companyCustomerRepository.GetByCustomerIdAsync(customerId, ct);

            var response = new CustomerResponse
            {
                CustomerId = customer.CustomerId,
                CustomerTypeId = customer.CustomerTypeId,
                FullName = customer.FullName,
                EmailAddress = customer.EmailAddress,
                CpfNumber = customer.CpfNumber,
                IsActive = customer.IsActive,
                Company = company is null
                    ? null
                    : new CompanyInfoResponse
                    {
                        CorporateName = company.CorporateName,
                        TradeName = company.TradeName,
                        CnpjNumber = company.CnpjNumber,
                        StateRegistration = company.StateRegistration
                    }
            };

            return response;
        }
    }
}
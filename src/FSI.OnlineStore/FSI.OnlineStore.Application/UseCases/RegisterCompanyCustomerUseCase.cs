using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FSI.OnlineStore.Application.Dtos;
using FSI.OnlineStore.Domain.Entities;
using FSI.OnlineStore.Domain.Repositories;

namespace FSI.OnlineStore.Application.UseCases
{
    public sealed class RegisterCompanyCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerTypeRepository _customerTypeRepository;
        private readonly ICompanyCustomerRepository _companyCustomerRepository;

        public RegisterCompanyCustomerUseCase(
            ICustomerRepository customerRepository,
            ICustomerTypeRepository customerTypeRepository,
            ICompanyCustomerRepository companyCustomerRepository)
        {
            _customerRepository = customerRepository;
            _customerTypeRepository = customerTypeRepository;
            _companyCustomerRepository = companyCustomerRepository;
        }

        public async Task<uint> ExecuteAsync(RegisterCompanyCustomerRequest request, CancellationToken ct)
        {
            var existing = await _customerRepository.GetByEmailAsync(request.EmailAddress, ct);
            if (existing is not null)
            {
                throw new InvalidOperationException("Email already registered.");
            }

            var companyType = await _customerTypeRepository.GetByNameAsync("Company", ct)
                             ?? throw new InvalidOperationException("Customer type 'Company' not found.");

            var passwordHash = HashPassword(request.Password);

            var customer = new Domain.Entities.Customer(
                companyType.CustomerTypeId,
                request.FullName,
                request.EmailAddress,
                passwordHash,
                null);

            var customerId = await _customerRepository.InsertAsync(customer, ct);

            var company = new CompanyCustomer(
                customerId,
                request.CorporateName,
                request.CnpjNumber,
                request.TradeName,
                request.StateRegistration);

            await _companyCustomerRepository.InsertAsync(company, ct);

            return customerId;
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }
    }
}

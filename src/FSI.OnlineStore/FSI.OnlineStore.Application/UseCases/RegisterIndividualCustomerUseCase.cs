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
    public sealed class RegisterIndividualCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerTypeRepository _customerTypeRepository;

        public RegisterIndividualCustomerUseCase(
            ICustomerRepository customerRepository,
            ICustomerTypeRepository customerTypeRepository)
        {
            _customerRepository = customerRepository;
            _customerTypeRepository = customerTypeRepository;
        }

        public async Task<uint> ExecuteAsync(RegisterIndividualCustomerRequest request, CancellationToken ct)
        {
            var existing = await _customerRepository.GetByEmailAsync(request.EmailAddress, ct);
            if (existing is not null)
            {
                throw new InvalidOperationException("Email already registered.");
            }

            var individualType = await _customerTypeRepository.GetByNameAsync("Individual", ct)
                               ?? throw new InvalidOperationException("Customer type 'Individual' not found.");

            var passwordHash = HashPassword(request.Password);

            var customer = new Domain.Entities.Customer(
                individualType.CustomerTypeId,
                request.FullName,
                request.EmailAddress,
                passwordHash,
                request.CpfNumber);

            var id = await _customerRepository.InsertAsync(customer, ct);
            return id;
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
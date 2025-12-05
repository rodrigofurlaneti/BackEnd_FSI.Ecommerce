using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FSI.OnlineStore.Application.Dtos;
using FSI.OnlineStore.Application.Dtos.Customer;
using FSI.OnlineStore.Application.UseCases;
using FSI.OnlineStore.Application.UseCases.Customer;

namespace FSI.OnlineStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class CustomersController : ControllerBase
    {
        private readonly RegisterIndividualCustomerUseCase _registerIndividualCustomerUseCase;
        private readonly RegisterCompanyCustomerUseCase _registerCompanyCustomerUseCase;
        private readonly GetCustomerByIdUseCase _getCustomerByIdUseCase;
        private readonly ListCustomersUseCase _listCustomersUseCase;

        public CustomersController(
            RegisterIndividualCustomerUseCase registerIndividualCustomerUseCase,
            RegisterCompanyCustomerUseCase registerCompanyCustomerUseCase,
            GetCustomerByIdUseCase getCustomerByIdUseCase,
            ListCustomersUseCase listCustomersUseCase)
        {
            _registerIndividualCustomerUseCase = registerIndividualCustomerUseCase;
            _registerCompanyCustomerUseCase = registerCompanyCustomerUseCase;
            _getCustomerByIdUseCase = getCustomerByIdUseCase;
            _listCustomersUseCase = listCustomersUseCase;
        }

        [HttpPost("individual")]
        public async Task<IActionResult> RegisterIndividual(
            [FromBody] RegisterIndividualCustomerRequest request,
            CancellationToken ct)
        {
            var id = await _registerIndividualCustomerUseCase.ExecuteAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id }, new { CustomerId = id });
        }

        [HttpPost("company")]
        public async Task<IActionResult> RegisterCompany(
            [FromBody] RegisterCompanyCustomerRequest request,
            CancellationToken ct)
        {
            var id = await _registerCompanyCustomerUseCase.ExecuteAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id }, new { CustomerId = id });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(uint id, CancellationToken ct)
        {
            CustomerResponse? customer = await _getCustomerByIdUseCase.ExecuteAsync(id, ct);
            if (customer is null) return NotFound();
            return Ok(customer);
        }

        [HttpGet]
        public async Task<IActionResult> List(CancellationToken ct)
        {
            var list = await _listCustomersUseCase.ExecuteAsync(ct);
            return Ok(list);
        }
    }
}

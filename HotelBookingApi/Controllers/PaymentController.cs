using HotelBookingApi.Models;
using HotelBookingApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var payments = await _paymentService.GetAllPayments(cancellationToken);
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var payment = await _paymentService.GetPaymentById(id, cancellationToken);
            if (payment == null)
                return NotFound("Payment not found.");
            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Payment payment, CancellationToken cancellationToken)
        {
            var createdPayment = await _paymentService.CreatePayment(payment, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = createdPayment.Id }, createdPayment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Payment payment, CancellationToken cancellationToken)
        {
            var updatedPayment = await _paymentService.UpdatePayment(id, payment, cancellationToken);
            if (updatedPayment == null)
                return NotFound("Payment not found.");
            //return Ok(updatedPayment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var isDeleted = await _paymentService.DeletePayment(id, cancellationToken);
            if (!isDeleted)
                return NotFound("Payment not found.");
            return NoContent();
        }
    }
}



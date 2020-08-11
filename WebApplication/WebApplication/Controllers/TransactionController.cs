using System.Collections.Generic;
using System.Threading;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Operations;
using Operations.Exceptions;
using Operations.Models.BindingModels;
using Operations.Models.ViewModels;

namespace WebApplication.Controllers
{
    [Route("transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly OperationsContext _operationsContext;
        
        public TransactionController(OperationsContext operationsContext)
        {
            _operationsContext = operationsContext;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_operationsContext.TransationOperations.Get());
        }
        
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]string id)
        {
            try
            {
                return Ok(_operationsContext.TransationOperations.Get(id));
            }
            catch (TransactionNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        

        [HttpPost]
        public IActionResult PlaceTransaction([FromBody] TransactionBindingModel transaction)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(_operationsContext.TransationOperations.Create(transaction));
                }

                return ValidationProblem();
            }
            catch (InvalidTransactionException ex)
            {
                return ValidationProblem(ex.Message);
            }
        }
    }
}
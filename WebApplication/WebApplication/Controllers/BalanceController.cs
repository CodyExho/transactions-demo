using Microsoft.AspNetCore.Mvc;
using Operations;

namespace WebApplication.Controllers
{
    [Route("balance")]
    public class BalanceController : Controller
    {
        private readonly OperationsContext _operationsContext;
        
        public BalanceController(OperationsContext operationsContext)
        {
            _operationsContext = operationsContext;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_operationsContext.BalanceOperations.GetBalance());
        }
    }
}
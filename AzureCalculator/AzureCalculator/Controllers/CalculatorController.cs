using Microsoft.AspNetCore.Mvc;
using AzureCalculator.Models;

namespace AzureCalculator.Controllers
{
    public class CalculatorController : Controller
    {

        private readonly IConfiguration _configuration;

        public CalculatorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.CalculatorName =
                _configuration["CALCULATOR_NAME"] ?? "Calculator";

            return View(new CalculatorModel());
        }

        [HttpPost]
        public IActionResult Index(CalculatorModel model) 
        {
            switch (model.Operation)
            {
                case "+":
                    model.Result = model.Number1 + model.Number2;
                    break;

                case "-":
                    model.Result = model.Number1 - model.Number2;
                    break;

                case "*":
                    model.Result = model.Number1 * model.Number2;
                    break;

                case "/":
                    if (model.Number2 == 0)
                    {
                        model.ErrorMessage = "Cant divide by zero";
                    }
                    else 
                    {
                        model.Result = model.Number1 / model.Number2;
                    }
                    break;

                default:
                    model.ErrorMessage = "Retry operation";
                    break;
                        
            }

            ViewBag.CalculatorName =
                _configuration["CALCULATOR_NAME"] ?? "Calculator";

            return View(model);
        }
    }
}

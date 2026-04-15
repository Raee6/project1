using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(string gender, double age, double weight, double height)
    {
        var result = BMICalculator.Calculate(gender, age, weight, height);
        
        if (!result.IsValid)
        {
            ViewBag.Error = result.Error;
            return View();
        }

        return View(result);
    }
}
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class BMIController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Redirect("/index.html");
    }

    [HttpPost("calculate")]
    public IActionResult Calculate(string gender, string age, string weight, string height)
    {
        double a = double.TryParse(age, out var aVal) ? aVal : 0;
        double w = double.TryParse(weight, out var wVal) ? wVal : 0;
        double h = double.TryParse(height, out var hVal) ? hVal : 0;

        var result = BMICalculator.Calculate(gender ?? "", a, w, h);

        if (!result.IsValid)
        {
            string errorHtml = $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>BMI CHECKER</title>
    <link rel=""stylesheet"" href=""/css/style.css"">
</head>
<body>
    <div class=""container"">
        <h1><i class=""fas fa-weight-scale""></i> BMI CHECKER</h1>
        <p style=""color:red"">{result.Error}</p>
        <form action=""/api/bmi/calculate"" method=""post"">
            <div class=""input-group"">
                <label>Gender</label>
                <select name=""gender"">
                    <option value="""">Select</option>
                    <option value=""male"">Male</option>
                    <option value=""female"">Female</option>
                </select>
            </div>
            <div class=""input-group"">
                <label>Age</label>
                <input type=""number"" name=""age"" placeholder=""Input age"">
            </div>
            <div class=""input-group"">
                <label>Weight (kg)</label>
                <input type=""number"" name=""weight"" placeholder=""kg"">
            </div>
            <div class=""input-group"">
                <label>Height (cm)</label>
                <input type=""number"" name=""height"" placeholder=""cm"">
            </div>
            <button type=""submit"">Calculate</button>
        </form>
    </div>
</body>
</html>";
            return Content(errorHtml, "text/html");
        }

        string resultHtml = $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>BMI CHECKER</title>
    <link rel=""stylesheet"" href=""/css/style.css"">
</head>
<body>
    <div class=""container"">
        <h1><i class=""fas fa-weight-scale""></i> BMI CHECKER</h1>
        <form action=""/api/bmi/calculate"" method=""post"">
            <div class=""input-group"">
                <label>Gender</label>
                <select name=""gender"">
                    <option value=""{gender}"">{gender}</option>
                    <option value=""male"">Male</option>
                    <option value=""female"">Female</option>
                </select>
            </div>
            <div class=""input-group"">
                <label>Age</label>
                <input type=""number"" name=""age"" value=""{age}"">
            </div>
            <div class=""input-group"">
                <label>Weight (kg)</label>
                <input type=""number"" name=""weight"" value=""{weight}"">
            </div>
            <div class=""input-group"">
                <label>Height (cm)</label>
                <input type=""number"" name=""height"" value=""{height}"">
            </div>
            <button type=""submit"">Calculate</button>
        </form>
        <div class=""result {result.ColorClass} show"">
            <div class=""bmi-value"">{result.Bmi}</div>
            <div class=""category"">{result.Category}</div>
            <div class=""calc-info"">{result.CalcInfo}</div>
        </div>
    </div>
</body>
</html>";

        return Content(resultHtml, "text/html");
    }
}
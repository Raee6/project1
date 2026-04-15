public class BMIResult
{
    public string Bmi { get; set; } = "";
    public string Category { get; set; } = "";
    public string ColorClass { get; set; } = "";
    public string CalcInfo { get; set; } = "";
    public bool IsValid { get; set; }
    public string Error { get; set; } = "";
}

public static class BMICalculator
{
    public static BMIResult Calculate(string gender, double age, double weight, double heightCm)
    {
        if (string.IsNullOrEmpty(gender) || 
            double.IsNaN(age) || double.IsNaN(weight) || double.IsNaN(heightCm) || 
            age < 1 || weight <= 0 || heightCm <= 0)
        {
            return new BMIResult 
            { 
                IsValid = false, 
                Error = "Please fill all fields" 
            };
        }

        double height = heightCm / 100;
        double bmiValue = weight / (height * height);
        string bmi = bmiValue.ToString("F1");

        string category = "";
        string colorClass = "";
        
        if (bmiValue < 18.5) { category = "Underweight"; colorClass = "moderate"; }
        else if (bmiValue < 25) { category = "Normal"; colorClass = "normal"; }
        else if (bmiValue < 30) { category = "Overweight"; colorClass = "moderate"; }
        else { category = "Obese"; colorClass = "obese"; }

        string calcInfo = $"Weight: {weight}kg ÷ ({height:F2}m²) = {bmi}<br>Age: {age} | {gender}";

        return new BMIResult
        {
            Bmi = bmi,
            Category = category,
            ColorClass = colorClass,
            CalcInfo = calcInfo,
            IsValid = true
        };
    }
}
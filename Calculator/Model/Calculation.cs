namespace Calculator.Model
{
    public class Calculation
    {
        string leftValue = "0";
        string rightValue = "";
        string operation = "";
        string calculationText = "";
        string resultText = "";

        public string CalculationText { get { return calculationText; } }
        public string ResultText { get { return resultText; } }

        public void UpdateText(string value)
        {
            if (char.IsDigit(value[0]))
            {
                if (operation == string.Empty)
                    UpdateNumber(ref leftValue, value);
                else
                    UpdateNumber(ref rightValue, value);
            }
            else if (value == "C")
            {
                ClearValues();
            }
            else if (value == "=" && leftValue != "")
            {
                Calculate();
                rightValue = "";
                operation = "";
            }
            else if (value == "D")
            {
                if (operation == string.Empty)
                {
                    string temp = "";
                    for (int i = 0; i < leftValue.Length - 1; i++)
                        temp += leftValue[i];
                    leftValue = temp;
                    if (leftValue == string.Empty)
                        leftValue = "0";
                    resultText = leftValue;
                }
                else
                {
                    string temp = "";
                    for (int i = 0; i < rightValue.Length - 1; i++)
                        temp += rightValue[i];
                    rightValue = temp;
                    if (rightValue == string.Empty)
                        rightValue = "0";
                    resultText = rightValue;
                }
            }
            else if (value == ",")
            {
                if (operation != "")
                {
                    if (rightValue.Contains(","))
                        return;
                    rightValue += ",";
                    resultText = rightValue;
                }
                else
                {
                    if (leftValue.Contains(","))
                        return;
                    leftValue += ",";
                    resultText = leftValue;
                }
            }
            else if (value == "+/-")
            {
                if (operation == string.Empty)
                {
                    leftValue = (double.Parse(leftValue) - double.Parse(leftValue) * 2).ToString();
                    resultText = leftValue;
                }
                else
                {
                    rightValue = (double.Parse(rightValue) - double.Parse(rightValue) * 2).ToString();
                    resultText = rightValue;
                }
            }
            else if (value == "%")
            {
                if (operation != string.Empty && rightValue != string.Empty)
                {
                    rightValue = (double.Parse(rightValue) * double.Parse(leftValue) / 100).ToString();
                    resultText = rightValue;
                }
            }
            else
            {
                if (rightValue == "")
                {
                    operation = value;
                    calculationText = leftValue + " " + operation;
                    resultText = "0";
                    return;
                }
                if (operation != "")
                {
                    Calculate();
                    rightValue = "";
                }
                operation = value;
                calculationText = leftValue + " " + operation;
                resultText = "0";
            }
        }

        private void Calculate()
        {
            double num1 = double.Parse(leftValue);
            double num2 = 0;
            if (rightValue != "")
                num2 = double.Parse(rightValue);
            double result;
            switch (operation)
            {
                case "+":
                    result = num1 + num2;
                    break;

                case "-":
                    result = num1 - num2;
                    break;

                case "*":
                    result = num1 * num2;
                    break;

                case "/":
                    if (num2 == 0)
                    {
                        ClearValues();
                        resultText = "Деление на ноль невозможно";
                        return;
                    }
                    result = num1 / num2;
                    break;

                default:
                    result = num1;
                    break;
            }
            calculationText = leftValue + " " + operation + " " + rightValue + " =";
            resultText = result.ToString();
            leftValue = resultText;
        }

        private void ClearValues()
        {
            leftValue = "0";
            rightValue = "";
            operation = "";
            calculationText = "";
            resultText = "0";
        }

        private void UpdateNumber(ref string number, string value)
        {
            number += value;
            if (!number.Contains(","))
                number = double.Parse(number).ToString();
            resultText = number;
        }
    }
}

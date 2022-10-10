using System.ComponentModel;
using System.Text;

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
            ref string number = ref leftValue;
            if (operation == string.Empty)
                number = ref leftValue;
            else
                number = ref rightValue;

            if (char.IsDigit(value[0]))
                UpdateNumber(ref number, value);
            else if (value == "C")
                ClearValues();
            else if (value == "=" && leftValue != "")
            {
                Calculate();
                operation = "";
            }
            else if (value == "D")
                DeleteLastChar(ref number);
            else if (value == ",")
                AddPoint(ref number);
            else if (value == "+/-")
                InverseNumber(ref number);
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
                if (operation != string.Empty)
                    Calculate();
                operation = value;
                rightValue = "0";
                calculationText = leftValue + " " + operation;
                resultText = rightValue;
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
            rightValue = string.Empty;
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

        private void DeleteLastChar(ref string number)
        {
            StringBuilder temp = new StringBuilder();
            for (int i = 0; i < number.Length - 1; i++)
                temp.Append(number[i]);
            number = temp.ToString();

            if (number == string.Empty)
                number = "0";

            resultText = number;
        }

        private void AddPoint(ref string number)
        {
            if (number.Contains(","))
                return;
            number += ",";
            resultText = number;
        }

        private void InverseNumber(ref string number)
        {
            number = (double.Parse(number) - double.Parse(number) * 2).ToString();
            resultText = number;
        }
    }
}
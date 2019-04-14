using System.Globalization;
using System.Text;

namespace GetecTask.Services
{
    public class WordyFormatter
    {
        const string NumberGroupSeparator = " ";
        const string NumberDecimalSeparator = ",";

        private readonly string[] multiplications = new[] { "dollar", "thousand", "million" };
        private readonly string[] digits = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fiveteen", "sixteen", "seventeen", "eighteen", "nineteen", "twenty" };
        private readonly string[] tens = new[] { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        private readonly NumberFormatInfo _f;

        public WordyFormatter()
        {
            _f = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
            _f.NumberGroupSeparator = NumberGroupSeparator;
            _f.NumberDecimalSeparator = NumberDecimalSeparator;
        }

        public WordPresentation Parse(string input)
        {
            var isNumber = decimal.TryParse(input, NumberStyles.Number, _f, out decimal number);
            if (!isNumber)
            {
                return new WordPresentation
                {
                    Success = false,
                    WordyOutput = "Not a number."
                };
            }
            if (number > 999999999.99m || number < 0)
            {
                return new WordPresentation
                {
                    Success = false,
                    WordyOutput = "Number out of range."
                };
            }

            var sb = new StringBuilder();

            var formatedInput = number.ToString("#,###,##0.##", _f);
                                                                        
            ParseFormattedString(formatedInput, sb);

            return new WordPresentation
            {
                Success = true,
                WordyOutput = sb.ToString()
            };
        }

        void ParseFormattedString(string input, StringBuilder sb)
        {
            var decimalSplit = input.Split(NumberDecimalSeparator);
            var groupSplit = decimalSplit[0].Split(NumberGroupSeparator);

            for (var i = 0; i < groupSplit.Length; i++)
            {
                var isLastBlock = i == groupSplit.Length - 1;

                if (groupSplit[i] == "000" && !isLastBlock)
                {
                    continue;
                }

                ParseBlock(groupSplit[i], sb);
                sb.Append(multiplications[groupSplit.Length - i - 1]);

                if (!isLastBlock)
                {
                    sb.Append(" ");
                }
                else if (groupSplit[i] != "1")
                {
                    sb.Append("s");
                }
            }
            var isCentPart = decimalSplit.Length > 1;
            if (isCentPart)
            {
                ParseCentPart(decimalSplit[1], sb);
            }
        }

        void ParseCentPart(string input, StringBuilder sb)
        {
            if (input.Length == 1)
            {
                input = input + "0";
            }
            sb.Append(" and ");
            ParseBlock(input, sb);
            sb.Append("cent");
            if (input != "01")
            {
                sb.Append("s");
            }
        }

        void ParseBlock(string input, StringBuilder sb)
        {
            if (string.IsNullOrEmpty(input) || input == "000" || input == "00")
            {
                return;
            }

            var numeric = int.Parse(input);
            var firstDigit = input.Substring(0, 1);

            if (numeric <= 20)
            {
                sb.Append(digits[numeric]);
                sb.Append(" ");
            }
            else if(numeric < 100)
            {
                var secondDigit = input.Substring(1, 1);
                var tensDigitIndex = "23456789".IndexOf(firstDigit);
                if (tensDigitIndex >= 0)
                {
                    sb.Append(tens[tensDigitIndex]);
                    sb.Append("-");
                    sb.Append(GetWordyDigit(secondDigit));
                    sb.Append(" ");
                }
            }
            else
            {
                sb.Append(GetWordyDigit(firstDigit));
                sb.Append(" hundred ");
                ParseBlock(input.Substring(1), sb);
            }
        }

        string GetWordyDigit(string currentDigit)
        {
            var digitIndex = "0123456789".IndexOf(currentDigit);
            if (digitIndex >= 0)
            {
                return digits[digitIndex];
            }
            return string.Empty;
        }
    }
}

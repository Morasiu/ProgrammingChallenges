using System;
using System.Linq;

class RadixConverter{
  static void Main(string[] args){
    Console.ForegroundColor = ConsoleColor.Green;
    Console.CursorVisible = false;
    Console.Title = "Radix Converter";
    Console.WriteLine("<-----------Radix Converter----------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    ChooseOption();
    Console.ReadKey();
  }

  private static void ChooseOption() {
    Console.WriteLine("Options:");
    Console.WriteLine("1 - Convert from decimal");
    Console.WriteLine("2 - Convert to decimal");

    var isOptionCorrect = false;
    do {
      var key = Console.ReadKey().Key;
      switch (key) {
        case ConsoleKey.D1:
          isOptionCorrect = true;
          ConvertFromDecimalAndDisplayResult();
          break;
        case ConsoleKey.D2:
          ConvertToDecimalAndDisplayResult();
          isOptionCorrect = true;
          break;
        default:
          Console.Write("\b");
          continue;
      }
    } while (!isOptionCorrect);
  }

  private static void ConvertToDecimalAndDisplayResult() {
    Console.Write("\bNumber: ");
    var number = Console.ReadLine();
    var radix = GetRadix();
    var result = ConvertToDecimal(number, radix);
    Console.WriteLine(number + " from radix " + radix + " to decimal is equal " + result);
  }

  private static void ConvertFromDecimalAndDisplayResult() {
    var number = GetNumber();
    var radix = GetRadix();
    var result = ConvertFromDecimal(number, radix);
    Console.WriteLine(number + " in " + radix + " is: " + result);
  }

  private static int GetNumber() {
    Console.Write("\bNumber: ");
    var input = Console.ReadLine();
    if(!int.TryParse(input, out var number)) {
      ExitWithError("ERROR: Wrong number");
    }
    return number;
  }

  private static int GetRadix() {
    Console.Write("\bRadix: ");
    var input = Console.ReadLine();
    if(!int.TryParse(input, out var radix)) {
      ExitWithError("ERROR: Wrong number");
    }

    if (radix > 36 || radix < 2)
      ExitWithError("ERROR: Radix needs to be between 2 and 36");

    return radix;
  }

  private static string ConvertFromDecimal(int number, int radix) {
    var result = "";
    var inputNumber = number;
    var alphabet = GetAlphabet();

    while (inputNumber >= radix) {
      var modal = inputNumber % radix;
      if (modal >= 10)
        result += alphabet[modal - 10];
      else
        result += modal;
      inputNumber = inputNumber / radix;
    }

    if (inputNumber > 9)
      result += alphabet[inputNumber - 10];
    else
      result += inputNumber;
    return string.Concat(result.Reverse());
  }

  private static long ConvertToDecimal(string number, int radix) {
    var result = new long();
    var inputNumber = number.Reverse().ToList();

    for (var index = 0; index < inputNumber.Count(); index++) {
      result += GetNumberFromChar(inputNumber[index]) * (long) Math.Pow(radix, Convert.ToDouble(index));
    }

    return result;
  }

  private static char[] GetAlphabet() {
    return Enumerable.Range('A', 26).Select(x => (char)x).ToArray();
  }

  private static int GetNumberFromChar(char number) {
    if (int.TryParse(number.ToString(), out var result)) {
      return result;
    }

    var alphabet = GetAlphabet();

    if (!alphabet.Contains(number.ToString().ToUpper()[0])) throw new ArgumentException("Wrong number");

    var index = alphabet.ToList().FindIndex(c => c == number.ToString().ToUpper()[0]);
    return index + 10;
  }

  private static void ExitWithError(string s) {
    Console.WriteLine(s);
    Environment.Exit(1);
  }
}

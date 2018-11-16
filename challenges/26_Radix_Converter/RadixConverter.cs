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
          var number = GetNumber();
          var radix = GetRadix();
          var result = ConvertFromDecimal(number, radix);
          Console.WriteLine(number + " in " + radix + " is: "  + result);
          break;
        case ConsoleKey.D2:
          isOptionCorrect = true;
          break;
        default:
          Console.Write("\b");
          continue;
      }
    } while (!isOptionCorrect);
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
    var alphabet = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();

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

  private static int ConvertToDecimal(string number, int radix) {
    var result = 0;
    var inputNumber = number;
    var alphabet = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();

    foreach (var num in inputNumber) {
      
    }

    return result;
  }

  private static void ExitWithError(string s) {
    Console.WriteLine(s);
    Environment.Exit(1);
  }
}

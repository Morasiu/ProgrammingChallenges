using System;
using System.Linq;

class Reverse{
  static void Main(string[] args){
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("<-----------Reverse String------------>");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Console.Write("Your text: ");
    Console.WriteLine(ReverseString(Console.ReadLine()));
    Console.ReadKey();
  }

  static string ReverseString(string text){
    // Simple one-liner challenge. Look how smooth it looks :)
    return string.Concat(text.Reverse());
  }
}

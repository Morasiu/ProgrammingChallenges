using System;

class CountWords{
  static void Main(string[] args){
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Title = "Count Words";
    Console.WriteLine("<------------Count Words-------------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Start();
  }

  static void Start() {
    Console.WriteLine("Write your sentence and press Enter");
    var sentence = Console.ReadLine();
    Console.WriteLine($"Words: {Count(sentence)}");
    Console.ReadKey();
  }

  static int Count(string sentence) {
    return sentence.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Length;
  }
}

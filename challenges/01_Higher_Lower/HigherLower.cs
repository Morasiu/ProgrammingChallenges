using System;

class HigherLower{
  static void Main(){
    Console.WriteLine("\n<----------Higher/Lower Game---------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Console.Write("Press any key to start.");
    Console.ReadKey();
    Console.WriteLine("Game Start!");
    GetDifficultLevel();

  }

  static int  GetDifficultLevel(){
    Console.WriteLine("1. Easy (1-10)\n2. Medium (1-100)\n3. Hard (1-1000)\nPick difficult level: ");
    int maxNumber = 0;
    var difficult = Console.ReadLine();
    if (difficult = "1"){                        
    
    }
    return difficult;
  }
}

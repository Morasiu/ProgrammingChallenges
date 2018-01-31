using System;

class HigherLower{
  static void Main(){
    Console.WriteLine("\n<----------Higher/Lower Game---------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Console.Write("Press any key to start.");
    Console.ReadKey();
    
    StartGame();
  }

  static void StartGame(){
    int maxNumber = GetDifficultLevel();
    Random rand = new Random();
    //First number is included, but second is exluded so I needed to add 1.
    int randomNumber  = rand.Next(1, maxNumber + 1);
    int number = 0;
    int attempt = 0;
    Console.WriteLine("\nGame Start!");

    //Start guessing loop.
    do {
      Console.Write("Enter valid number: ");
      //Try parse string from Console to int.
      try {
        number = int.Parse(Console.ReadLine());
      } catch {
        Console.WriteLine("Not a valid number.");
        continue;
      }
      if(number == randomNumber){
        attempt++;
        Console.WriteLine("Hurray! You won :) Attempts: " + attempt.ToString());
      } else if(number > randomNumber){
        attempt++;
        Console.WriteLine("Lower");
      } else if (number < randomNumber){
        attempt++;
        Console.WriteLine("Higher");
      }
    } while(number != randomNumber);
    //Ask if player want to play again.
    PlayAgain(); 
  }

  static void PlayAgain(){
    Console.Write("Do you want to play again? (y/n): ");
    string decision = Console.ReadLine();
    if (decision == "y")
      StartGame();
    else if (decision == "n"){
      Console.WriteLine("Thanks for playing. :)");
    } else {
      Console.WriteLine("Wrong option");
      PlayAgain();
    }
  }

  static int GetDifficultLevel(){
    Console.WriteLine("\n1. Easy (1-10)\n2. Medium (1-100)\n3. Hard (1-1000)\nPick difficult level: ");
    int maxNumber = 0;
    var difficult = Console.ReadLine();
    if (difficult == "1")                        
      maxNumber = 10;
    else if (difficult == "2")
      maxNumber = 100;
    else if(difficult == "3")
      maxNumber = 1000;
    else {
      Console.WriteLine("Wrong option.");
      maxNumber = GetDifficultLevel();
    }

    return maxNumber;
  }
}

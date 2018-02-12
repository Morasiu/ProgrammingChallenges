using System;
using System.Collections.Generic;
using System.Linq;

class RockPaperScissors{
  static void Main(string[] args){
    Console.WriteLine("<-----------RockPaperScissors----------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");

    Game();
    Console.ReadKey();
  }
  
  static void Game(){
    List<string> playerPicks = new List<string>();
    int playerWin = 0;
    int compWin = 0;
    while (true){
      string playerPick = GetPlayerPick();
      playerPicks.Add(playerPick);
      string compPick = GetComputerPick(playerPicks);
      Console.WriteLine("Your:     " + GetFullName(playerPick) + "\nComputer: " + GetFullName(compPick) + "\n");
      //Hm... Why not switch? It looks nice.
       switch (playerPick){
         case "r":
           if (compPick == "p"){
             Console.WriteLine("Paper beats rock.\nYou lose.");
             compWin++;
           } else if (compPick == "s"){
             Console.WriteLine("Rock beats scissors.\nYou win!");
             playerWin++;
           } else if (compPick == "r"){
             Console.WriteLine("Rock vs rock.\nIt's a draw.");
           }
           break;
         case "s":
           if (compPick == "r"){
             Console.WriteLine("Rock beats scissors.\nYou lose.");
             compWin++;
           } else if (compPick == "p"){
             Console.WriteLine("Scissors beats paper.\nYou win!");
             playerWin++;
           } else if (compPick == "s"){
             Console.WriteLine("Scissors vs scissors.\nIt's a draw.");
           }
           break;
         case "p":
           if (compPick == "s"){
             Console.WriteLine("Scissors beats paper.\nYou lose.");
             compWin++;
           } else if (compPick == "r"){
             Console.WriteLine("Paper beats rock.\nYou win!");
             playerWin++;
           } else if (compPick == "p"){
             Console.WriteLine("Paper vs paper.\nIt's a draw.");
           }
           break;
       }

       Console.WriteLine("You: " + playerWin + " Computer: " + compWin);
    }
  }

  static string GetComputerPick(List<string> playerPicks){
    //Of course I could make some random, but where is the fun?
    string pick = "";
    if (playerPicks.Count == 1){
      Random rand = new Random();
      string[] possiblePicks = new [] {"r", "p", "s"};
      int randIndex = rand.Next(0, possiblePicks.Length);
      pick = possiblePicks[randIndex];
    } else{
      //AI time. It's my algorytm.
      
    }
    return pick; 
  }


  static string GetWinPick(string pick){
    //Return pick which will win with given pick.
    string winPick;
    if (pick == "s")
      winPick = "r";
    else if (pick == "p")
      winPick == "s";
    else if (pick == "r")
      winPick == "p";
    return winPick;
  }

  static string GetPlayerPick(){
    while (true) {
      Console.WriteLine("\nPick rock (r), paper (p) or scissors (s): ");
      string pick = Console.ReadLine();
      if (new [] {"r", "p", "s"}.Contains(pick))
        return pick;
      Console.WriteLine("Wrong option");
    }
  }

  static string GetFullName(string pick){
    switch (pick){
      case "r":
        return "Rock";
      case "p":
        return "Paper";
      case "s":
        return "Scissors";
      default:
        Console.WriteLine("Wrong option");
        return "";
    }
  }
}

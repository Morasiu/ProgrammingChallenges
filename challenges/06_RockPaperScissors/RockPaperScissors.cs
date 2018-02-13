using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class RockPaperScissors{
  static void Main(string[] args){
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("<-----------RockPaperScissors----------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Console.Write("Choose your name: ");
    string name = Console.ReadLine();
    Game(name);
    Console.ReadKey();
  }
  
  static void Game(string name){
    //Values: <playerPick, compPick, result>
    List<Tuple<string, string, string>> gameInfo = new List<Tuple<string, string,string>>();
    string compName = "Sirius";
    int playerWin = 0;
    int compWin = 0;
    while (true){
      string playerPick = GetPlayerPick();
      string compPick = GetComputerPick(gameInfo);
      string result = "";
      //Fancy colors!
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine(name + ": " + GetFullName(playerPick));
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine(compName + ": " + GetFullName(compPick));
      Console.ForegroundColor = ConsoleColor.Green;
      
      //Hm... Why not switch? It looks nice.
       switch (playerPick){
         case "r":
           if (compPick == "p"){
             Console.WriteLine("Paper beats rock.\nYou lose.");
             result = "win";
             compWin++;
           } else if (compPick == "s"){
             Console.WriteLine("Rock beats scissors.\nYou win!");
             result = "lose";
             playerWin++;
           } else if (compPick == "r"){
             result = "draw";
             Console.WriteLine("Rock vs rock.\nIt's a draw.");
           }
           break;
         case "s":
           if (compPick == "r"){
             Console.WriteLine("Rock beats scissors.\nYou lose.");
             result = "win";
             compWin++;
           } else if (compPick == "p"){
             Console.WriteLine("Scissors beats paper.\nYou win!");
             result = "lose";
             playerWin++;
           } else if (compPick == "s"){
             result = "draw";
             Console.WriteLine("Scissors vs scissors.\nIt's a draw.");
           }
           break;
         case "p":
           if (compPick == "s"){
             Console.WriteLine("Scissors beats paper.\nYou lose.");
             result = "win";
             compWin++;
           } else if (compPick == "r"){
             Console.WriteLine("Paper beats rock.\nYou win!");
             result = "lose";
             playerWin++;
           } else if (compPick == "p"){
             Console.WriteLine("Paper vs paper.\nIt's a draw.");
             result = "draw";
           }
           break;
       }

       gameInfo.Add(new Tuple<string, string, string>(playerPick, compPick, result));
       File.AppendAllLines(@"./" + name, new[] {playerPick + "," + compPick + "=" + result});
       Console.WriteLine(name + ": " + playerWin + " " + compName + ": " + compWin);
    }
  }

  static string GetComputerPick(List<Tuple<string, string, string>> gameInfo){
    //Of course I could make some random, but where is the fun? Check this algorytm in docs.
    string pick = "";
    string[] possiblePicks = new[]{"r", "p", "s"};
    if (gameInfo.Count == 0){
      //Round 1. Random, but 50% paper.
      Random randObject = new Random();
      int index = 0;
      int rand = randObject.Next(1, 101); //Exclude 101, so it is 1-101
      if (rand <= 25) //1-25, so 25% rock.
        index = 0;
      else if (rand > 25 && rand <= 75)
        index = 1; //25-75, so 50% paper.
      else
        index = 2; //25% scissors. 
      pick = possiblePicks[index];
    } else {
      //AI time. It's my algorytm.

      //Special condition against spaming with one pick
      if (gameInfo.Count > 1)
        if (gameInfo.Count > 2)
          if (gameInfo.Last().Item1 == gameInfo.Skip(1).Last().Item1 && gameInfo.Skip(1).Last().Item1 == gameInfo.Skip(2).Last().Item1){
            pick = GetWinPick(gameInfo.Last().Item1);
            return pick;
          }
        else if (gameInfo.Last().Item3 == "lose" && gameInfo.Skip(1).Last().Item3 == "lose"){
          pick = GetWinPick(gameInfo.Last().Item1);
          return pick;
        }
      int rPick = gameInfo.Where(x => x.Item1.Equals("r")).Count();
      int sPick = gameInfo.Where(x => x.Item1.Equals("s")).Count();
      int pPick = gameInfo.Where(x => x.Item1.Equals("p")).Count();
      int allPicks = rPick + sPick + pPick;
      //Console.WriteLine("R-" + rPick + " P-" + pPick + " S-" + sPick);

      if(rPick == sPick && sPick == pPick)   
        pick = GetLosePick(gameInfo.Last().Item1);
      else {
        Dictionary<string, int> pickDict = new Dictionary<string, int>();
        pickDict.Add("r", rPick);
        pickDict.Add("s", sPick);
        pickDict.Add("p", pPick);
        //Sort this to easy get pick with lowest value.
        var sortedDict = from entry in pickDict orderby entry.Value ascending select entry;
        //If there is only one pick with the lowest number of picks.

        if(sortedDict.First().Value != sortedDict.Skip(1).First().Value){
         pick = GetWinPick(sortedDict.First().Key);
        } else {
          pick = GetBetterPick(new [] {sortedDict.First().Key, sortedDict.Skip(1).First().Key}); 
        }
      }
    }
    return pick; 
  }

  static string GetBetterPick(string[] picks){
    string betterPick = "";
    if(picks.Contains("s") && picks.Contains("p"))
      betterPick = "s";
    else if (picks.Contains("s") && picks.Contains("r"))
      betterPick = "r";
    else if (picks.Contains("p") && picks.Contains("r"))
      betterPick = "p";
    //Console.WriteLine("Better pick from " + picks[0] + " and " + picks[1] + " is " + betterPick);
    return betterPick;
  }

  static string GetLosePick(string pick){
    //Return pick, which will lose with given pick.
    string losePick = "";
    if (pick == "s")
      losePick = "p";
    else if (pick == "p")
      losePick = "r";
    else if (pick == "r")
     losePick = "s";
    //Console.WriteLine("Get lose " + pick + " with " + losePick);
    return losePick;
  }

  static string GetWinPick(string pick){
    //Return pick which will win with given pick.
    string winPick = "";
    if (pick == "s")
      winPick = "r";
    else if (pick == "p")
      winPick = "s";
    else if (pick == "r")
      winPick = "p";
    //Console.WriteLine("Get win " + pick + " with " + winPick);
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

using System;
using System.Net;

class Hangman{
  static void Main(string[] args){
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("<--------------Hangman---------------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Console.WriteLine("Press any key to start.");
    Console.ReadKey();
    Console.Clear();
    Display(GetWord(), new char[26]);
    Console.ReadKey();
  }

  static void Display(string word, char[] letters){
    int fail;
    Console.WriteLine(word);

    foreach(char c in word){
      Console.Write("_ " + c);
    } 
    GetLetter();
  }

  static char GetLetter(){
    char letter;
    while(true){
      Console.Write("Give me a letter (a-z): ");
      string input = Console.ReadLine();
      if (input == ""){
        Console.WriteLine("Some guess");
        return '0';
      }else if(char.TryParse(input, out letter)){
        return letter;
      } else {
        Console.WriteLine("Invalid letter");
      }
    }
  }

  static string GetWord(){
    //Get a word from https://fakena.me/random-english-words/one/
    WebClient client = new WebClient();
    string url = "https://fakena.me/random-english-words/one/";
    string line = "";
    //Check internet connection
    try{
      line = client.DownloadString(url).Split('\n')[27];
    } catch (System.Net.WebException) {
      Console.WriteLine("Network connection problem :(");
      System.Environment.Exit(1);
    }
    string word = line.Substring(39).Split('<')[0];
    return word;
  }

  static string GetHangman(int fail){
    //Hangman ASCII art
    string hangman = ""; 
    switch (fail){
      case 0:
        hangman = @"
          
          
          
          
          
          
          
          ";
        break;
      case 1:
        hangman = @"
     ________
    |/      |
    |      (_)
    |      
    |     
    |    
    |
 ___|___";
        break;
      case 2:
        hangman = @"
     ________
    |/      |
    |      (_)
    |       |
    |       |
    |    
    |
 ___|___";
        break;
      case 3:
        hangman = @"
     ________
    |/      |
    |      (_)
    |       |
    |       |
    |      /
    |
 ___|___";
        break;
      case 4:
        hangman = @"
     ________
    |/      |
    |      (_)
    |       |
    |       |
    |      / \
    |
 ___|___";
        break;
      case 5:
        hangman = @"
     ________
    |/      |
    |      (_)
    |      \|
    |       |
    |      / \
    |
 ___|___";
        break;
      case 6:
        hangman = @"
     ________
    |/      |
    |      (_)
    |      \|/
    |       |
    |      / \
    |
 ___|___";
        break;
    }
    return hangman;
  }
}

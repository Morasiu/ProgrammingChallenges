using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Net;

class Hangman{
  static void Main(string[] args){
    Console.ForegroundColor = ConsoleColor.Green;
    Console.OutputEncoding = System.Text.Encoding.UTF8;
    Console.WriteLine("<--------------Hangman---------------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Console.WriteLine("Press any key to start.");
    Console.ReadKey();

    Display(GetWord(), new List<char>());
    Console.ReadKey();
  }

  static void Display(string word, List<char> letters){
    Console.Clear();
    int fails = 0;
    char[] guessWordLetters = new char[word.Length];
    //Console.WriteLine(word);

    for(int i = 0; i < letters.Count(); i++){
      if(word.Contains(letters[i])){
        for(int j = 0; j < word.Length; j++){
          if(word[j] == letters[i]){
            guessWordLetters[j] = word [j];
          }
        }
      } else {
        fails++;  
      }
    } 

    for (int i = 0; i < guessWordLetters.Length; i++){
      if (guessWordLetters[i] == '\0')
        guessWordLetters[i] = '_';
    }
  
    Console.WriteLine(GetHangman(fails) + "\n");

    Console.Write("Letters: ");
    
    foreach(char l in letters){
      Console.Write(l + ", ");
    }
    Console.Write("\nWord: ");
    foreach(char c in guessWordLetters){
      Console.Write(c + " ");
    }
    // Game Over :(
    if (fails == 7){
      Console.WriteLine("\nGame Over");
      Console.WriteLine("Your word was: " + word);
      Console.WriteLine("Definition:" + GetDefinition(word));
      System.Environment.Exit(1);
    }else if (word == String.Concat(guessWordLetters)){
      Console.WriteLine("\nYou won!");
      Console.WriteLine("Definition:" + GetDefinition(word));
      System.Environment.Exit(1);
    }
    char letter = GetLetter();
    if(!letters.Contains(letter))
      letters.Add(letter);

    Display(word, letters);
  }

  static char GetLetter(){
    char letter;
    while(true){
      Console.Write("\nGive me a letter (a-z): ");
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

  static string GetDefinition(string word){
    //Get definition to prove that is a real word.
    WebClient client = new WebClient();
    string url = "https://www.thefreedictionary.com/" + word;
    string site = client.DownloadString(url);
    string[] lines = site.Split(new [] { '\r', '\n' });
    string definition = "";
    foreach(string line in lines){
      if(line.Contains("id=\"Definition\"")){
        definition = line;
        break;
      }
    }
    
    definition = definition.Substring(definition.IndexOf("1.") + 3);
    definition = definition.Substring(0, definition.IndexOf("</div>"));
    definition = Regex.Replace(definition, "<.*?>", String.Empty);
    return definition;
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
    |      
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
    |      
    |     
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
    |    
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
    |      /
    |
 ___|___";
        break;
      case 5:
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
      case 6:
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
      case 7:
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
    default:
      hangman = "Got wrong number.";
      break;
    }

    return hangman;
  }
}

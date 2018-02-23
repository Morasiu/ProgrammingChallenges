using System;
using System.Net;

class Hangman{
  static void Main(string[] args){
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("<--------------Hangman---------------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Console.WriteLine("Press any to start.");
    Console.ReadKey();
    Console.Clear();

    Console.WriteLine(GetWord());
    Console.ReadKey();
  }

  static string GetWord(){
    //Get a word from https://www.randomlists.com/random-words
    WebClient client = new WebClient();
    string url = "https://www.randomlists.com/random-words";
    string word = client.DownloadString(url);

    return word;
  }
}

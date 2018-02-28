using System;
using System.Net;
using System.Linq;

class SentenceGenerator{
  static void Main(string[] args){
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("<------Random Sentence Generator------>");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Console.WriteLine("Press any key");
    Console.ReadKey();
    Console.WriteLine(GetWord("nouns"));
    Console.ReadKey();
  }

  static string GetSentence(){
    string sentence = "The ";
    return sentence;
  }

  static string GetWord(string type){
    string word = "";
    string url = "https://randomwordgenerator.com/noun.php";
    WebClient client  = new WebClient();
    word = client.DownloadString(url);
    return word;
 } 
}


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
    Console.WriteLine(GetSentence());
    Console.ReadKey();
  }

  static string GetSentence(){
    //I know, it take "the easy way" just getting sentence from site, but I couln't find any good sites with nouns, verbs etc.
    string sentence = "";
    string url = "https://funnysentences.com/random-sentences";
    WebClient client  = new WebClient();
    sentence = client.DownloadString(url).Split('\n')[118];
    sentence = sentence.Substring(sentence.IndexOf("\"></div>") + 8);
    sentence = sentence.Substring(0, sentence.IndexOf("<div class"));
    return sentence;
 } 
}


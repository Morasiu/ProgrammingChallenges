using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

class Haiku{
  static void Main(string[] args){
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("<-----------Haiku Genrator------------>");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Console.WriteLine("It may take a while. Don't worry, be happy.");
    Generate();
    Console.ReadKey();
  }

  static void Generate(){
    //Template for Haiku
    //From 4-syllabes
    //To 6-syllabes
    //5-syllabes
    string haiku = "";
    string[] words = GetWords();
    haiku += "From " + words[0] + "\n";
    haiku += "to " + words[1] + " " + words[2] +  "\n";
    haiku += words[3] + " " + words[4] + ".";
    Console.WriteLine(haiku);
  }

  static string[] GetWords(){
    List<string> words = new List<string>() {"", "", "", "", ""};
    while(words.Contains("")){
      string word = GetWord();
      int syllabes = CountSyllabes(word);
      if(words[0] == "" && syllabes == 4) words[0] = word;
      else if(words[1] == "" && syllabes == 2) words[1] = word;
      else if(syllabes == 4) words[2] = word;
      else if(syllabes == 2) words[3] = word;
      else if(syllabes == 3) words[4] = word;
    }

    return words.ToArray();
  }

  static int CountSyllabes(string word){
    //Count syllabes, simple as that. It works, but have a few exception like "pure" or "Language". Damn you English.
    string wordLow = word.ToLower();
    int syllabes = 0;
    // string consonants = "bcdfghjklmnqprstvwxz";
    string vovels = "aeiouy";

    for (int i = 0; i < wordLow.Length; i++){
      if (i < wordLow.Length - 1){
        if (vovels.Contains(wordLow[i].ToString()) && !vovels.Contains(wordLow[i+1].ToString()))
          syllabes++;
      } else if (i == wordLow.Length - 1){
        if (vovels.Contains(wordLow[i].ToString()))
          syllabes++;
      }
    }
    return syllabes;
  }

    static string GetWord(){
    //Copied from 08_Hangman
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
}

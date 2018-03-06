using System;
using System.Collections.Generic;

class Magic8Ball{
  static void Main(string[] args){
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("<------------Magic 8-Ball------------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Console.WriteLine("Think about you question and press any key");
    Console.ReadKey();

    DisplayAnswer();
  }

  static void DisplayAnswer(){
    Console.WriteLine(GetSpeechBubble(GetRandomAnswer()));
    Console.Write(Get8Ball());
  }

  static string GetRandomAnswer(){
    List<string> answers = new List<string>() {
      "It is certain",
      "It is decidedly so",
      "Without a doubt",
      "Yes definitely",
      "You may rely on it",
      "As I see it, yes",
      "Most likely",
      "Outlook good",
      "Yep",
      "Signs point to yes",
      "Reply hazy try again",
      "Ask again later",
      "Better not tell you now",
      "Cannot predict now",
      "Concentrate and ask again",
      "Don't count on it",
      "My reply is no",
      "My sources say no",
      "Outlook not so good",
      "Very doubtful"
    };
    Random rand = new Random();
    return answers[rand.Next(0, answers.Count)];
  }

  static string GetSpeechBubble(string text){
    string bubble = "";
    string[] template = new string[3];
    int textLength = text.Length;
    template[0] += "／";
    template[1] += "| " + text;
    template[2] += "＼";

    for (int i = 0; i < textLength; i++){
      template[0] += "￣";
      template[1] += " ";
      template[2] += "__";
    }
    template[0] += "＼";
    template[1] += " |";
    template[2] += "／";
    bubble = template[0] + "\n" + template[1] + "\n" + template[2] + "\n" + "　　　∨";
    return bubble;
  }

  static string Get8Ball(){
    string ball = @"
          _......._
       .-:::::::::::-.
     .:::::::::::::::::.
    :::::::' .-. `:::::::
   :::::::  :   :  :::::::
  ::::::::  :   :  ::::::::
  :::::::::._`-'_.:::::::::
  :::::::::' .-. `:::::::::
  ::::::::  :   :  ::::::::
   :::::::  :   :  :::::::
    :::::::._`-'_.:::::::
     `:::::::::::::::::'
       `-:::::::::::-'
          `'''''''`'
          ";
    return ball;
  }
}

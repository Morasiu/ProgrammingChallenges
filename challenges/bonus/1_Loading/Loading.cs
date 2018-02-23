using System;
using System.Threading;

class Loading{
  static void Main(string[] args){
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("<---------------Loading--------------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");

    StartLoading();

    Console.ReadKey();
  }

  static void StartLoading(){
    while (true){
      string load = "-\\|/";
      foreach (char s in load){
        Console.Write(s);
        Thread.Sleep(80);
        Console.Write("\b");
      } 
    }
  }
}

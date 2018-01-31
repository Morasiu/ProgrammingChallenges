using System;
using System.Linq;
using System.Collections;

class Converter{
  static void Main(string[] args){
    Console.WriteLine("<--------Temperature Converter-------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Console.WriteLine("Enter temperature below. For example 50 C or 24.2 F.");
   
    GetTemp();
  }


  static Hashtable GetTemp(){
    string input  = Console.ReadLine();
    string[] temp = new string[2];
    temp = input.Split(' ');
    Hashtable temps = new Hashtable();
    int tempValue = 0;
    //Check if temperature format is valid.
    if (temp.Length != 2 || !(new [] {"F", "K", "C"}.Contains(temp[1]))){
      Console.WriteLine("Wrong data. Try again.");
      GetTemp();
    }

    try{
      tempValue = int.Parse(temp[0]);
    }catch{
      Console.WriteLine("Not a valid number");
      GetTemp();
    }
    
    temps.Add("scale", temp[1]);
    temps.Add("value", tempValue);

    foreach (string x in temp)
      Console.WriteLine(x);
    return temps;
  }
}

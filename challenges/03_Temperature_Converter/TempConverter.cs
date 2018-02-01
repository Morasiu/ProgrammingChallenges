using System;
using System.Linq;
using System.Collections;

class Converter{
  static void Main(string[] args){
    Console.WriteLine("<--------Temperature Converter-------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Console.WriteLine("Enter temperature below. For example 50 C or 24.2 F.");
   
    Hashtable tempInfo = GetTemp();
    ChooseScale((char)tempInfo["scale"]);
  }
  
  static char ChooseScale(char originScale){
    Console.Write("1. Kelvin\n2. Celsius\n3. Fahrenheit\nChoose scale: ");
    char convertScale = char.Parse(Console.ReadLine());

    return convertScale;
  }

  static Hashtable GetTemp(){
    Hashtable tempHashtable = new Hashtable();
    float tempValue = 0;
    string[] temp = new string[2];
    //Check if temperature format is valid.
    while (true){
      string input  = Console.ReadLine();
      temp = input.Split(' ');
      if (temp.Length == 2 || (new [] {"F", "K", "C"}.Contains(temp[1])))
        break;
      //check if it is int
      try{
        tempValue = float.Parse(temp[0]);
        break;
      }catch{
        Console.WriteLine("Not a valid number");
      }
    
      Console.WriteLine("Wrong data");
    }
       
    tempHashtable.Add("scale", char.Parse(temp[1]));
    tempHashtable.Add("value", tempValue);

    return tempHashtable;
  }
}

using System;
using System.Collections.Generic;
using System.Linq;

class LoveCalculator{
  static void Main(string[] args){
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("<-----------Love Calculator----------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");

    Calculate();

    Console.ReadKey();
  }

  static void Calculate(){
    //Step 1. Take the letters of the word LOVES, and then find out how many of each letter are in both of the names.
    //Step 2. place each of these numbers beside the other.
    //Step 3. start adding each pair of numbers together, to generate the next number, you will continue to do this until there are only two numbers left.
    //For example. If the names where Duchess and Latro you would find, L=1 O=1 V=0 E=1 S=2. 11012 -> (1+1,1+0,0+1,1+2) -> 2113 -> 324 -> 56
    
    List<int> count = new List<int>(); //Always 5 on start because of five letter in "loves"
    //First name
    Console.Write("First name: ");
    string firstName = Console.ReadLine().ToLower();
    //Second name
    Console.Write("Second name: ");
    string secondName = Console.ReadLine().ToLower();

    count = Enumerable.Zip(CountLetters(firstName), CountLetters(secondName), (a, b) => a + b).ToList();
    string sum = "";

    foreach(int i in count){
      sum += i.ToString();
    }
    Console.WriteLine("Love percent: " + SumNumbers(sum) + " ♥");
  }

  static string SumNumbers(string num){
    //Recursive time!
    string sum = num;
    string tmpSum = "";
    if(num.Length == 2)
      return num;
    else {
      for(int i = 0; i < sum.Length - 1; i++){
        int iSum = (int)(sum[i] - '0') + (int)(sum[i+1] - '0');
        if (iSum > 10){
          iSum = (int)(iSum.ToString()[0] - '0') + (int)(iSum.ToString()[1] - '0');
        }
        tmpSum += iSum.ToString();
      }
      return SumNumbers(tmpSum);
    }
  }
  

  static int[] CountLetters(string name){
    int[] count = new int[5];
    string loves = "loves";
    foreach(char l in name){
      for(int i = 0; i < loves.Length; i++){
        if(l == loves[i]){
          count[i]++;
          break;
        }
      }
    }
    return count;
  }
}

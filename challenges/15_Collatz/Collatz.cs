using System;

class Collatz{
  static void Main(string[] args){
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("<--------------Collatz--------------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Start();
  }

  static void Start(){
    int num = GetNumber();
    CollatzConjecture(num);
    Console.Write("\b\n");
  }

  static int CollatzConjecture(int num){
    /*Consider the following operation on an arbitrary positive integer:
      If the number is even, divide it by two.
      If the number is odd, triple it and add one.*/
    Console.Write(num + ",");
    if(num == 1)
      return 1;
    else if(num % 2 == 0){
      return CollatzConjecture(num/2);
    } else {
      return CollatzConjecture(3*num + 1);
    }
  }

  static int GetNumber(){
    int num = 0;
    while (true){
      Console.Write("Enter starting number: ");
      string sNum = Console.ReadLine();
        try{
          num = int.Parse(sNum);
          if(num > 0)
            break;
      } catch {}
      Console.WriteLine("Wrong number");
    }
    return num;
  }
}

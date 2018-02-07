using System;
using System.Globalization;
using System.Text.RegularExpressions;

class AgeCalculator{
  static void Main(string[] args){
    Console.WriteLine("<------------Age Calculator----------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Console.WriteLine("Enter your birth date. For example 22-05-1987");
    Console.WriteLine("Seconds passed since your birth: " + GetDate());
    Console.ReadKey();
  }

  static string GetDate(){
    string date;
    while(true){
    date = Console.ReadLine();
      if (Regex.IsMatch(date, "((?!00)+([0-2][0-9])|(3[0-1]))+-((0[1-9])|(1[0-2]))+-([0-9]{4})")){
        break;
      }
      Console.WriteLine("Wrong date format");
    }
    DateTime birthDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
    TimeSpan diff = DateTime.Today - birthDate;
    return diff.TotalSeconds.ToString();
  }
}

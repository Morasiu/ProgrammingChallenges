using System;

class PasswordGen{
  static void Main(string[] args){
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("<----------Password Generator---------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Console.ReadKey();
    Console.WriteLine("Your password:\n" + Generate());
  }

  static string Generate(){
    // A strong password has:
    // - at least 15 characters
    // - uppercase letters
    // - lowercase letters
    // - numbers
    // - symbols
    int length = 15;
    string passwd = "";
    string lowLetters = "qwertyuiopasdfghjklzxcvbnm";
    string upLetters = lowLetters.ToUpper();
    string symbols = "[]{}();:!@#$%^&*?=-_+`~";
    string numbers = "0123456789";
    string all = lowLetters + upLetters + numbers + symbols;
    Random rand = new Random();
    for(int i = 0; i < length; i++){
      passwd += all[rand.Next(0, all.Length)];
    }
    return passwd;
  }
}

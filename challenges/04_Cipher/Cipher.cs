using System;
using System.Threading;

class Cipher{
  static void Main(string[] args){
    Console.WriteLine("<---------------Cipher---------------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");

    string text = GetText();
    string key  = GetKey();
    string option = ChooseOption();
    
    if (option == "1")
      Encrypt(text, key);
    else if (option == "2")
      Decrypt(text, key);
    Console.ReadKey();
  }

  static void Encrypt(string text, string key){
    string encText = "";
    int keyIndex = 0;
    if (key.Length == 0){
      Console.WriteLine("Empty key");
    } else {
      for(int i = 0; i < text.Length; i++){
        char originLetter = text[i];
        int asciiOrigin = originLetter;
        char keyChar = key[keyIndex];
        int asciiKey = keyChar - 32;
        int asciiEnc = asciiOrigin + asciiKey;
        if (asciiEnc > 126)
          asciiEnc = asciiEnc - 126 + 32; 
  
        char encChar = (char) (asciiEnc);
  
        keyIndex++;
        if(keyIndex > key.Length -1)
         keyIndex = 0;
        encText += encChar.ToString();
      }
      Console.Write("Result:\n>" + encText + "<\n");
    }
  }

  static void Decrypt(string text, string key){
    string decText = "";
    int keyIndex = 0;
    if (key.Length == 0){
      Console.WriteLine("Empty key");
    } else {
      for(int i = 0; i < text.Length; i++){
        char originLetter = text[i];
        int asciiOrigin = originLetter;
        char keyChar = key[keyIndex];
        int asciiKey = keyChar - 32;
        int asciiDec = asciiOrigin - asciiKey;
        if (asciiDec <  32)
          asciiDec = 126 - (32 - asciiDec); 
  
        char decChar = (char) (asciiDec);
  
        keyIndex++;
        if(keyIndex > key.Length -1)
         keyIndex = 0;
        decText += decChar.ToString();
      }
      Console.Write("Result:\n>" + decText + "<\n");
    }

  }

  static string ChooseOption(){
    string option;
    do {
      Console.WriteLine("1. Encrypting\n2. Decrypting");
      Console.Write("Choose option: ");
      option = Console.ReadLine();
    } while (option != "1" && option != "2");
    return option;
  }

  static string GetText(){
    //I know. I looks useless. I could just add this in Main, but hey it looks nice.
    string text;
    Console.WriteLine("Enter your text below and press Enter.");
    text = Console.ReadLine();
    return text;
  }

  static string GetKey(){
    //"You could write this in one line". Yeah, yeah, but it is so pretty :)
    string key;
    Console.WriteLine("Enter your key below and press Enter.");
    key = Console.ReadLine();
    return key;
  }
}

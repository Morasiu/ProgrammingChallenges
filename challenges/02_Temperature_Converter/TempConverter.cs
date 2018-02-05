using System;
using System.Linq;
using System.Collections;

class Converter{
  static void Main(string[] args){
    Console.WriteLine("<--------Temperature Converter-------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Console.WriteLine("Enter temperature below. For example 50 C or 24,2 F.");
   
    Hashtable tempInfo = GetTemp();
    char scale = ChooseScale((char)tempInfo["scale"]);
    string convertedTemp = Convert(float.Parse(tempInfo["value"].ToString()), (char) tempInfo["scale"], scale);
    Console.WriteLine("Result: " + convertedTemp);
  }
  
  static string  Convert(float temp, char scale, char choosenScale){
    float convertedTempValue;
    string convertedTemp = "";
    //If choosen scale is equal to temp scale, it doesn't need to convert.
    if(scale == choosenScale){
      convertedTemp = temp.ToString() + ' ' + scale;
      return convertedTemp;
    }

    //I used switch to show how it works, nobody use switch anymore :(
    switch (scale){
      //Convert Fahrenheit.
      case 'F':
        //Convert Fahrenheit to Celsius.
        convertedTempValue = (temp - 32f)/1.8f;
        if (choosenScale ==  'C')
          convertedTemp = convertedTempValue.ToString() + " C";
        else 
          //Add 253.15 to make Kelvin from Celsius.
          convertedTemp = (convertedTempValue + 253.15f).ToString() + " K";
        break;
      //Convert Kelvin.
      case 'K':
        // Convert to Celsius.
        convertedTempValue = temp - 253.13f;
        if (choosenScale == 'C')
          convertedTemp = convertedTempValue.ToString() + " C";
        else{
          //Convert to Fahrenheit.
          convertedTempValue = convertedTempValue*1.8f + 32f;
          convertedTemp = convertedTempValue.ToString() + " F";
        }
        break;
      //Convert Celsius.
      case 'C':
        //Convert to Kelvin
        if (choosenScale == 'K'){
          convertedTempValue = temp + 253.15f;
          convertedTemp = convertedTempValue.ToString() + " K";
        } else {
          convertedTempValue = temp*1.8f + 32f;
          convertedTemp = convertedTempValue.ToString() + " F";
        }
        break;
      //It is always good to make default, just in case.
      default:
        Console.WriteLine("Wrong option.");
        break;
    }

    return convertedTemp;
  }

  static char ChooseScale(char originScale){
    Console.Write("1. Kelvin\n2. Celsius\n3. Fahrenheit\nChoose scale: ");
    char numConvertScale = char.Parse(Console.ReadLine());
    char convertScale = '0';
    // Change number to char.
    if (numConvertScale == '1')
      convertScale = 'K';
    else if (numConvertScale == '2')
      convertScale = 'C';
    else if (numConvertScale == '3')
      convertScale = 'F';
    return convertScale;
  }

  static Hashtable GetTemp(){
    Hashtable tempHashtable = new Hashtable();
    float tempValue = 0;
    string[] temp; 
    //Check if temperature format is valid.
    while (true){
      string input  = Console.ReadLine();
      temp = input.Split(' ');
      if(temp.Length == 2){
        if(new [] {"F", "K", "C"}.Contains(temp[1])){
          //Check if first value is int
          try{
            tempValue = float.Parse(temp[0]);
            break;
          }catch{
            Console.WriteLine("Not a valid number");
          }
        }
      }
      Console.WriteLine("Wrong data");
    }
    tempHashtable.Add("scale", char.Parse(temp[1]));
    tempHashtable.Add("value", tempValue);

    return tempHashtable;
  }
}

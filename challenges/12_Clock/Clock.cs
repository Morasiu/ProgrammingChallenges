using System;
using System.Threading;
using System.Collections.Generic;
class Clock{
  static void Main(string[] args){
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("<-----------Internet Time------------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Console.WriteLine("Press any key to start clock");
    Console.ReadKey();
    StartClock();
  }

  static string GetTime(){
     DateTime time = DateTime.Now;
     string sTime = time.ToString("HH:mm:ss");
     return sTime;
  }

  static void StartClock(){
    List<string> arts = new List<string>();
    string time;
    string clock = "";
    Console.Clear();
    arts.Clear();
    time = GetTime();
    foreach (var c in time){
      arts.Add(GetACIIArt(c));
    }

    for (int i = 0; i < 6; i++){
      foreach (string art in arts){
        clock += art.Split('\n')[i];
      }
      clock += "\n";
    }
    Console.WriteLine(clock);
    Thread.Sleep(1000);
    StartClock();
  }

  static string GetACIIArt(char c){
    string art = "";
    switch (c){
      case '0':
        art = @"         
  ,--.   
 /    \  
|  ()  | 
 \    /  
  `--'   ";
        break;
      case '1':
        art = @"      
 ,--. 
/   | 
`|  | 
 |  | 
 `--' 
        ";
        break;
      case '2':
        art = @"        
 ,---.  
'.-.  \ 
 .-' .' 
/   '-. 
'-----' 
        ";
        break;
      case '3':
        art = @"        
,----.  
'.-.  | 
  .' <  
/'-'  | 
`----'  
        ";
        break;
      case '4':
        art = @"        
  ,---. 
 /    | 
/  '  | 
'--|  | 
   `--' 
        ";
        break;
      case '5':
        art = @"        
,-----. 
|  .--' 
'--. `\ 
.--'  / 
`----'  
        ";
        break;
      case '6':
        art = @"        
  ,--.  
 /  .'  
|  .-.  
\   o | 
 `---'  
        ";
        break;
      case '7':
        art = @"        
,-----. 
'--,  / 
 .'  /  
/   /   
`--'    
        ";
        break;
      case '8':
        art = @"        
 ,---.  
|  o  | 
.'   '. 
|  o  | 
 `---'  
        ";
        break;
      case '9':
        art = @"        
 ,---.  
| o   \ 
`..'  | 
 .'  /  
 `--'   
        ";
        break;
      case ':':
        art = @"     
     
.--. 
'--' 
.--. 
'--' 
        ";
        break;
      default:
        art = @"                                        
,------.                             
|  .---',--.--.,--.--. ,---. ,--.--. 
|  `--, |  .--'|  .--'| .-. ||  .--' 
|  `---.|  |   |  |   ' '-' '|  |    
`------'`--'   `--'    `---' `--'                                       
        ";
        break;
    }
    return art;
  }
}

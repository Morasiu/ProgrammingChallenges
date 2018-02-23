using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

class Euler{
  static void Main(string[] args){
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("<------------Project Euler------------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");

    OptionsChooser();

    Console.ReadKey();
  }

  static void Euler1(){
    //Problem 1:
    //If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.
    //Find the sum of all the multiples of 3 or 5 below 1000.

    //Ok, that's clear, we need to make some efficent for loop.
    //Easy way, typical for loop takes 1000 cycles. It could be done better.
    //
    //for(int i = 3; i < 1000; i++){
    //  if (i % 3 == 0)
    //    sum += i;
    //  else if (i % 5 == 0)
    //   sum += i;
    //}

    int sum = 0;
    int j = 0;
    
    for (int i = 3; i < 1000; i += 3){
      j++;
      sum += i;
    }

    for (int i = 5; i < 1000; i +=5){
      j++;
      sum += i;
      if((i+5) % 3 == 0)
       i+=5; 
    }
    //I've managed to decrease cycles from 1000 to 466. 

    Console.WriteLine("Solution: " + sum + ". For loop cycles: " + j);
  }

  static void Euler2(){
    //Problem 2:
    //By considering the terms in the Fibonacci sequence whose values do not exceed four million, find the sum of the even-valued terms.
    
    //Fibonacci from 1 to 4 000 000 and sum all even numbers. Simple as that.
    int fib = 1; 
    int temp = 1;
    int sum = 0;
    while (fib < 4000000){
      fib = fib + temp;
      temp = fib - temp;
      if (fib % 2 == 0)
        sum += fib;
    }

    Console.WriteLine("Solution: " + sum);
  }

  static void Euler3(){
    //Problem 3:
    //The prime factors of 13195 are 5, 7, 13 and 29.
    //What is the largest prime factor of the number 600851475143 ?
    //Result should be: 71 × 839 × 1471 × 6857
    long num = 600851475143;
    //2 x 3 x 5
    long test = num;
    long  testOrigin = test;
    List<int> factors = new List<int>();
    factors.Add(1);

    long j;
    do {
      j = 1;
      for (int i = 2; i <= test; i++){
        if(test % i == 0){
          factors.Add(i);
          test = test / i;
          break;
        }
      }
      
      foreach (int i in factors)
        j = j * i;
    } while (j != testOrigin);
    
    Console.WriteLine("Solution: " + factors.Last());
  }
  
  static void Euler4(){
    //Problem 4:
    //A palindromic number reads the same both ways. The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 × 99.
    //Find the largest palindrome made from the product of two 3-digit numbers.
    //906609 = 993 X 913 
    
    bool isPal = false;
    int num = 0;
    int pal = 0;
    for(int i = 999; i >= 100; i--){
      for(int j = 999; j >= 100; j--){
        num = i*j;
        isPal = isPalindrom(num); 
        if(isPal && num > pal)
          pal = num;
      }
    }
    Console.WriteLine("Solution: " + pal);
  }

  static void Euler5(){
    //Problem 5:
    //2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.
    //What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
    int solution = 20;
    while (true){
      if(isDivisible(solution))
        break;
      solution++;
    }
    
    Console.WriteLine("Solution: " + solution);
  }

  static void Euler6(){
    //Problem 6
    //The sum of the squares of the first ten natural numbers is,
    //1^2 + 2^2 + ... + 10^2 = 385
    //The square of the sum of the first ten natural numbers is,
    //(1 + 2 + ... + 10)^2 = 55^2 = 3025
    //Hence the difference between the sum of the squares of the first ten natural numbers and the square of the sum is 3025 − 385 = 2640.
    //Find the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.
    int sumOfSqr = 0;
    int sqrOfSum = 0;

    for(int i = 1; i <= 100; i++){
      sumOfSqr += (i*i); 
      sqrOfSum += i;
    }

    sqrOfSum = sqrOfSum * sqrOfSum;
    
    Console.WriteLine("Solution: " + sqrOfSum + " - " + sumOfSqr + " = " + (sqrOfSum-sumOfSqr));
  }

  static void Euler7(){
    //Problem 7
    //By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
    //What is the 10 001st prime number?
    int prime = 2;
    int index = 1;
    while(true){
      if(index == 10001)
        break;
      if(isPrime(prime))
        index++;
      prime++;
    }

    Console.WriteLine("Solution:" + prime);
  }

  static void Euler8(){
    //Problem 8
    //The four adjacent digits in the 1000-digit number that have the greatest product are 9 × 9 × 8 × 9 = 5832.
    //Find the thirteen adjacent digits in the 1000-digit number that have the greatest product. What is the value of this product?
    string allDigits = @"
      73167176531330624919225119674426574742355349194934
      96983520312774506326239578318016984801869478851843
      85861560789112949495459501737958331952853208805511
      12540698747158523863050715693290963295227443043557
      66896648950445244523161731856403098711121722383113
      62229893423380308135336276614282806444486645238749
      30358907296290491560440772390713810515859307960866
      70172427121883998797908792274921901699720888093776
      65727333001053367881220235421809751254540594752243
      52584907711670556013604839586446706324415722155397
      53697817977846174064955149290862569321978468622482
      83972241375657056057490261407972968652414535100474
      82166370484403199890008895243450658541227588666881
      16427171479924442928230863465674813919123162824586
      17866458359124566529476545682848912883142607690042
      24219022671055626321111109370544217506941658960408
      07198403850962455444362981230987879927244284909188
      84580156166097919133875499200524063689912560717606
      05886116467109405077541002256983155200055935729725
      71636269561882670428252483600823257530420752963450";
    allDigits = allDigits.Replace(System.Environment.NewLine, "");
    allDigits = allDigits.Replace(" ", "");

    int limit = 13;
    long maxProduct = 1;
    long product = 1;
    string digits = "";
    for (int i = 0; i < allDigits.Length - limit; i++){
      product = 1;
      for(int j = 0; j < limit; j++){
        product *= long.Parse(allDigits[i+j] + "");
      } 
      if (product > maxProduct)
        maxProduct = product;
    }

    Console.WriteLine("Solution: " + digits + " Prodcut: " + maxProduct);
  }

  static void Euler9(){
    //Problem 9
    //A Pythagorean triplet is a set of three natural numbers, a < b < c, for which,
    //a^2 + b^2 = c^2
    //For example, 3^2 + 4^2 = 9 + 16 = 25 = 5^2.
    //There exists exactly one Pythagorean triplet for which a + b + c = 1000.
    //Find the product abc.
    //
    //First, based on sum = 1000 we can assume:
    //+--------------+
    //| c <250, 499> |
    //| b <249, 499> |
    //| a <1, 249>   |
    //+--------------+
    //The Pythagorean triple is 200, 375, 425, and the sum is 1000
    //The product is 31875000

    int sum = 1000;
    int a = 0, b = 0, c = 0, cycles = 0;
    bool isPythagorean = false;
    for (a = 1; a < 250; a++){
      for(b = 249; b < 500; b++){
        cycles++;
        c = sum - a - b;
        if((a*a + b*b) == c*c){
          isPythagorean = true;
          break;
        }
      }
      if(isPythagorean)
        break;
    }

    Console.WriteLine("a = {0}, b = {1}, c = {2}", a, b, c);
    Console.WriteLine("For loop cycles: " + cycles);
  }

  static void Euler10(){
    //The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
    //Find the sum of all the primes below two million.
    //Result: 142913828922
    //Brute Force (about 4 min):
    //long sum =i 2;
    //for(int i = 3; i < 2000000; i+=2){
    //  if(isPrime(i))
    //    sum+=i;
    //}
   
    //Using Sieve of Eratosthenes (about 0,04s) 
    long sum = 0;
    int j = 0;
    //Create tab with false values
    bool[] tab = new bool[2000000];

    //Sieve of Eratosthenes
    for(int i = 2; i*i < tab.Length; i++){
      j = 2;
      while(j*i < tab.Length){
        tab[i*j] = true;
        j++;
      }
    }
    //Sum all primes and it's done. 
    for(int i = 2; i < tab.Length; i++){
      if(tab[i] == false){
        sum += i;
      }
    }

    Console.WriteLine("Solution: " + sum);
  }

  static void OptionsChooser(){
    Console.Write("Choose problem (1-10): ");
    string option = Console.ReadLine();
    Stopwatch sw = new Stopwatch ();
    sw.Start();
    switch (option){
      case "1":
        Euler1();
        break;
      case "2":
        Euler2();
        break; 
      case "3":
        Euler3();
        break;
      case "4":
        Euler4();
        break;
      case "5":
        Euler5();
        break;
      case "6":
        Euler6();
        break;
      case "7":
        Euler7();
        break;
      case "8":
        Euler8();
        break;
      case "9":
        Euler9();
        break;
      case "10":
        Euler10();
        break;
      default:
        Console.WriteLine("Wrong option");
        break;
    }
    sw.Stop();
    Console.WriteLine("Time: " + sw.Elapsed);
  }

  static bool isDivisible(int num){
    for(int i = 1; i <= 20; i++){
      if(num % i != 0){
        return false;
      }
    }
    return true;
  }

  static bool isPalindrom(int num){
    string sNum = num.ToString();
    for (int k = 0; k < sNum.Length/2; k++){
      if (sNum[k] != sNum[sNum.Length - 1 - k]){
        return false;
      } 
    }
    return true;
  }  

  static bool isPrime(int num){
    for (int i = 2; i < num/2; i++){
      if (num % i == 0)
        return false;
    }
    return true;
  }
 }

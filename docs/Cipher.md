# Cipher

I thought that it deserve some docs. So... Here we go:

## How it works

For loop which code each letter separate.
1. Convert letter to int value in ASCII. For example "a" has value 97 (case sensitive)
1. Convert key letter to int value, but minus 32. Normal letters start with value 32 (Space or " ") and it need to measure how much move letter in ASCII table. So letter "a" with key value 1, should be moved by 1 not by 32. Just like in `Caesar cipher`.
1. Swift letter by value of key int. For example:
    * Letter "a" has value 97.
    * Key "1" has value 49. Minus 32, so it will be 17.
    * Simple 97 + 17 = 114.
    * Value of 114 is "r".
    * Voile, letter "a" encoded by key "1" is "r".

OK, but what if you have text longer than key?
Simple, you start reading key from begging.

## Example
text = Message
key = pass

Coding:
  * **M** --> **p** = **?**
  * **e** --> **a** = **H**
  * **s** --> **s** = **h**
  * **s** --> **s** = **h**
  * **a** --> **p** = **S**
  * **g** --> **a** = **J**
  * **e** --> **s** = **Z**

Encrypted text: ?HhhSJZ

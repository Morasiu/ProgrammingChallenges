#!/usr/bin/python
# coding=utf-8

import sys, random

print("\n<--------Name generator 0.1.1--------->")
print("Created by Morasiu (morasiu2@gmail.com)\n")
try:
    lengh = int(raw_input("Enter name lengh: "))
except:
    print("Not a number")
    sys.exit()

consonants = "bcdfghjklmnqprstvwxz"
vovels = "aeiouy"

name = ""
# Should name start with vovel.
start_vovel = random.randint(0,1)


for x in range(0, lengh):
    if len(name) >lengh:
        continue

    if start_vovel:
        if x%2==1:
            name += random.choice(consonants) 
            if random.randint(0,3) == 0:
                name += random.choice(consonants) 
        else:
            name += random.choice(vovels)
    else:
        if x%2==1:
            name += random.choice(vovels) 
        else:
            name += random.choice(consonants)
            if random.randint(0,3) == 0:
                name += random.choice(consonants)


name = name.title()
print("Your name:\n"+ name)

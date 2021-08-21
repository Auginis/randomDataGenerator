import os
fileA = "Names.txt"
fileB = "Surnames.txt"
files = [fileA, fileB]

if os.path.exists("count.txt"):
    os.remove("count.txt")

for temp in files:
    with open(temp, 'r', encoding='utf8') as input:
        lines = input.readlines()
        for line in lines:
            words = line.split(' ')
            file = open("count.txt", 'a')
            file.write("{}\n".format(len(words)))
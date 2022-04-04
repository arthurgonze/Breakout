def addBackSlash(string):
    new_string = ""
    for char in string:
        if char == '"':
            new_string += "\\" + char
        else:
            new_string += char
    return new_string

def main():
    filePath = "#insert right path #/Breakout/Code Breaker/Assets/Game/Scripts/Blockchain/leaderboard_abi.json"
    resultPath = "#insert right path #/Breakout/Code Breaker/Assets/Game/Scripts/Blockchain/leaderboard_abi_result.txt"
    horizontalABI = ""

    # Create the horizontal file
    inputFile = open(filePath, 'r')
    line = inputFile.readline()
    while line != "":
        for char in line:
            if(char != "\n" and char != " " and char != "\t"):
                horizontalABI += char
        line = inputFile.readline()
    inputFile.close()

    # put the backslashs in the new string
    horizontalABI = addBackSlash(horizontalABI)

    # write the new string in the file
    resultFile = open(resultPath, 'w')
    resultFile.write(horizontalABI)
    resultFile.close()


if __name__ == '__main__':
    main()


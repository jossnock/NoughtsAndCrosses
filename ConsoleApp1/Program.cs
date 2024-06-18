using System.Diagnostics;
/*
To-do:
 - Generalise the functions to work with n-by-n size board
     - Select board size
 - Improve checking system:
     - Check top left corner for matches in adjacent cells (proceed if match, else break)
     - Check opposite corner for matches in adjacent cells (proceed if match, else break)
     - Check top right corner for anti-diagonal matches
     - Check centre for row/column matches
*/

// defining methods:

static void NCPrint(char[,] inputBoard)
/* 
Prints the current state of the board.
*/
{
    int BoardLength = inputBoard.GetLength(0); // BoardWidth = BoardHeight b/c board will always be a square

    // Dynamically constructing rowSeparators:

    string rowTop = "┌─";
    for (int i = 0; i < BoardLength - 1; i++) rowTop += "──┬─";
    rowTop += "──┐";
    // So for BoardLength = 2, rowTop = "┌───┬───┐" (allowing 2 spots for sigils per row)

    string rowMiddle = "├─";
    for (int i = 0; i < BoardLength - 1; i++) rowMiddle += "──┼─";
    rowMiddle += "──┤";
    // So for BoardLength = 5, rowMiddle = "├───┼───┼───┼───┼───┤"

    string rowBottom = "└─";
    for (int i = 0; i < BoardLength - 1; i++) rowBottom += "──┴─";
    rowBottom += "──┘";
    // So for BoardLength = 3, rowBottom = "└───┴───┴───┘"

    // Creating the rowSeparators Array:
    string[] rowSeparators = new string[BoardLength + 1]; // + 1 to account for rowBottom
    rowSeparators[0] = rowTop;
    for (int i = 1; i < BoardLength; i++) rowSeparators[i] = rowMiddle; // Populates array with (BoardLength - 1) 'rowMiddle's
    rowSeparators[BoardLength] = rowBottom;
    // So for BoardLength = 4, rowSeparators = { "┌───┬───┬───┬───┐",
    //                                           "├───┼───┼───┼───┤",
    //                                           "├───┼───┼───┼───┤",
    //                                           "├───┼───┼───┼───┤",
    //                                           "└───┴───┴───┴───┘" } 


    for (int row = 0; row < BoardLength; row++)
    {
        Console.WriteLine(rowSeparators[row]);
        for (int column = 0; column < BoardLength; column++)
        {
            Console.Write("| "); // separator between each char
            Console.Write(inputBoard[row, column]);
            Console.Write(' '); // spacing after each char
        }
        Console.WriteLine('|'); // separator between end char and makes a new line after each row
    }
    Console.WriteLine(rowSeparators[BoardLength]);
}


static char CheckForWin(char[,] inputBoard)
/*
Checks if, in the given board, a player has won.
Returns the winning player's sigil (e.g. 'X').
*/
{
    int BoardLength = inputBoard.GetLength(0); // BoardWidth = BoardHeight b/c board will always be a square

    // Horizontal:
    for (int row = 0; row < BoardLength; row++)
    {
        for (int column = 1; column < BoardLength; column++) // starts on 1 to not check if inputBoard[row, 0] != itself
        {
            if (inputBoard[row, 0] != inputBoard[row, column]) break; // stop checking row if not matching
            if (column == BoardLength - 1) return inputBoard[row, 0]; // if after all the checks the statement hasn't been broken out of, they must all match
        }        
    }

    // Vertical:
    for (int column = 0; column < BoardLength; column++)
    {
        for (int row = 1; row < BoardLength; row++) // starts on 1 to not check if inputBoard[0, column] != itself
        {
            if (inputBoard[0, column] != inputBoard[row, column]) break; // stop checking column if not matching
            if (row == BoardLength - 1) return inputBoard[0, column]; // if after all the checks the statement hasn't been broken out of, they must all match
        }        
    }

    // Main diagonal:
    for (int coords = 1; coords < BoardLength; coords++) // starts on 1 to not check if inputBoard[0, 0] != itself
    {
        if (inputBoard[0, 0] != inputBoard[coords, coords]) break; // stop checking diagonal if not matching
        if (coords == BoardLength - 1) return inputBoard[0, 0]; // if after all the checks the statement hasn't been broken out of, they must all match
    }

    // Anti-diagonal:
    for (int row = 1, column = BoardLength - 2; (row < BoardLength) & (column >= 0); row++, column--) // starts on 1 and BoardLength - 2 to not check if inputBoard[0, BoardLength - 1] != itself
    {
        if (inputBoard[0, BoardLength - 1] != inputBoard[row, column]) break; // stop checking diagonal if not matching
        if (row == BoardLength - 1) return inputBoard[0, BoardLength - 1]; // if after all the checks the statement hasn't been broken out of, they must all match
    }

    // default case:
    return ' '; // neither player has won yet
}


// Playing the game:

// Dynamically setting boardSize and defining board:

char[,] board = { { '1', '2', '3' }, 
                  { '4', '5', '6' }, 
                  { '7', '8', '9' } };




int currentPlayer = 0; // 0 = player 1 because it makes converting from player number to X or O and switching between p1 and p2 easier
char[] Players = { 'X', 'O' };
int turnNum = 1;

while (CheckForWin(board) == ' ')
{
    NCPrint(board);
    Console.WriteLine($"- - - Player {currentPlayer + 1}'s turn - - -");

    Console.WriteLine($"Player {currentPlayer + 1}, choose a space (1-9): "); // since currentPlayer starts on 0, 1 must be added
    char space = Console.ReadLine()[0]; // convert input string of 1 letter to char

    Console.WriteLine("- - - - - - - - - - - - - -"); // buffer between rounds

    if (!Char.IsDigit(space)) // if the space isn't within the board
    {
        Console.WriteLine("That space isn't on the board, choose a space within rows 0-2 and columns 0-2");
        continue; // skips the switch between players and the board update
    }

    // converting space name to corresponding row and column:
    int row = 0, column = 0; // defining vars in current scope to change them in switch statement
    switch (space)
    {
        case '1':
            break;
        case '2':
            column += 1;
            break;
        case '3':
            column += 2;
            break;
        case '4':
            row += 1;
            break;
        case '5':
            row += 1; column += 1;
            break;
        case '6':
            row += 1; column += 2;
            break;
        case '7':
            row += 2;
            break;
        case '8':
            row += 2; column += 1;
            break;
        case '9':
            row += 2; column += 2;
            break;
    }

    if (!Char.IsDigit(board[row, column])) // if the space is already filled with a sigil
    {
        Console.WriteLine("That space is already filled, choose an empty space");
        continue; // skips the switch between players and the board update
    }
    
    board[row, column] = Players[currentPlayer]; // updating board with X or O in selected space (depending on the player)

    // Switching currentPlayer between 0 and 1 and updating turnNum:
    currentPlayer++;
    currentPlayer %= 2;
    turnNum += 1;

    if (turnNum > 9)
    {
        break;
    }
}

NCPrint(board);

char winner = CheckForWin(board);

if (winner == 'X') Console.WriteLine("Player 1 wins!");
else if (winner == 'O') Console.WriteLine("Player 2 wins!");
else Console.WriteLine("Draw!");
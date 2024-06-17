// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

Console.WriteLine("Hello, World!");

// temp variables:
char[,] board = { {'O', 'X', ' '}, 
                  {' ', 'O', ' '}, 
                  {'X', ' ', 'X'} };

// defining methods:

static void NCPrint(char[,] inputBoard)
    // Prints the current state of the board:
    {
        for (int row = 0; row < 3; row++)
        {
            for (int column = 0; column < 3; column++)
            {
                Console.Write(inputBoard[row, column]);
                Console.Write(' '); // spacing after each char
            }
            Console.Write('\n'); // new line after each row
        }
    }

    static char CheckForWin(char[,] inputBoard)
    {
        // Horizontal:
        for (int row = 0; row < 3; row++)
        {
            if ((inputBoard[row, 0] == inputBoard[row, 1]) & (inputBoard[row, 1] == inputBoard[row, 2]) & (inputBoard[row, 0] != ' ')) // if all chars in row are the same and row is not empty
            {
                return inputBoard[row, 1];
            }
        }

        // Vertical:
        // for (int row = 0; row < 3; row++)
        // {
        //     if ((inputBoard[row, 0] == inputBoard[row, 1]) & (inputBoard[row, 1] == inputBoard[row, 2]) & (inputBoard[row, 0] != ' ')) // if all chars in row are the same and row is not empty
        //     {
        //         return inputBoard[row, 1];
        //     }
        // }

        // Diagonal:





        return ' '; // neither player has won yet
    }

// Playing the game:

bool gameWon = false;

while (gameWon == false)
{

    


    NCPrint(board);

}

// Asserts:

char[,] tempBoard1 = { {'O', 'O', 'O'}, 
                       {' ', 'X', ' '}, 
                       {'X', ' ', 'X'} };
Debug.Assert(CheckForWin(tempBoard1) == 'O'); // Horizontal

char[,] tempBoard2 = { {'O', 'X', 'O'}, 
                       {' ', 'X', ' '}, 
                       {'O', 'X', 'O'} };
Debug.Assert(CheckForWin(tempBoard2) == 'O'); // Vertical

// char[,] tempBoard3 = { {'X', 'O', 'O'}, 
//                        {' ', 'X', ' '}, 
//                        {'O', ' ', 'X'} };
// Debug.Assert(CheckForWin(tempBoard3) == 'O'); // Diagonal
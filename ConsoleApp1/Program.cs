// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

// defining methods:

static void NCPrint(char[,] inputBoard)
    {
        // Prints the current state of the board:
        string[] rowSeparators = { "┌───┬───┬───┐", "├───┼───┼───┤", "├───┼───┼───┤", "└───┴───┴───┘" };

        for (int row = 0; row < 3; row++)
        {
            Console.WriteLine(rowSeparators[row]);
            for (int column = 0; column < 3; column++)
            {
                Console.Write("| "); // separator between each char
                Console.Write(inputBoard[row, column]);
                Console.Write(' '); // spacing after each char
            }
            Console.WriteLine('|'); // separator between eand char + new line after each row
        }
        Console.WriteLine(rowSeparators[3]);
    }

    static char CheckForWin(char[,] inputBoard)
    {
        // Horizontal:
        for (int row = 0; row < 3; row++)
        {
            if ((inputBoard[row, 0] == inputBoard[row, 1]) & (inputBoard[row, 1] == inputBoard[row, 2]) & (inputBoard[row, 0] != ' ')) // if all chars in row are the same and row is not empty
            {
                return inputBoard[row, 0];
            }
        }

        // Vertical:
        for (int column = 0; column < 3; column++)
        {
            if ((inputBoard[0, column] == inputBoard[1, column]) & (inputBoard[1, column] == inputBoard[2, column]) & (inputBoard[0, column] != ' ')) // if all chars in column are the same and column is not empty
            {
                return inputBoard[0, column];
            }
        }

        // Main diagonal:
        if ((inputBoard[0, 0] == inputBoard[1, 1]) & (inputBoard[1, 1] == inputBoard[2, 2]) & (inputBoard[0, 0] != ' ')) // if all chars in diagonal are the same and diagonal is not empty
        {
            return inputBoard[0, 0];
        }

        // Anti-diagonal:
        if ((inputBoard[0, 2] == inputBoard[1, 1]) & (inputBoard[1, 1] == inputBoard[2, 0]) & (inputBoard[0, 2] != ' ')) // if all chars in diagonal are the same and diagonal is not empty
        {
            return inputBoard[0, 2];
        }
        return ' '; // neither player has won yet
    }

// Playing the game:

char[,] board = { { ' ', ' ', ' ' }, 
                  { ' ', ' ', ' ' }, 
                  { ' ', ' ', ' ' } };

int currentPlayer = 0; // 0 = player 1 because it makes converting from player number to X or O and switching between p1 and p2 easier
char[] PLAYERS = { 'X', 'O' };


while (CheckForWin(board) == ' ')
{
    NCPrint(board);
    Console.WriteLine($"- - - Player {currentPlayer + 1}'s turn - - -");

    Console.WriteLine($"Player {currentPlayer + 1}, choose a row (0, 1, or 2): "); // since currentPlayer is either 0 or 1, 1 must be added
    int row = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine($"Player {currentPlayer + 1}, choose a column (0, 1, or 2): "); // since currentPlayer is either 0 or 1, 1 must be added
    int column = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("- - - - - - - - - - - - - -"); // buffer between rounds

    board[row, column] = PLAYERS[currentPlayer]; // updating board

    // Switching currentPlayer between 0 and 1:
    currentPlayer++;
    currentPlayer %= 2;
}

NCPrint(board);

char winner = CheckForWin(board);

if (winner == 'X') Console.WriteLine("Player 1 wins!");
else if (winner == 'O') Console.WriteLine("Player 2 wins!");
else Console.WriteLine("Draw!");



// Asserts:

char[,] tempBoard1 = { { 'O', 'O', 'O' }, 
                       { ' ', 'X', ' ' }, 
                       { 'X', ' ', 'X' } };
Debug.Assert(CheckForWin(tempBoard1) == 'O'); // Horizontal

char[,] tempBoard2 = { { 'O', 'X', 'O' }, 
                       { ' ', 'X', ' ' }, 
                       { ' ', 'X', ' ' } };
Debug.Assert(CheckForWin(tempBoard2) == 'X'); // Vertical

char[,] tempBoard3 = { { 'X', 'O', ' ' }, 
                       { ' ', 'X', ' ' }, 
                       { 'O', ' ', 'X' } };
Debug.Assert(CheckForWin(tempBoard3) == 'X'); // Main diagonal

char[,] tempBoard4 = { { 'X', 'X', 'O' }, 
                       { 'X', 'O', ' ' }, 
                       { 'O', ' ', ' ' } };
Debug.Assert(CheckForWin(tempBoard3) == 'X'); // Anti-diagonal
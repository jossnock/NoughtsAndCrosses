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

static void PlayNC()
{
    char[,] board = { { '1', '2', '3' }, 
                      { '4', '5', '6' }, 
                      { '7', '8', '9' } };
    int currentPlayer = 0; // 0 = player 1 because it makes converting from player number to X or O and switching between p1 and p2 easier
    char[] PLAYERS = { 'X', 'O' };
    int turnNum = 1;

    while (CheckForWin(board) == ' ')
    {
        NCPrint(board);
        Console.WriteLine($"- - - Player {currentPlayer + 1}'s turn - - -");

        Console.WriteLine($"Player {currentPlayer + 1}, choose a space (1-9): "); // since currentPlayer is either 0 or 1, 1 must be added
        char space = Console.ReadLine()[0]; // convert input string of 1 letter to char

        Console.WriteLine("- - - - - - - - - - - - - -"); // buffer between rounds

        if (!Char.IsDigit(space)) // if the space isn't within the board
        {
            Console.WriteLine("That space isn't on the board, choose a space within rows 0-2 and columns 0-2");
            continue; // skips the switch between players and the board update
        }

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

        if (!Char.IsDigit(board[row, column])) // if the space is already filled
        {
            Console.WriteLine("That space is already filled, choose an empty space");
            continue; // skips the switch between players and the board update
        }
        
        board[row, column] = PLAYERS[currentPlayer]; // updating board with X or O in selected space (depending on the player)

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
}

PlayNC();


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
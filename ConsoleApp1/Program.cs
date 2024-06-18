// defining methods:

static void NCPrint(string[,] inputBoard)
/* 
Prints the current state of the board.
*/
{
    int boardSizeSquared = inputBoard.Length;
    int boardMaxDigits = Convert.ToString(boardSizeSquared).Length; // number of digits in the highest number on the board

    int BoardLength = inputBoard.GetLength(0); // BoardWidth = BoardHeight b/c board will always be a square

    // Dynamically constructing rowSeparators:

    string rowTop = "┌─";
    for (int i = 0; i < BoardLength - 1; i++) rowTop += "─┬─".PadLeft(3 + boardMaxDigits, '─');
    rowTop += "─┐".PadLeft(2 + boardMaxDigits, '─');
    // So for BoardLength = 2, rowTop = "┌───┬───┐" (allowing 2 spots for symbols per row)

    string rowMiddle = "├─";
    for (int i = 0; i < BoardLength - 1; i++) rowMiddle += "─┼─".PadLeft(3 + boardMaxDigits, '─');
    rowMiddle += "─┤".PadLeft(2 + boardMaxDigits, '─');
    // So for BoardLength = 5, rowMiddle = "├────┼────┼────┼────┼────┤"

    string rowBottom = "└─";
    for (int i = 0; i < BoardLength - 1; i++) rowBottom += "─┴─".PadLeft(3 + boardMaxDigits, '─');
    rowBottom += "─┘".PadLeft(2 + boardMaxDigits, '─');
    // So for BoardLength = 3, rowBottom = "└───┴───┴───┘"

    // Creating the rowSeparators Array:
    string[] rowSeparators = new string[BoardLength + 1]; // + 1 to account for rowBottom
    rowSeparators[0] = rowTop;
    for (int i = 1; i < BoardLength; i++) rowSeparators[i] = rowMiddle; // Populates array with (BoardLength - 1) 'rowMiddle's
    rowSeparators[BoardLength] = rowBottom;
    // So for BoardLength = 4, rowSeparators = { "┌────┬────┬────┬────┐",
    //                                           "├────┼────┼────┼────┤",
    //                                           "├────┼────┼────┼────┤",
    //                                           "├────┼────┼────┼────┤",
    //                                           "└────┴────┴────┴────┘" } 


    for (int row = 0; row < BoardLength; row++)
    {
        Console.WriteLine(rowSeparators[row]);
        for (int column = 0; column < BoardLength; column++)
        {
            Console.Write("| "); // separator between each symbol
            Console.Write(inputBoard[row, column]);
            Console.Write(' '); // spacing after each symbol
        }
        Console.WriteLine('|'); // separator between end symbol and makes a new line after each row
    }
    Console.WriteLine(rowSeparators[BoardLength]);
}


static char CheckForWin(string[,] inputBoard)
/*
Checks if, in the given board, a player has won.
Returns the winning player's symbol (e.g. 'X').
*/
{
    int BoardLength = inputBoard.GetLength(0); // BoardWidth = BoardHeight b/c board will always be a square

    // Horizontal:
    for (int row = 0; row < BoardLength; row++)
    {
        for (int column = 1; column < BoardLength; column++) // starts on 1 to not check if inputBoard[row, 0] != itself
        {
            if (inputBoard[row, 0] != inputBoard[row, column]) break; // stop checking row if not matching
            // if after all the checks the statement hasn't been broken out of, they must all match:
            if (column == BoardLength - 1) return inputBoard[row, 0][0]; // [0] to convert single-character string to char (e.g. "X" => 'X')
        }        
    }

    // Vertical:
    for (int column = 0; column < BoardLength; column++)
    {
        for (int row = 1; row < BoardLength; row++) // starts on 1 to not check if inputBoard[0, column] != itself
        {
            if (inputBoard[0, column] != inputBoard[row, column]) break; // stop checking column if not matching
            // if after all the checks the statement hasn't been broken out of, they must all match:
            if (row == BoardLength - 1) return inputBoard[0, column][0]; // [0] to convert single-character string to char (e.g. "X" => 'X')
        }        
    }

    // Main diagonal:
    for (int coords = 1; coords < BoardLength; coords++) // starts on 1 to not check if inputBoard[0, 0] != itself
    {
        if (inputBoard[0, 0] != inputBoard[coords, coords]) break; // stop checking diagonal if not matching
        // if after all the checks the statement hasn't been broken out of, they must all match:
        if (coords == BoardLength - 1) return inputBoard[0, 0][0]; // [0] to convert single-character string to char (e.g. "X" => 'X')
    }

    // Anti-diagonal:
    for (int row = 1, column = BoardLength - 2; (row < BoardLength) & (column >= 0); row++, column--) // starts on 1 and BoardLength - 2 to not check if inputBoard[0, BoardLength - 1] != itself
    {
        if (inputBoard[0, BoardLength - 1] != inputBoard[row, column]) break; // stop checking diagonal if not matching
        // if after all the checks the statement hasn't been broken out of, they must all match:
        if (row == BoardLength - 1) return inputBoard[0, BoardLength - 1][0]; // [0] to convert single-character string to char (e.g. "X" => 'X')
    }

    // default case:
    return ' '; // neither player has won yet
}


// Playing the game:

// Dynamically setting boardSize and defining board:

Console.Write("Choose a board size: ");
int boardSize = Convert.ToInt32(Console.ReadLine());
string[,] board = new string[boardSize, boardSize];
int currentSpaceNum = 1; // the number representing each space, it increments later
int boardMaxDigits = Convert.ToString(boardSize * boardSize).Length; // number of digits in the highest number on the board

// Adding in numbers and spaces
for (int row = 0; row < boardSize; row++)
{
    for (int column = 0; column < boardSize; column++)
    {
        board[row, column] = Convert.ToString(currentSpaceNum)
                                .PadLeft(boardMaxDigits, '0');// Adding leading 0s to make the board align with 2+ digit numbers
        currentSpaceNum++;
    }
}

// So for boardSize = 4, board = { { "01", "02", "03", "04" }, 
//                                 { "05", "06", "07", "08" }, 
//                                 { "09", "10", "11", "12" }, 
//                                 { "13", "14", "15", "16" } };

int currentPlayer = 0; // 0 = player 1 because it makes converting from player number to X or O and switching between p1 and p2 easier
string[] PlayerSymbols = { "X".PadLeft(boardMaxDigits, 'X'), "O".PadLeft(boardMaxDigits, 'O') }; // string b/c the player symbols are used in board
int turnNum = 1;

while (CheckForWin(board) == ' ')
{
    NCPrint(board);
    Console.WriteLine($"- - - Player {currentPlayer + 1}'s turn - - -"); // since currentPlayer starts on 0, 1 must be added

    Console.WriteLine($"Player {currentPlayer + 1}, choose a space ({"1".PadLeft(boardMaxDigits, '0')}-{boardSize * boardSize}): "); // leading 0s to account for leading 0s in space numbers
    int space = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("- - - - - - - - - - - - - -"); // buffer between rounds

    if (space > boardSize * boardSize) // if the space is outside the bounds of the board
    {
        Console.WriteLine($"That space isn't on the board, choose a space ({"1".PadLeft(boardMaxDigits, '0')}-{boardSize * boardSize})"); // leading 0s to account for leading 0s in space numbers
        continue; // skips the switch between players and the board update
    }

    // converting space to corresponding row and column:
    int row = (space - 1) / boardSize; // - 1 accounts for 0-indexed co-ords
    int column = (space - 1) % boardSize; // - 1 accounts for 0-indexed co-ords

    if ((board[row, column] == "X") | (board[row, column] == "O")) // if the space is already filled with a symbol // [EDIT to account for n players]
    {
        Console.WriteLine("That space is already filled, choose an empty space");
        continue; // skips the switch between players and the board update
    }

    board[row, column] = PlayerSymbols[currentPlayer]; // updating board with X or O in selected space (depending on the player)

    // Switching currentPlayer between 0 and 1 and updating turnNum:
    currentPlayer++;
    currentPlayer %= 2;
    turnNum += 1;

    if (turnNum > boardSize * boardSize) // if all spaces are filled
    {
        break;
    }
}

NCPrint(board);

char winner = CheckForWin(board);

if ((winner == 'X') | (board.Length == 1)) Console.WriteLine("Player 1 wins!"); // if board.Length == 1 and the board is full, player 1 must've won
else if (winner == 'O') Console.WriteLine("Player 2 wins!");
else Console.WriteLine("Draw!");
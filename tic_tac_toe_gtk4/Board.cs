using GtkDotNet;
using GtkDotNet.SafeHandles;

namespace tic_tac_toe_gtk4;

public class Board
{
    private readonly char[,] _board;
    public char Player { get; private set; } = 'X';
    public enum Winner
    {
        Blank,
        Tie,
        X,
        O
    }
    
    public Board(int sideLength)
    {
        _board = new char[sideLength, sideLength];
        for (var i = 0; i < sideLength; i++)
        {
            for (var j = 0; j < sideLength; j++)
            {
                _board[i, j] = ' ';
            }
        }
    }

    public void ResetBoard(ObjectRef<ButtonHandle>[] buttonHandles)
    {
        for (var i = 0; i < _board.GetLength(0); i++)
        {
            for (var j = 0; j < _board.GetLength(1); j++)
            {
                _board[i, j] = ' ';
            }
        }

        foreach (var buttonHandle in buttonHandles)
        {
            buttonHandle.Ref.Label("");
        }
    }
    
    public char UpdateBoard(int row, int column)
    {
        if (_board[row, column] == ' ')
        {
            _board[row, column] = Player;
            Player = Player == 'X' ? 'O' : 'X';
        }
        return _board[row, column];
    }

    public Winner CheckBoard(int row, int column, int squaresToWin)
    {
        var lastPlayer = _board[row, column];
        int CountInDirection(int dRow, int dCol)
        {
            var count = 1; // one square already

            for (var dir = -1; dir <= 1; dir += 2) // check both directions
            {
                var r = row + dir * dRow;
                var c = column + dir * dCol;
                while (r >= 0 && r < _board.GetLength(0) &&
                       c >= 0 && c < _board.GetLength(1) &&
                       _board[r, c] == lastPlayer)
                {
                    count++;
                    r += dir * dRow;
                    c += dir * dCol;
                }
            }
            return count;
        }

        // X or O winner check
        if (CountInDirection(0, 1) >= squaresToWin ||   // horizontal
            CountInDirection(1, 0) >= squaresToWin ||   // vertical
            CountInDirection(1, 1) >= squaresToWin ||   // diagonal
            CountInDirection(1, -1) >= squaresToWin)    // diagonal
        {
            return lastPlayer == 'X' ? Winner.X : Winner.O;
        }

        // Tie check
        var boardFull = true;
        foreach (var cell in _board)
        {
            if (cell == ' ')
            {
                boardFull = false;
                break;
            }
        }
        return boardFull ? Winner.Tie : Winner.Blank;
    }
}
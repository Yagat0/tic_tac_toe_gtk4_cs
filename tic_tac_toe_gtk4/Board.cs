using GtkDotNet;
using GtkDotNet.SafeHandles;

namespace tic_tac_toe_gtk4;

public class Board
{
    private char[,] _board;
    
    public Board(int sideLength)
    {
        _board = new char[sideLength, sideLength];
        for (int i = 0; i < sideLength; i++)
        {
            for (int j = 0; j < sideLength; j++)
            {
                _board[i, j] = ' ';
            }
        }
    }
    
    public char Player { get; set; } = 'X';

    public void ResetBoard(ObjectRef<ButtonHandle>[] buttonHandles)
    {
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                _board[i, j] = ' ';
            }
        }

        for (int i = 0; i < buttonHandles.Length; i++)
        {
            buttonHandles[i].Ref.Label("");
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

    public enum Winner
    {
        Blank,
        Tie,
        X,
        O
    }

    public Winner CheckBoard(int row, int column, int squaresToWin)
    {
        char lastPlayer = _board[row, column];
        if (lastPlayer == ' ')
            return Winner.Blank;

        int CountInDirection(int dRow, int dCol)
        {
            int count = 1; // one square already

            for (int dir = -1; dir <= 1; dir += 2) // check both directions
            {
                int r = row + dir * dRow;
                int c = column + dir * dCol;
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

        if (CountInDirection(0, 1) >= squaresToWin ||   // horizontal
            CountInDirection(1, 0) >= squaresToWin ||   // vertical
            CountInDirection(1, 1) >= squaresToWin ||   // diagonal
            CountInDirection(1, -1) >= squaresToWin)    // diagonal
        {
            return lastPlayer == 'X' ? Winner.X : Winner.O;
        }

        bool boardFull = true;
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
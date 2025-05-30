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
    
    public Winner CheckBoard()
    {
        return Winner.Blank;
    }
}
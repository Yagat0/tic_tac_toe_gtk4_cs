namespace tic_tac_toe_gtk4;

public class Board
{
    private char[,] _board = new char[3,3]
    {
        {' ',' ',' '},
        {' ',' ',' '},
        {' ',' ',' '}
    };

    public char Player { get; set; } = 'X';

    public void ResetBoard()
    {
        _board = new char[3,3] {
            {' ',' ',' '},
            {' ',' ',' '},
            {' ',' ',' '}
        };
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
}
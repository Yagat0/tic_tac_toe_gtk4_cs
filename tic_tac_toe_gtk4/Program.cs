using CsTools.Extensions;
using GtkDotNet;
using GtkDotNet.SafeHandles;
using tic_tac_toe_gtk4;

// Game parameters
const int sideLength = 3;
const int squaresToWin = 3;

var board = new Board(sideLength);
var boxHandle = new ObjectRef<BoxHandle>();
var buttonHandles = Enumerable.Repeat(0, sideLength * sideLength).Select(h => new ObjectRef<ButtonHandle>()).ToArray();
var labelHandle = new ObjectRef<LabelHandle>();

void ButtonOnClicked(int row, int column)
{
    buttonHandles[row * sideLength + column].Ref.Label($"{board.UpdateBoard(row, column)}");
    // TODO: Better two line text alignment
    labelHandle.Ref.Set($"Player: {board.Player}");
    
    Board.Winner winner = board.CheckBoard(row, column, squaresToWin);
    switch (winner)
    {
        case Board.Winner.X:
        case Board.Winner.O:
            labelHandle.Ref.Set($"{winner} is the winner!\nPlayer: X");
            board.ResetBoard(buttonHandles);
            break;

        case Board.Winner.Tie:
            labelHandle.Ref.Set("It's a tie!\nPlayer: X");
            board.ResetBoard(buttonHandles);
            break;

        case Board.Winner.Blank:
            break;
    }
}

return Application
    .New("org.gtk.tic_tac_toe")
    .OnActivate(app => 
        app.NewWindow()
            .SideEffect(w => StyleContext
                .AddProviderForDisplay(Display.GetDefault(), 
                                       CssProvider.New()
                                                  .FromResource("style"), 
                                       StyleProviderPriority.Application
                )
            )
           .Title("Tic Tac Toe")
           .Resizable(false)
           .SideEffect(win =>
           {
               var grid = Grid.New();
               for (var i = 0; i < sideLength; i++)
               {
                   for (var j = 0; j < sideLength; j++)
                   {
                       var row = j;
                       var column = i;
                       grid.Attach(
                           Button.NewWithLabel("")
                               .OnClicked(() => ButtonOnClicked(row, column))
                               .SizeRequest(80, 80)
                               .Margin(5)
                               .CssClass("square-button")
                               .Ref(buttonHandles[j * sideLength + i]),
                           i, j, 1, 1
                       );
                   }
               }
               
               win.Child(Box.New(Orientation.Vertical)
                   .Append(Label.New("Tic Tac Toe\nPlayer: X")
                       .Margin(10)
                       .CssClass("title-label")
                       .Ref(labelHandle))
                   .Append(grid)
               );
           })
            .Show())
    .Run(0, 0);
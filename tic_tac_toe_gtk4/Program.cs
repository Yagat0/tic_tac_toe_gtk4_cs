using CsTools.Extensions;
using GtkDotNet;
using GtkDotNet.SafeHandles;
using tic_tac_toe_gtk4;

var sideLength = 3;
var board = new Board(sideLength);
var buttonHandles = Enumerable.Repeat(0, sideLength * sideLength).Select(h => new ObjectRef<ButtonHandle>()).ToArray();

void ButtonOnClicked(int row, int column)
{
    buttonHandles[row * sideLength + column].Ref.Label($"{board.UpdateBoard(row, column)}");
    // TODO: Game logic
    board.CheckBoard();
}

return Application
    .New("org.gtk.tic_tac_toe")
    .OnActivate(app => 
        app.NewWindow()
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
                               .SizeRequest(50, 50)
                               .Ref(buttonHandles[j * sideLength + i]),
                           i, j, 1, 1
                       );
                   }
               }
               win.Child(Box.New(Orientation.Vertical)
                   .Append(Label.New("Tic Tac Toe"))
                   .Append(grid)
               );
           })
            .Show())
    .Run(0, 0);
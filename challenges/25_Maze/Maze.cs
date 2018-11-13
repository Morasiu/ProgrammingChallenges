using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

class Maze {
  private const string WallWithLeftSideEmpty = "    ";

  private delegate void Go();

  private const string WallWithLeftSideEmptyAndCursor = "  ■ ";

  private const string WallWithLeftSideFull = "|   ";

  private const string WallWithLeftSideFullAndCursor = "| ■ ";

  private const string FloorWithHole = "+   ";

  private const string FloorWithoutHole = "+---";

  private const string WallEnd = "|\n";

  private const string WallEndWithExit = " \n";

  private const string FloorCorner = "+\n";

  private static Cell[][] _maze;

  private static (int, int) _cursor;


  static void Main(string[] args) {
    Console.ForegroundColor = ConsoleColor.Green;
    Console.CursorVisible = false;
    Console.Title = "Maze";
    Console.WriteLine("<----------------Maze----------------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Console.WriteLine("Press any key to start.");
    Console.ReadKey();
    Start();
  }

  static void Start() {
    PrintTitle();
    ChooseOption();
    Console.ReadKey();
  }

  private static void ChooseOption() {
    Console.WriteLine("Options");
    Console.WriteLine("1 - Generate maze");
    Console.WriteLine("2 - Generate maze with animations");
    Console.WriteLine("3 - Solve");
    Console.WriteLine("4 - Solve with animations");
    var isOptionCorrect = false;
    do {
      var key = Console.ReadKey().Key;
      switch (key) {
        case ConsoleKey.D1:
          GenerateMaze(false);
          isOptionCorrect = true;
          break;
        case ConsoleKey.D2:
          GenerateMaze(true);
          isOptionCorrect = true;
          break;
        case ConsoleKey.D3:
          Console.WriteLine("Soon");
          isOptionCorrect = true;
          break;
        case ConsoleKey.D4:
          Console.WriteLine("Soon");
          isOptionCorrect = true;
          break;
        default:
          Console.Write("\b");
          continue;
      }
    } while (!isOptionCorrect);
  }

  private static void GenerateMaze(bool showAnimation) {
    var width = GetWidthFromPlayer();
    var height = GetHeightFromPlayer();
    Console.WriteLine("Generating maze " + width + "x" + height);
    GenerateEmptyMaze(width, height);

    DepthFirstSearchAlgorithm(showAnimation);

    PrintMaze(false);
    Console.WriteLine("Press any key to Quit");
    Console.ReadKey();
  }

  private static void DepthFirstSearchAlgorithm(bool showAnimation) {
    var random = new Random();
    // Entrance at (0, 0)
    _maze[0][0].Left = true;

    // Pick random place (why not?)
    _cursor = (random.Next(0, _maze.Length), random.Next(0, _maze[0].Length));
    _maze[_cursor.Item1][_cursor.Item2].Visited = true;

    // Add first item to stack
    var logStack = new Stack<(int, int)>();
    logStack.Push(_cursor);
    
    // Delegate to store method. I just wanted to try it...
    Go go = null;

    // Depth-first search algorithm

    do {
      if (IsDeadEnd(_cursor)) {
        if (logStack.Count <= 1) break;
        logStack.Pop();
        // Go back (backtrack)
        _cursor = logStack.Peek();
        if (showAnimation) {
          Thread.Sleep(100);
          PrintMaze(true);
        }
        continue;
      }

      if (IsBottomCellNotVisited(_cursor))
        go += GoBottom;
      
      if (IsTopCellNotVisited(_cursor))
        go += GoTop;

      if (IsLeftCellNotVisited(_cursor))
        go += GoLeft;

      if (IsRightCellNotVisited(_cursor))
        go += GoRight;

      // Get random direction
      go = (Go) go.GetInvocationList()[random.Next(0, go.GetInvocationList().Length)];

      //Goto that direction
      go();

      // Clear delegate
      go = null;

      // Add new entry to log
      logStack.Push(_cursor);

      if (showAnimation) {
        Thread.Sleep(300);
        PrintMaze(true);
      }

    } while (logStack.Count > 0);

    // Exit at bottom right corner
    _maze[_maze.Length - 1][_maze[0].Length - 1].Right = true;
  }
  private static void GoBottom() {
    _maze[_cursor.Item1][_cursor.Item2].Bottom = true;
    _cursor.Item1 += 1;
    _cursor.Item2 += 0;
    _maze[_cursor.Item1][_cursor.Item2].Visited = true;
    _maze[_cursor.Item1][_cursor.Item2].Top = true;
  }  
  
  private static void GoTop() {
    _maze[_cursor.Item1][_cursor.Item2].Top = true;
    _cursor.Item1 += -1;
    _cursor.Item2 += 0;
    _maze[_cursor.Item1][_cursor.Item2].Visited = true;
    _maze[_cursor.Item1][_cursor.Item2].Bottom = true;
  }

  private static void GoLeft() {
    _maze[_cursor.Item1][_cursor.Item2].Left = true;
    _cursor.Item1 += 0;
    _cursor.Item2 += -1;
    _maze[_cursor.Item1][_cursor.Item2].Visited = true;
    _maze[_cursor.Item1][_cursor.Item2].Right = true;
  }

  private static void GoRight() {
    _maze[_cursor.Item1][_cursor.Item2].Right = true;
    _cursor.Item1 += 0;
    _cursor.Item2 += 1;
    _maze[_cursor.Item1][_cursor.Item2].Visited = true;
    _maze[_cursor.Item1][_cursor.Item2].Left = true;
  }

  private static void PrintMaze(bool printCursor) {
    Console.Clear();
    PrintTitle();

    //+---+
    //|   | 1x1 block
    //+---+

    var board = " ";
    // Add first line
    board = AddOneLine(board);

    // Crate one maze in one string
    for (var row = 0; row < _maze.Length; row++) {
      for (var k = 1; k <= 2; k++) {
        board += " ";
        for (var column = 0; column < _maze[row].Length; column++) {
          board = AddMazeFragment(printCursor, board, row, k, column);
        }
        board = AddEndLines(board, k, row);
      }
    }

    // Print maze
    Console.WriteLine(board);
  }

  private static string AddMazeFragment(bool printCursor, string board, int row, int k, int column) {
    if (ShouldPrintVertical(k)) {
      board = AddWall(printCursor, board, row, column);
    } else {
      board = AddFloor(board, row, column);
    }

    return board;
  }

  private static string AddWall(bool printCursor, string board, int row, int column) {
    if (ShouldPrintWall(row, column))
      board = AddWallWithFullSideAndCursor(printCursor, board, row, column);
    else {
      board = AddWallWithFullSide(printCursor, board, row, column);
    }

    return board;
  }

  private static string AddFloor(string board, int row, int column) {
    if (ShouldPrintFloor(row, column))
      board += FloorWithoutHole;
    else {
      board += FloorWithHole;
    }

    return board;
  }

  private static string AddWallWithFullSide(bool printCursor, string board, int row, int column) {
    if (printCursor && CursorIsAtPosition(row, column))
      board += WallWithLeftSideEmptyAndCursor;
    else
      board += WallWithLeftSideEmpty;
    return board;
  }

  private static string AddWallWithFullSideAndCursor(bool printCursor, string board, int row, int column) {
    if (printCursor && CursorIsAtPosition(row, column))
      board += WallWithLeftSideFullAndCursor;
    else
      board += WallWithLeftSideFull;
    return board;
  }

  private static bool IsDeadEnd((int, int) cursor) {
    // Cell that has no unvisited neighbors being considered a dead-end ~ Wikipedia

    // cursor.Item1 - Row (Y)
    // cursor.Item2 - Column (X)

    // Top
    if (IsTopCellNotVisited(cursor)) {
      return false;
    }

    // Bottom
    if (IsBottomCellNotVisited(cursor)) {
      return false;
    }

    // Left
    if (IsLeftCellNotVisited(cursor)) {
      return false;
    }

    // Right
    if (IsRightCellNotVisited(cursor)) {
      return false;
    }

    return true;
  }

  private static bool IsRightCellNotVisited((int, int) cursor) {
    return IsCellOnRight(cursor) && !_maze[cursor.Item1][cursor.Item2 + 1].Visited;
  }

  private static bool IsLeftCellNotVisited((int, int) cursor) {
    return IsCellOnLeft(cursor) && !_maze[cursor.Item1][cursor.Item2 - 1].Visited;
  }

  private static bool IsBottomCellNotVisited((int, int) cursor) {
    return IsCellBelowCursor(cursor) && !_maze[cursor.Item1 + 1][cursor.Item2].Visited;
  }

  private static bool IsTopCellNotVisited((int, int) cursor) {
    return IsCellAboveCursor(cursor) && !_maze[cursor.Item1 - 1][cursor.Item2].Visited;
  }

  private static bool IsCellOnRight((int, int) cursor) {
    return cursor.Item2 + 1 < _maze[0].Length;
  }

  private static bool IsCellOnLeft((int, int) cursor) {
    return cursor.Item2 - 1 >= 0;
  }

  private static bool IsCellBelowCursor((int, int) cursor) {
    return cursor.Item1 + 1 < _maze.Length;
  }

  private static bool IsCellAboveCursor((int, int) cursor) {
    return cursor.Item1 - 1 >= 0;
  }

  private static bool CursorIsCorrect((int, int) cursor) {
    return cursor.Item1 - 1 >= 0 &&
           cursor.Item2 - 1 >= 0 &&
           cursor.Item1 + 1 < _maze.Length &&
           cursor.Item2 + 1 < _maze[0].Length;
  }

  private static bool CursorIsAtPosition(int row, int column) {
    return _cursor.Item1 == row && _cursor.Item2 == column;
  }

  private static bool ShouldPrintFloor(int row, int column) {
    return _maze[row][column].Bottom == false;
  }

  private static bool ShouldPrintWall(int row, int column) {
    return _maze[row][column].Left == false;
  }

  private static string AddEndLines(string board, int k, int row) {
    if (ShouldPrintVertical(k)) {
      if (_maze[row][_maze[0].Length - 1].Right) {
        board += WallEndWithExit;
      } else {
        board += WallEnd;
      }
    } else {
      board += FloorCorner;
    }

    return board;
  }

  private static bool ShouldPrintVertical(int k) {
    return k % 2 == 1;
  }

  private static string AddOneLine(string board) {
    for (var j = 0; j < _maze[0].Length; j++) {
      board += "+---";
    }

    board += "+\n";
    return board;
  }

  private static void GenerateEmptyMaze(int width, int height) {
    _maze = new Cell[height][];
    for (var row = 0; row < height; row++) {
      _maze[row] = new Cell[width];
      for (var column = 0; column < _maze[row].Length; column++) {
        _maze[row][column] = new Cell();
      }
    }
  }

  private static int GetWidthFromPlayer() {
    int width;
    Console.Write("\bEnter width: ");
    while (!int.TryParse(Console.ReadLine(), out width) || width < 1 || width > 100) {
      Console.WriteLine("Wrong value");
      Console.Write("\bEnter width: ");
    }

    return width;
  }

  private static int GetHeightFromPlayer() {
    int height;
    Console.Write("\bEnter height: ");
    while (!int.TryParse(Console.ReadLine(), out height) || height < 1 || height > 100) {
      Console.WriteLine("Wrong value");
      Console.Write("\bEnter height: ");
    }

    return height;
  }

  private static void PrintTitle() {
    Console.Clear();
    Console.WriteLine(" +----------+");
    Console.WriteLine(" |/        \\|");
    Console.WriteLine(" |   Maze   |");
    Console.WriteLine(" |\\        /|");
    Console.WriteLine(" +----------+");
    Console.Write("\n");
  }
}

class Cell {
  public bool Top;
  public bool Bottom;
  public bool Left;
  public bool Right;
  public bool Visited;
}

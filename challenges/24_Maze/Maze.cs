using System;
using System.Collections.Generic;

class Maze {

  private const string WallWithLeftSideEmpty = "=   ";
  private const string WallWithLeftSideFull =  "|   ";
  private const string FloorWithHole = "+   ";
  private const string FloorWithoutHole = "+---";
  private const string WallEnd = "|\n";
  private const string WallEndWithExit = "=\n";
  private const string FloorCorner = "+\n";
  private static Cell[][] _maze;

  static void Main(string[] args){
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
          GenerateMaze(showAnimation: false);
          isOptionCorrect = true;
          break;
        case ConsoleKey.D2:
          GenerateMaze(showAnimation: true);
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

    MazeGeneratingAlgorithm();

    PrintMaze();
    Console.ReadKey();
  }

  private static void MazeGeneratingAlgorithm() {
    // Starting point at (0, 0)
    _maze[0][0].Left = true;
    var cursor = (0, 0);
    var logStack = new Stack<(int, int)>();
    // Depth-first search algorithm

    do {
      _maze[cursor.Item1][cursor.Item2].Visited = true;
      logStack.Push(cursor);

      if (IsDeadEnd(cursor)) {
        logStack.Pop();
        if(logStack.Count == 0) break;
      }



    } while (cursor.Equals((0, 0)));


    // Exit at bottom right corner
    _maze[_maze.Length-1][_maze[0].Length-1].Right = true;
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
    if (IsLeftCellNotVisisted(cursor)) {
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

  private static bool IsLeftCellNotVisisted((int, int) cursor) {
    return IsCellOnLeft(cursor) && !_maze[cursor.Item1][cursor.Item2 - 1].Visited;
  }

  private static bool IsBottomCellNotVisited((int, int) cursor) {
    return IsCellBelowCursor(cursor) && !_maze[cursor.Item1 + 1][cursor.Item2].Visited;
  }

  private static bool IsTopCellNotVisited((int, int) cursor) {
    return IsCellAboveCursor(cursor) && !_maze[cursor.Item1 - 1][cursor.Item2].Visited;
  }

  private static bool IsCellOnRight((int, int) cursor) {
    return cursor.Item2 + 1 <= _maze[0].Length;
  }

  private static bool IsCellOnLeft((int, int) cursor) {
    return cursor.Item2 - 1 > 0;
  }

  private static bool IsCellBelowCursor((int, int) cursor) {
    return cursor.Item1 + 1 <= _maze.Length;
  }

  private static bool IsCellAboveCursor((int, int) cursor) {
    return cursor.Item1 - 1 > 0;
  }

  private static bool CursorIsCorrect((int, int) cursor) {
    return cursor.Item1 - 1 >= 0 &&
           cursor.Item2 - 1 >= 0 &&
           cursor.Item1 + 1 < _maze.Length &&
           cursor.Item2 + 1 < _maze[0].Length;
  }

  private static void PrintMaze() {
    Console.Clear();
    PrintTitle();
    
    //+---+
    //|   | 1x1 block
    //+---+

    var board = "";
    // Add first line
    board = AddOneLine(board);

    // Add all maze
    for (var row = 0; row < _maze.Length; row++) {
      for (var k = 1; k <= 2; k++) {
        for (var column = 0; column < _maze[row].Length; column++) {
          if (ShouldPrintVertical(k)) {
            if (ShouldPrintWall(row, column))
              board += WallWithLeftSideFull;
            else {
              board += WallWithLeftSideEmpty;
            }
          } else {
            if (ShouldPrintFloor(row, column))
              board += FloorWithoutHole;
            else {
              board += FloorWithHole;
            }
          }
        }
        board = AddEndLines(board, k, row);
      }
    }
    // Print maze
    Console.WriteLine(board);
  }

  private static bool ShouldPrintFloor(int row, int column) {
    return _maze[row][column].Bottom == false;
  }

  private static bool ShouldPrintWall(int row, int column) {
    return _maze[row][column].Left == false;
  }

  private static string AddEndLines(string board, int k, int row) {
    if (ShouldPrintVertical(k)) {
      if (_maze[row][_maze.Length-1].Right) {
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
    while (!int.TryParse(Console.ReadLine(), out width) || width < 2 || width > 100) {
      Console.WriteLine("Wrong value");
      Console.Write("\bEnter width: ");
    }

    return width;
  }

  private static int GetHeightFromPlayer() {
    int height;
    Console.Write("\bEnter height: ");
    while (!int.TryParse(Console.ReadLine(), out height) || height < 2  || height > 100) {
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

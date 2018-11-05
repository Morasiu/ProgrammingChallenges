using System;
using System.Collections.Generic;

class Maze {

  private static Cell[][] maze;

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

    // TODO Algorithm
    PrintMaze();
    Console.ReadKey();
  }

  private static void PrintMaze() {

    //+---+
    //|   | 1x1 block
    //+---+

    var board = "";
    // Add first line
    board = AddOneLine(board);

    // Add all maze
    for (var row = 0; row < maze.Length; row++) {
      for (var k = 1; k <= 2; k++) {
        for (var column = 0; column < maze[row].Length; column++) {
          if (ShouldPrintVertical(k)) {
            if (ShouldPrintWall(row, column))
              board += "|   ";
            else {
              board += ";   ";
            }
          } else {
            if (ShouldPrintFloor(row, column))
              board += "+---";
            else {
              board += "+-_-";
            }
          }
        }
        board = AddEndLines(board, k);
      }
    }
    // Print maze
    Console.WriteLine(board);
  }

  private static bool ShouldPrintFloor(int row, int column) {
    return maze[row][column].Bottom == false;
  }

  private static bool ShouldPrintWall(int row, int column) {
    return maze[row][column].Left == false;
  }

  private static string AddEndLines(string board, int k) {
    if (ShouldPrintVertical(k)) {
      board += "|\n";
    } else {
      board += "+\n";
    }

    return board;
  }

  private static bool ShouldPrintVertical(int k) {
    return k % 2 == 1;
  }

  private static string AddOneLine(string board) {
    for (var j = 0; j < maze[0].Length; j++) {
      board += "+---";
    }

    board += "+\n";
    return board;
  }

  private static void GenerateEmptyMaze(int width, int height) {
    maze = new Cell[height][];
    for (var row = 0; row < height; row++) {
      maze[row] = new Cell[width];
      for (var column = 0; column < maze[row].Length; column++) {
        maze[row][column] = new Cell();
      }
    }
  }

  private static int GetWidthFromPlayer() {
    int width;
    Console.Write("\bEnter width: ");
    while (!int.TryParse(Console.ReadLine(), out width) || width <= 0 || width > 100) {
      Console.WriteLine("Wrong value");
      Console.Write("\bEnter width: ");
    }

    return width;
  }

  private static int GetHeightFromPlayer() {
    int height;
    Console.Write("\bEnter height: ");
    while (!int.TryParse(Console.ReadLine(), out height) || height <= 0 || height > 100) {
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
  public bool visited;
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

class Sudoku {
  private static readonly Field[][] Board = new Field[9][];
  private const int MinGeneratedFiels = 25;
  private const int MaxGeneratedFiels = 80;
  private static int step;
  private static readonly Stopwatch _watch = new Stopwatch();

  private class Field {
    public int value;
    public bool canChange = true;
  }

  private struct Cursor {
    public static int x;
    public static int y;
  }

  static void Main(string[] args) {
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Title = "Sudoku";
    Console.WriteLine("<---------------Sudoku---------------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Console.WriteLine("Press any key to start.\n");
    Console.ReadKey();
    Start();
  }

  private static void Start() {
    step = 0;
    Console.WriteLine("\bGenerating sudoku...");
    GenerateBoard();
    while (true) {
      PrintBoard(true);
      HandleInput();
    }
  }

  private static void HandleInput() {
    var key = Console.ReadKey();

    if (char.IsNumber(key.KeyChar) && Board[Cursor.x][Cursor.y].canChange) {
      Board[Cursor.x][Cursor.y].value = (int) char.GetNumericValue(key.KeyChar);
      if (CheckForWin()) Win();
    }

    switch (key.Key) {
      case ConsoleKey.Q:
        Environment.Exit(0);
        break;
      case ConsoleKey.Delete:
        if (Board[Cursor.x][Cursor.y].canChange) Board[Cursor.x][Cursor.y].value = 0;
        break;
      case ConsoleKey.S:
        AutoSolve();
        break;
      case ConsoleKey.UpArrow:
        Cursor.x--;
        if (Cursor.x < 0) Cursor.x = 0;
        break;
      case ConsoleKey.DownArrow:
        Cursor.x++;
        if (Cursor.x > 8) Cursor.x = 8;
        break;
      case ConsoleKey.LeftArrow:
        Cursor.y--;
        if (Cursor.y < 0) Cursor.y = 0;
        break;
      case ConsoleKey.RightArrow:
        Cursor.y++;
        if (Cursor.y > 8) Cursor.y = 8;
        break;
    }
  }

  private static void AutoSolve() {
    Cursor.x = 0;
    Cursor.y = 0;
    _watch.Reset();
    _watch.Start();
    Console.WriteLine("\bSolving...");
    if (!Solve()) {
      Console.WriteLine("Solution not found.");
      QuitOrRestart();
    }
    _watch.Stop();
    PrintBoard(false);
    Console.WriteLine("\nSolved. Steps: " + step);
    Console.WriteLine("Time (ms): " + _watch.ElapsedMilliseconds);
    QuitOrRestart();
  }

  private static bool Solve() {
    if (_watch.ElapsedMilliseconds > 1000) {
      return false;
    }
    if (CheckForWin()) return true;
    var emptySpotsCords = GetEmptySpotsCord(Board);
    Cursor.x = emptySpotsCords.Item1;
    Cursor.y = emptySpotsCords.Item2;
    if (Equals(new Tuple<int, int>(9, 9), emptySpotsCords)) {
      return true;
    }
    for (int num = 1; num <= 9; num++) {
      if (IsNumberCorrect(emptySpotsCords.Item1, emptySpotsCords.Item2, num)) {
        Board[emptySpotsCords.Item1][emptySpotsCords.Item2].value = num;
        step++;
        if (Solve()) {
          return true;
        }
        Board[emptySpotsCords.Item1][emptySpotsCords.Item2].value = 0;
      }
    }

    return false;
  }

  private static Tuple<int, int> GetEmptySpotsCord(Field[][] board) {
    for (var i = 0; i < board.Length; i++) {
      for (var j = 0; j < board.Length; j++) {
        if (board[i][j].canChange && board[i][j].value == 0) {
          return new Tuple<int, int>(i, j);
        }
      }
    }
    return new Tuple<int, int>(9, 9);
  }

  private static bool IsNumberCorrect(int x, int y, int num) {
    // Check column
    foreach (var field in Board[x]) {
      if (field.value == num) {
        return false;
      }
    }

    foreach (var row in Board) {
      if (row[y].value == num) {
        return false;
      }
    }

    for (var i = 0; i < 3; i++) {
      for (var j = 0; j < 3; j++) {
        if (Board[i + (3 * (x / 3))][j + (3 * (y / 3))].value == num) {
          return false;
        }
      }
    }

    return true;
  }

  private static bool CheckForWin() {
    return Board.SelectMany(line => line).All(field => field.value != 0) && IsBoardCorrect();
  }

  private static void Win() {
    PrintBoard(false);
    Console.WriteLine(@"
     __     __          __          __           _ _ _ 
     \ \   / /          \ \        / /          | | | |
      \ \_/ /__  _   _   \ \  /\  / /__  _ __   | | | |
       \   / _ \| | | |   \ \/  \/ / _ \| '_ \  | | | |
        | | (_) | |_| |    \  /\  / (_) | | | | |_|_|_|
        |_|\___/ \__,_|     \/  \/ \___/|_| |_| (_|_|_)
      ");
    QuitOrRestart();
  }

  private static void QuitOrRestart() {
    Console.WriteLine("Press Enter to play again or Q to quit.");
    while (true) {
      var key = Console.ReadKey().Key;
      Console.Write("\b");
      switch (key) {
        case ConsoleKey.Q:
          Environment.Exit(0);
          break;
        case ConsoleKey.Enter:
          Start();
          break;
      }
    }
  }

  private static void PrintBoard(bool showCursor) {
    Console.Clear();

    PrintTitle();

    for (var i = 0; i < Board.Length; i++) {
      if (i % 3 == 0) {
        Console.WriteLine("+---+---+---+");
      }

      for (var j = 0; j < Board[0].Length; j++) {
        if (j % 3 == 0) {
          Console.Write('|');
        }

        if (i == Cursor.x && j == Cursor.y && showCursor) {
          Console.ForegroundColor = ConsoleColor.White;
          Console.Write('█');
        } else if (Board[i][j].value > 0) {
          Console.ForegroundColor = !Board[i][j].canChange ? ConsoleColor.Red : ConsoleColor.White;
          Console.Write(Board[i][j].value);
        } else {
          Console.Write(' ');
        }

        Console.ForegroundColor = ConsoleColor.Green;
      }

      Console.Write("|\n");
    }

    Console.WriteLine("+---+---+---+");

    Console.WriteLine(
      "\nHELP Arrows - controls, 1-9 - Place number, 0 or Del - to remove number, S - autosolve, Q - quit");
  }

  private static void PrintTitle() {
    Console.WriteLine("  ╔══════╗");
    Console.WriteLine("  ║SUDOKU║");
    Console.WriteLine("  ╚══════╝");
  }

  private static void GenerateBoard() {
    step = 0;
    for (var i = 0; i < Board.Length; i++) {
      Board[i] = new Field[9];
      for (var j = 0; j < Board.Length; j++) {
        Board[i][j] = new Field();
      }
    }

    var random = new Random();
    var randomRange = random.Next(MinGeneratedFiels, MaxGeneratedFiels);
    for (var i = 0; i < randomRange; i++) {
      var x = random.Next(0, 9);
      var y = random.Next(0, 9);
      Board[x][y].value = random.Next(1, 9);
      Board[x][y].canChange = false;
      if (!IsBoardCorrect()) {
        Board[x][y].value = 0;
        i--;
        Board[x][y].canChange = true;
      }
    }

    _watch.Restart();
    if (Solve()) {
      foreach (var row in Board) {
        foreach (var field in row) {
          if (field.canChange) {
            field.value = 0;
          }
        }
      }
      _watch.Stop();
    } else {
      _watch.Stop();
      GenerateBoard();
    }
  }

  private static bool IsBoardCorrect() {
    List<int> numberList;
    // Check all rows
    foreach (var row in Board) {
      numberList = GenerateNumbeList();
      foreach (var field in row) {
        if (numberList.Contains(field.value)) numberList.Remove(field.value);
        else if (field.value > 0) {
          return false;
        }
      }
    }

    //Check all columns
    for (var i = 0; i < Board.Length; i++) {
      numberList = GenerateNumbeList();
      for (var j = 0; j < Board.Length; j++) {
        if (numberList.Contains(Board[j][i].value)) numberList.Remove(Board[j][i].value);
        else if (Board[j][i].value > 0) {
          return false;
        }
      }
    }

    //Check all squares
    for (var squareX = 0; squareX < 3; squareX++) {
      for (var squareY = 0; squareY < 3; squareY++) {
        numberList = GenerateNumbeList();
        for (var rowInSquare = 0; rowInSquare < 3; rowInSquare++) {
          for (var columnInSquare = 0; columnInSquare < 3; columnInSquare++) {
            if (numberList.Contains(Board[squareX * 3 + rowInSquare][squareY * 3 + columnInSquare].value))
              numberList.Remove(Board[squareX * 3 + rowInSquare][squareY * 3 + columnInSquare].value);
            else if (Board[squareX * 3 + rowInSquare][squareY * 3 + columnInSquare].value > 0) return false;
          }
        }
      }
    }

    return true;
  }

  private static List<int> GenerateNumbeList() {
    var numberList = new List<int>();
    for (var i = 1; i < 10; i++) {
      numberList.Add(i);
    }

    return numberList;
  }
}

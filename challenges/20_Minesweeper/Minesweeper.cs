using System;

internal class Minesweeper {
  private static readonly Field[][] board = new Field[10][];
  private static readonly int boardSize = 10;
  private static readonly int minesNumber = 10;
  private static int cursorX;
  private static int cursorY;

  private static readonly char cursor = '×';
  private static readonly char mine = '¤';
  private static readonly char flag = '¶';
  private static readonly char notRevealed = '█';
  private static readonly char revealed = ' ';

  private static void Main(string[] args) {
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Title = "Minesweeper";
    Console.WriteLine("<------------Minesweeper-------------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");
    Console.WriteLine("Press any key to start");
    Console.ReadKey();

    Start();
  }

  private static void Start() {
    cursorX = 0;
    cursorY = 0;
    InitEmptyBoard(boardSize);
    AddMines();
    while (true) {
      Console.Clear();
      Console.WriteLine("<| Minesweeper |>");
      PrintBoard();
      HandleInput();
      CheckWin();
    }
  }

  private static void CheckWin() {
    int filedsLeft = boardSize * boardSize;
    for (int i = 0; i < board.Length; i++) {
      for (int j = 0; j < board[i].Length; j++) {
        if (board[i][j].isReviled) {
          filedsLeft--;
        }
      }
    }
    Console.WriteLine(filedsLeft);
    if (filedsLeft <= minesNumber) {
      Console.WriteLine("You won! Press any key to start again or Q to quit.");
      var key = Console.ReadKey().Key;
      if (key == ConsoleKey.Q) Environment.Exit(0);
      Start();
    }
  }

  private static void AddMines() {
    var random = new Random();
    for (var i = 0; i < minesNumber; i++) {
      var x = random.Next(0, 10);
      var y = random.Next(0, 10);
      if (!board[x][y].hasMine) board[x][y].hasMine = true;
      else i--;
    }

    AddInfoAboutMinesAround();
  }

  private static void AddInfoAboutMinesAround() {
    for (var i = 0; i < board.Length; i++)
    for (var j = 0; j < board[i].Length; j++) {
      var bombAround = 0;
      if (i - 1 >= 0 && j - 1 >= 0 && board[i - 1][j - 1].hasMine) bombAround++;
      if (i - 1 >= 0 && board[i - 1][j].hasMine) bombAround++;
      if (i - 1 >= 0 && j + 1 <= board[i].Length - 1 && board[i - 1][j + 1].hasMine) bombAround++;
      if (j - 1 >= 0 && board[i][j - 1].hasMine) bombAround++;
      if (j + 1 <= board[i].Length - 1 && board[i][j + 1].hasMine) bombAround++;
      if (i + 1 <= board.Length - 1 && j - 1 >= 0 && board[i + 1][j - 1].hasMine) bombAround++;
      if (i + 1 <= board.Length - 1 && board[i + 1][j].hasMine) bombAround++;
      if (i + 1 <= board.Length - 1 && j + 1 <= board[i].Length - 1 && board[i + 1][j + 1].hasMine) bombAround++;
      board[i][j].mineAroundNumber = bombAround;
    }
  }

  private static void HandleInput() {
    var key = Console.ReadKey().Key;
    switch (key) {
      case ConsoleKey.DownArrow:
        board[cursorX][cursorY].hasCursor = false;
        cursorX++;
        if (cursorX > boardSize - 1) cursorX = boardSize - 1;
        break;
      case ConsoleKey.UpArrow:
        board[cursorX][cursorY].hasCursor = false;
        cursorX--;
        if (cursorX < 0) cursorX = 0;
        break;
      case ConsoleKey.LeftArrow:
        board[cursorX][cursorY].hasCursor = false;
        cursorY--;
        if (cursorY < 0) cursorY = 0;
        break;
      case ConsoleKey.RightArrow:
        board[cursorX][cursorY].hasCursor = false;
        cursorY++;
        if (cursorY > boardSize - 1) cursorY = boardSize - 1;
        break;
      case ConsoleKey.F:
        board[cursorX][cursorY].isFlagged = !board[cursorX][cursorY].isFlagged;
        break;
      case ConsoleKey.Q:
        Environment.Exit(0);
        break;
      case ConsoleKey.Enter:
        if (board[cursorX][cursorY].isFlagged) {
        } else if (board[cursorX][cursorY].hasMine) {
          Console.WriteLine("You lost, press any key to play again or Q to quit");
          var consoleKey = Console.ReadKey().Key;
          if (consoleKey == ConsoleKey.Q) Environment.Exit(0);
          Start();
        } else {
          board[cursorX][cursorY].isReviled = true;
          if (board[cursorX][cursorY].mineAroundNumber == 0) CheckAroundEmptyField(cursorX, cursorY);
        }

        break;
    }

    board[cursorX][cursorY].hasCursor = true;
  }

  private static void CheckAroundEmptyField(int x, int y) {
    if (x - 1 >= 0 && y - 1 >= 0 && !board[x - 1][y - 1].isReviled) {
      board[x - 1][y - 1].isReviled = true;
      if (board[x - 1][y - 1].mineAroundNumber == 0) CheckAroundEmptyField(x - 1, y - 1);
    }

    if (x - 1 >= 0 && !board[x - 1][y].isReviled) {
      board[x - 1][y].isReviled = true;
      if (board[x - 1][y].mineAroundNumber == 0) CheckAroundEmptyField(x - 1, y);
    }

    if (x - 1 >= 0 && y + 1 <= board[x].Length - 1 && !board[x - 1][y + 1].isReviled) {
      board[x - 1][y + 1].isReviled = true;
      if (board[x - 1][y + 1].mineAroundNumber == 0) CheckAroundEmptyField(x - 1, y + 1);
    }

    if (y - 1 >= 0 && !board[x][y - 1].isReviled) {
      board[x][y - 1].isReviled = true;
      if (board[x][y - 1].mineAroundNumber == 0) CheckAroundEmptyField(x, y - 1);
    }

    if (y + 1 <= board[x].Length - 1 && !board[x][y + 1].isReviled) {
      board[x][y + 1].isReviled = true;
      if (board[x][y + 1].mineAroundNumber == 0) CheckAroundEmptyField(x, y + 1);
    }

    if (x + 1 <= board.Length - 1 && y - 1 >= 0 && !board[x + 1][y - 1].isReviled) {
      board[x + 1][y - 1].isReviled = true;
      if (board[x + 1][y - 1].mineAroundNumber == 0) CheckAroundEmptyField(x + 1, y - 1);
    }

    if (x + 1 <= board.Length - 1 && !board[x + 1][y].isReviled) {
      board[x + 1][y].isReviled = true;
      if (board[x + 1][y].mineAroundNumber == 0) CheckAroundEmptyField(x + 1, y);
    }

    if (x + 1 <= board.Length - 1 && y + 1 <= board[x].Length - 1 && !board[x + 1][y + 1].isReviled) {
      board[x + 1][y + 1].isReviled = true;
      if (board[x + 1][y + 1].mineAroundNumber == 0) CheckAroundEmptyField(x + 1, y + 1);
    }
  }

  private static void PrintBoard() {
    PrintTopBorder();

    for (var i = 0; i < board.Length; i++) {
      Console.Write('║');
      for (var j = 0; j < board.Length; j++)
        if (board[i][j].hasCursor) {
          Console.Write(cursor);
        } else if (board[i][j].isFlagged) {
          Console.Write(flag);
        } else if (board[i][j].isReviled) {
          if (board[i][j].mineAroundNumber > 0) Console.Write(board[i][j].mineAroundNumber);
          else Console.Write(revealed);
        } else if (!board[i][j].isReviled) {
          Console.Write(notRevealed);
        } else {
          Console.Write('?');
        }

      Console.Write("║ " + i + "\n");
    }

    PrintLowBorder();

    PrintXCords();

    Console.Write("\n\n");
    Console.WriteLine("Arrows - Navigation, F - flag, Enter - check, Q - quit");
  }

  private static void PrintXCords() {
    for (var i = 0; i < 10; i++) Console.Write(i);
  }

  private static void PrintLowBorder() {
    Console.Write('╚');
    for (var i = 0; i < 10; i++) Console.Write('═');

    Console.Write("╝\n ");
  }

  private static void PrintTopBorder() {
    Console.Write('╔');
    for (var i = 0; i < boardSize; i++) Console.Write('═');

    Console.Write("╗\n");
  }

  private static void InitEmptyBoard(int size = 10) {
    for (var i = 0; i < boardSize; i++) {
      board[i] = new Field[10];
      for (var j = 0; j < boardSize; j++)
        board[i][j] = new Field {
          cordX = j,
          cordY = i
        };
    }

    board[0][0].hasCursor = true;
  }
}

internal class Field {
  public int cordX;
  public int cordY;
  public bool hasCursor;
  public bool hasMine;
  public bool isFlagged;
  public bool isReviled;
  public int mineAroundNumber;
}
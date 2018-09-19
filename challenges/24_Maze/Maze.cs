using System;

class Maze {

  private static Cell[][] cells;

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
    Console.WriteLine("2 - Generate maze with animaions");
    Console.WriteLine("3. Solve");
    Console.WriteLine("4. Solve with animations");
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



    Console.ReadKey();
  }

  private static void GenerateEmptyMaze(int width, int height) {
    cells = new Cell[height][];
    for (var row = 0; row < height; row++) {
      cells[row] = new Cell[width];
    }
  }

  private static int GetWidthFromPlayer() {
    int width;
    Console.Write("\bEnter width: ");
    while (!int.TryParse(Console.ReadLine(), out width) || width <= 0) {
      Console.WriteLine("Wrong value");
      Console.Write("\bEnter width: ");
    }

    return width;
  }

  private static int GetHeightFromPlayer() {
    int height;
    Console.Write("\bEnter hight: ");
    while (!int.TryParse(Console.ReadLine(), out height) || height <= 0) {
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
  private bool visited;
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class SimpleFileExplorer {

  private static string _currentDirectory = Directory.GetCurrentDirectory();
  private const string Version = "0.1.0";
  private static int _cursorPosition;
  private static string _selectedDirectory;
  private static int _currentDirectoryLength;

  private static void Main(string[] args) {
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Title = "Simple file explorer " + Version;
    Console.WriteLine("<--------Simple File Explorer--------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");

    Start();
  }

  private static void Start() {
    Console.Clear();
    while (true) {
      var files = GetFileList(_currentDirectory);
      _currentDirectoryLength = files.Count;
      RefreshScreen(files);
      ManageInput();
    }
  }

  private static void ManageInput() {
    var key = Console.ReadKey().Key;
    switch (key) {
      case ConsoleKey.Q:
        Environment.Exit(0);
        break;
      case ConsoleKey.F1:
        DisplayInstruction();
        break;
      case ConsoleKey.UpArrow:
        ChangeCurrentPositionBy(-1);
        break;
      case ConsoleKey.DownArrow:
        ChangeCurrentPositionBy(1);
        break;
      case ConsoleKey.LeftArrow:
        _currentDirectory = Directory.GetParent(_currentDirectory).FullName;
        _cursorPosition = 0;
        break;
      case ConsoleKey.RightArrow:
        if (IsFileDirectory(_selectedDirectory)) {
          _currentDirectory = _selectedDirectory;
          _cursorPosition = 0;
        }
        break;
    }
  }

  private static void ChangeCurrentPositionBy(int change) {
    _cursorPosition += change;
    if (_cursorPosition < 0) _cursorPosition = 0;
    else if (_cursorPosition > _currentDirectoryLength - 1) _cursorPosition = _currentDirectoryLength - 1;
  }

  private static void DisplayInstruction() {
    Console.Clear();
    Console.WriteLine("Simple File Explorer v" + Version);
    Console.WriteLine("Created by Morasiu in 2018");
    Console.WriteLine("--------------------------\n");
    Console.WriteLine("1. Use arrows to naviagte");
    Console.WriteLine("\nPress any key to bo back");
    Console.ReadKey();
  }

  private static void RefreshScreen(List<string> files) {
    Console.Clear();
    WriteAllFiles(files);
    MakeEmptyLines(files);
    WriteInstructionOnBottom();
  }

  private static void WriteInstructionOnBottom() {
    Console.Write("F1 - Help; Arrows - control; q - Quit");
  }

  private static void WriteAllFiles(IReadOnlyList<string> files) {
    Console.WriteLine("..");
    for (var i = 0;  i < files.Count(); i++) {
      WriteFileInfo(files[i], i == _cursorPosition);
    }
  }

  private static void WriteFileInfo(string file, bool addCursor) {
    var fileName = Path.GetFileName(file);
    if (addCursor) {
      fileName += " <-";
      _selectedDirectory = file;
    }
    var line = fileName;
    var fileSize = "DIR";
    if (!IsFileDirectory(file)) fileSize = GetFileSize(new FileInfo(file).Length);
    line = FillLineWithSpaces(fileName, line, fileSize);
    line += fileSize;
    Console.WriteLine(line);
  }

  private static bool IsFileDirectory(string file) {
    return (File.GetAttributes(file) & FileAttributes.Directory) != 0;
  }

  private static string FillLineWithSpaces(string fileName, string line, string fileSize) {
    for (var i = 0; i < Console.WindowWidth - fileName.Length - fileSize.Length - 1; i++) {
      line += " ";
    }

    return line;
  }

  private static List<string> GetFileList(string directory) {
    return Directory.GetFileSystemEntries(directory).ToList();
  }

  private static string GetFileSize(long size) {
    var convertedSize = "Error";
    if (size < 1000) convertedSize = size + " B";
    else if (size < 1000 * 1000) convertedSize = (size / 1000) + " KB";
    else if (size < 1000 * 1000 * 1000) convertedSize = size / (1000 * 1000) + " MB";
    else if (size >= 1000 * 1000 * 1000) convertedSize = size / (1000 * 1000 * 1000) + " GB";

    return convertedSize;
  }

  private static void MakeEmptyLines(ICollection files) {
    for (var i = 0; i < Console.WindowHeight - files.Count - 2; i++) {
      Console.WriteLine();
    }
  }
}

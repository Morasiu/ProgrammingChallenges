using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

class SimpleFileExplorer {
  static void Main(string[] args) {
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("<--------Simple File Explorer--------->");
    Console.WriteLine("Created by Morasiu (morasiu2@gmail.com)");

    Start();
  }

  static void Start() {
    Console.Clear();
    Console.WriteLine("..");
    var files = Directory.GetFiles(Directory.GetCurrentDirectory()).ToList();
    //var files = Directory.GetFiles(@"c:\Users\hmorawski\Downloads").ToList();
    foreach (var file in files) {
      var fileName = Path.GetFileName(file);
      var line = fileName;
      var fileSize = GetFileSize(new FileInfo(file).Length);
      for (var i = 0; i < Console.WindowWidth - fileName.Length - fileSize.Length - 1; i++) {
        line += " ";
      }

      line += fileSize;
      Console.WriteLine(line);
    }

    MakeEmptyLines(files);

    Console.Write("   q - Quit");
    var key = Console.ReadKey().Key;
    if (key == ConsoleKey.Q) Environment.Exit(0);
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

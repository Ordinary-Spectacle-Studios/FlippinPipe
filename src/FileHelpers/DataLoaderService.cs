using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using FlippinPipe.Components;

namespace FlippinPipe.FileHelpers
{
    public static class DataLoaderService
    {
        const string puzzlesFile = "Data/puzzles.json";
        public static List<PuzzleData> LoadPuzzles()
        {
            if (File.Exists(puzzlesFile))
            {
                try
                {
                    var json = File.ReadAllText(puzzlesFile);
                    var loadedData = JsonSerializer.Deserialize<List<PuzzleData>>(json);
                    return loadedData;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                }
            }
            return null;
        }
    }


    public class PuzzleData
    {
        public int solveCount { get; set; }
        public List<string> puzzle { get; set; }
    }

}
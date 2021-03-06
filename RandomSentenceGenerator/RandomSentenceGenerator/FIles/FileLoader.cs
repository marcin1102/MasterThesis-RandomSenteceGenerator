﻿using System;
using System.IO;

namespace RandomSentenceGenerator.Files
{
    public class FileLoader
    {
        /// <summary>
        /// Loads and returns file content.
        /// </summary>
        /// <param name="fileName">Name or dir of file to load</param>
        /// <returns>True if loading succesfulla</returns>
        public static bool Load(string fileName, out string content)
        {
            try
            {
                var loadedContent = File.ReadAllLines(fileName);
                content = string.Join('\n', loadedContent);
                return true;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Missing {fileName} file");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Missing file name or dir");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Incorrect file name or dir");
            }

            content = null;
            return false;
        }
    }
}

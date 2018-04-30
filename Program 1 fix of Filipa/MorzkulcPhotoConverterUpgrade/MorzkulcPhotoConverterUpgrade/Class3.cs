using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MorzkulcPhotoConverterUpgrade
{
    public class OtherMethodsContainer
    {
        public string format { get; set; }
        public string destinationFolder { get; set; }
        public string destinationFile { get; set; }
        public string destinationFullPath { get; set; }
        public string input { get; set; }
        public string sourceOfFolder { get; set; }
        public string sourceOfImage { get; set; }
        public string sourceOfFolderAndImage { get; set; }

        public string sourceOfImageM { get; set; }

        public string path { get; set; }
        public string searchPattern { get; set; }
        public SearchOption searchOption { get; set; }

        
        public static string CheckForXmlConfig()
        {
            string checkconfigFile;

            if (File.Exists("XML file\\PhotoConverterCommands.xml"))
            {
                Console.WriteLine("check for PhotoConverterCommands.xml = it exists!");
                Console.WriteLine("would you like to load the values from configFile? 'Yes/No'");
                checkconfigFile = Console.ReadLine().ToLower();
            }
            else
            {
                Console.WriteLine("xml file not found, proceed without it!");
                checkconfigFile = "";
            }

            return checkconfigFile;
        }

        
        public static Tuple<string, string, string> DestinationMethod (string format)
            {
            string destinationFolder;
            string destinationFile;
            string destinationFullPath;

            Console.WriteLine("\nProvide destination folder eg. 'e:\\programowanie' (If file does not exist, it will be automaticaly created)");
            destinationFolder = Console.ReadLine();

            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            Console.WriteLine("\nProvide name for the converted image e.g 'plakatNowy'");
            destinationFile = Console.ReadLine();
            destinationFullPath = destinationFolder + "\\" + destinationFile + "." + format;
            Console.WriteLine(destinationFullPath);
            return Tuple.Create(destinationFolder, destinationFile, destinationFullPath);
        }

        /// <summary>
        /// Retrieve the strings for the image source.
        /// </summary>
        /// <returns>Returns a Tuple contaning the User Input, the Source Path of the Folder and the Source Path of the Image, combined with the Folder Path.</returns>
        public static Tuple<string, string, string> SourceMethod () 
        {
            string input;
            string sourceOfFolder = "";
            string sourceOfImage = "";
            string sourceOfFolderAndImage = "";

            while (true)
            {
                Console.WriteLine("Would you like to convert single or multiple images from the folder? (single/multiple)");
                input = Console.ReadLine().ToLower();
                if (input.Equals("single"))
                {
                    while (true)
                    { 
                        try
                        {
                            Console.WriteLine("Provide source folder file e.g 'e:\\convert"); 
                            sourceOfFolder = Console.ReadLine();
                            //sourceOfFolder = "e:\\programowanie"; // temporary testValue for a folder
                            Console.WriteLine("Provide source image e.g. 'plakat.png'"); 
                            sourceOfImage = Console.ReadLine();
                            //sourceOfImage = "plakat.png"; // temporary testValue for an image
                            sourceOfFolderAndImage = sourceOfFolder + "\\" + sourceOfImage;
                            Console.WriteLine("source of Folder and Image: " + sourceOfFolderAndImage);
                            break;
                        }
                        catch (FileNotFoundException)
                        {
                            Console.WriteLine("File not found! Provide a proper source file e.g 'e:\\convert\\plakat.png'");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("something went wrong, provide a proper source file e.g 'e:\\convert\\plakat.png'");
                        }
                    }
                    break;
                }

                if (input.Equals("multiple") || input.Equals("multi"))
                {
                    while (true)
                    {
                        try
                        {
                            Console.WriteLine(@"Please, provide source folder, e.g 'e:\programy'");
                            sourceOfFolder = Console.ReadLine();
                            //sourceOfFolder = "e:\\programowanie"; // temporary testValue for a folder

                            if (Directory.Exists(sourceOfFolder))
                            {
                                Console.WriteLine("directory exists!");
                            }
                            break;
                        }

                        catch (Exception)
                        {
                            Console.WriteLine("something went wrong, provide a proper source folder e.g 'e:\\programy'");
                        }
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("wrong input, please write again");
                    break;
                }
            }
            return Tuple.Create(input, sourceOfFolder, sourceOfFolderAndImage);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /*
         * I again have a suggestion.
         * 
         * I would break SourceMethod up into two methods, one really handling case "Single" and one handling
         * case "Multiple". That way you could also save yourself of having to return an empty string in your
         * Tuple when you are doing multiple images at once.
         */
        ////////////////////////////////////////////////////////////////////////////////////////////////////


        public static int WidthMethod ()
           
        {
            string widthCheck;
            int widthM;

            while (true)
            {
                try
                {
                    Console.WriteLine("\nwhat is the new width?");
                    widthCheck = Console.ReadLine();
                    if (widthCheck.Equals(""))
                        widthCheck = "0";
                    widthM = Convert.ToInt32(widthCheck);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("You provided a wrong input, please, provide an integer for width");
                }
                catch (Exception)
                {
                    Console.WriteLine("Something else went wrong.");
                }
            }
            return widthM;
        }

        public static int HeightMethod ()
        {
            string heightCheck;
            int heightM;

            Console.WriteLine("\nWhat is the new height?");
            while (true)
            {
                try
                {
                    heightCheck = Console.ReadLine();
                    if (heightCheck.Equals(""))
                        heightCheck = "0";
                    heightM = Convert.ToInt32(heightCheck);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("You provided a wrong input, please, provide an integer for height");
                }
                catch (Exception)
                {
                    Console.WriteLine("Something else went wrong.");
                }
            }

            return heightM;
        }

        public static string FormatMethodCheck()
        {
            string[] formatArray = new string[] { "jpg", "png", "gif", "ico" }; // array containing formats to be searched for when looking for images in folders.
            string formatInput;

            while (true)
            {
                Console.WriteLine("\nProvide desired format of the photo: 'jpg'/ 'png' / 'gif'/ 'ico'?");
                formatInput = Console.ReadLine().ToLower();
                if (formatArray.Contains(formatInput))
                {
                    Console.WriteLine("Correct format: " + formatInput);
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect format! " + formatInput);
                }
            }
            return formatInput;
        }

        public static string[] FileExtractionMethod(string sourceOfImageM)
        {
            string[] formatOfFileInRootFolder = new string[] { "*.jpg|*.png|*.gif|*.tiff" };
            string joinedformatOfFileInRootFolder = string.Join("|", formatOfFileInRootFolder);

            string[] files = GetFiles(sourceOfImageM, joinedformatOfFileInRootFolder, SearchOption.AllDirectories);


            Console.WriteLine("files in the given directory: \n");
            foreach (string file in files)
            {
                Console.WriteLine(file);
            }
            return files;
        }

        /// <summary>
        /// GetFiles method to get more than one extension
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchPattern"></param>
        /// <param name="searchOption"></param>
        /// <returns></returns>
        public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
        {

            string[] searchPatterns = searchPattern.Split('|');
            List<string> files = new List<string>();
            foreach (string sp in searchPatterns)
                files.AddRange(Directory.GetFiles(path, sp, searchOption)); 
            files.Sort();
            return files.ToArray();
        }

    }
}

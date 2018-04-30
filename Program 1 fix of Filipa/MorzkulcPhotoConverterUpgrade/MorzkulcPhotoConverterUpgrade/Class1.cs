using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Xml.Linq;

namespace MorzkulcPhotoConverterUpgrade
{

    public class ImageMetaDataContainer
    {
        /// <summary>
        /// 
        /// </summary>
        public string input { get; set; }

        /// <summary>
        /// The path to the Folder.
        /// </summary>
        public string sourceOfFolder { get; set; }

        /// <summary>
        /// The path to the image. Source variable of combined Folder and Image.
        /// </summary>
        public string sourceOfFolderAndImage { get; set; } 

        /// <summary>
        /// New Width of the Image. 
        /// </summary>
        public string widthCheck { get; set; } 

        /// <summary>
        /// New Height of the Image. 
        /// </summary>
        public string heightCheck { get; set; } 

        /// <summary>
        /// The Width of the Image.
        /// </summary>
        public int width { get; set; }

        /// <summary>
        /// The Height of the Image.
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// The target Folder.
        /// </summary>
        public string destinationFolder { get; set; }

        /// <summary>
        /// The target File.
        /// </summary>
        public string destinationFile { get; set; }

        /// <summary>
        /// The path to the target File
        /// </summary>
        public string destinationFullPath { get; set; } 

        /// <summary>
        /// The paths to all converted Files.
        /// </summary>
        public List<string>
            destinationFullPathList { get; set; } 

        /// <summary>
        /// The desired Format for the new Image or Images.
        /// </summary>
        public string format { get; set; } 

        /// <summary>
        /// Counter for found files in a Folder.
        /// </summary>
        public int newNumber { get; set; } 

        /// <summary>
        /// The Image in question.
        /// </summary>
        public Image photo { get; set; }

        /// <summary>
        /// The converted Image.
        /// </summary>
        public Bitmap convertedPhoto { get; set; }

        /// <summary>
        /// Constructor for reading an XML File.
        /// </summary>
        /// <param name="xmlFilePath">The path to the XML File.</param>
        public ImageMetaDataContainer(string xmlFilePath)
        {
            XElement xmlPCC = XElement.Load(xmlFilePath);

            input = xmlPCC.Element("input").Value;
            sourceOfFolder = xmlPCC.Element("sourceOfFolder").Value;
            sourceOfFolderAndImage = sourceOfFolder + "\\" + xmlPCC.Element("sourceOfImage").Value;

            //I would seriously do this differently...
            widthCheck = xmlPCC.Element("widthCheck").Value;
            if (widthCheck.Equals(""))
                widthCheck = "0";
            width = Convert.ToInt32(widthCheck);

           
            heightCheck = xmlPCC.Element("heightCheck").Value;
            if (heightCheck.Equals(""))
                heightCheck = "0";
            height = Convert.ToInt32(heightCheck);

            format = xmlPCC.Element("format").Value;
            destinationFolder = xmlPCC.Element("destinationFolder").Value;
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            destinationFile = xmlPCC.Element("destinationFile").Value;
            destinationFullPath = destinationFolder + "\\" + destinationFile + "." + format;

            //The initialization now happens here.
            List<string> destinationFullPathList = new List<string>();
        }

        /// <summary>
        /// Constructor for passing each variable "manually".
        /// </summary>
        /// <param name="sourceContainer">Container for Input, Source Path of Folder, and Source Path of Image (if applicable).</param>
        /// <param name="newWidth">Width the image shall have at the end.</param>
        /// <param name="newHeight">Height the image shall have at the end.</param>
        /// <param name="newFormat">Format the image shall have at the end.</param>
        /// <param name="destinationContainer">Container for Destination Folder Path, Destination File Name and Destination File Path (if applicable).</param>
        public ImageMetaDataContainer(Tuple<string, string, string> sourceContainer, int newWidth, int newHeight, string newFormat, Tuple<string, string, string> destinationContainer)
        {
            input = sourceContainer.Item1;
            sourceOfFolder = sourceContainer.Item2;
            sourceOfFolderAndImage = sourceContainer.Item3;
            width = newWidth;
            height = newHeight;
            format = newFormat;
            destinationFolder = destinationContainer.Item1;
            destinationFile = destinationContainer.Item2;
            destinationFullPath = destinationContainer.Item3;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // to handle files
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;



namespace MorzkulcPhotoConverter
{

    class Program
    {
        /// <summary> Reading input of the user</summary>
        /// <param name= "input"> source file path</param>
        /// <param name="photo">photo file</param>
        /// <param name="width"> new width</param>
        /// <param name="height">new height</param>
        /// <param name="formatInput"> new format of the new image</param>
        /// <param name="whereToPutThePhoto"> path for the new photo + new format</param>
        /// <param name="convertedPhoto">Fully converted photo</param>

        static void Main(string[] args)
        {

            string input, widthCheck;
            int width = 0;

            do
            {
                Console.WriteLine("Provide source file e.g 'e:\\convert\\plakat.png'");
                input = Console.ReadLine();
            }
            while (File.Exists(input) == false);


                //var input = "e:\\programowanie\\plakat.png"; // test value

            var photo = Image.FromFile(input, true);
            Console.WriteLine("Provide value to height or with to resize image with original aspect ratio for this size. Other value write '0' or press enter. Write both values to loose aspect ratio but receive the demanded values");


            Console.WriteLine("what is the new width?");
            try
            {
                widthCheck = Console.ReadLine();
                if (widthCheck.Equals(""))
                    widthCheck = "0";
                width = Convert.ToInt32(widthCheck);
            }
            catch (FormatException )
            {
                Console.WriteLine("you provided a wrong input, please, provide an intege for width");
                //continue; //wat?
            }

   
 

            Console.WriteLine("what is the new height?");
             var heightCheck = Console.ReadLine();
            if (heightCheck.Equals(""))
                heightCheck  =  "0";
            int height = Convert.ToInt32(heightCheck);


            Console.WriteLine(" Provide desired format of the photo: 'jpg'/ 'png' / 'gif'/ 'ico'?"); 
            string formatInput = Console.ReadLine();

            Console.WriteLine(" Provide destination folder e.g 'e:\\convert\\plakatNowy'");
            string whereToPutThePhoto = Console.ReadLine()+"."+ formatInput;
            // string whereToPutThePhoto = "e:\\programowanie\\plakatNowy." + formatInput; // test value


            var convertedPhoto = ResizeImage(photo, width, height);




            switch (formatInput)
            {
                case "jpg":
                    var formatNeeded = System.Drawing.Imaging.ImageFormat.Jpeg;
                    convertedPhoto.Save(whereToPutThePhoto, formatNeeded);
                    Console.WriteLine("check");
                    if (File.Exists(whereToPutThePhoto))
                        Console.WriteLine("Image converted to 'jpg' and is saved to: " + whereToPutThePhoto);
                    break;

                case "png":
                    formatNeeded = System.Drawing.Imaging.ImageFormat.Png;
                    convertedPhoto.Save(whereToPutThePhoto, formatNeeded);
                    if (File.Exists(whereToPutThePhoto))
                        Console.WriteLine("Image converted to 'png'and is saved to: " + whereToPutThePhoto);
                    break;
                case "gif":
                    formatNeeded = System.Drawing.Imaging.ImageFormat.Gif;
                    convertedPhoto.Save(whereToPutThePhoto, formatNeeded);
                    if (File.Exists(whereToPutThePhoto))
                        Console.WriteLine("Image converted! But it's gif and it will suck hard! It is saved to: " + whereToPutThePhoto);
                    break;
                case "ico":
                    formatNeeded = System.Drawing.Imaging.ImageFormat.Icon;
                    convertedPhoto.Save(whereToPutThePhoto, formatNeeded);
                    if (File.Exists(whereToPutThePhoto))
                        Console.WriteLine("Image converted to 'ico'! and is saved to: " + whereToPutThePhoto);
                    break;

                default:
                    formatNeeded = System.Drawing.Imaging.ImageFormat.Jpeg;
                    convertedPhoto.Save(whereToPutThePhoto, formatNeeded);
                    if (File.Exists(whereToPutThePhoto))
                        Console.WriteLine("Image converted to 'jpg'! and is saved to: " + whereToPutThePhoto);
                    break;
            }


        }



        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name= "destRect"> resized Rectangle- magic happens </param>
        /// <param name= "destImage"> resized Bitmap image- magic happens </param>
        /// <param name="graphics">quality modifications and save</param>
        /// <param name="maxWidth">resize width.</param>
        /// <param name="maxHeight">resize height.</param>
        /// <param name="ratio">aspect ratio e.g 16:9</param>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <param name="originalWidth"> original width of the image</param>
        /// <param name="originalHeight"> original height of the image</param>
        /// <param name="RGBcheck">Palette Colour conversion check</param>

        /// <returns>The resized image.</returns>
        /// 

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            /// <additional code for aspect ratio>
            /// 
            Bitmap destImage;
            int newWidth = 0;
            int newHeight = 0;
            int originalWidth = image.Width;
            int originalHeight = image.Height;



            if ((height == 0 )  && (width != 0)) 
            {
                newWidth = width;

                newHeight = originalHeight * newWidth / originalWidth;

            }
            else if ((height != 0) && (width == 0))
            {
                newHeight = height;
                newWidth = originalWidth * newHeight / originalHeight;

            }
            else if ((height != 0 ) && (width != 0))
            {
                newWidth = width;
                newHeight = height;
            }


            Console.WriteLine("originalWidth: " + originalWidth);
            Console.WriteLine("originalHeight: " + originalHeight);

            if (originalWidth / originalHeight == 16 / 9)
                Console.WriteLine("originalHeight aspect ratio is: 16:9");
            else if (image.Width / image.Height == 4 / 3)
                Console.WriteLine("originalHeight aspect ratio is: 16:9");
            else
                Console.WriteLine("unable to determine aspect ratio");

            Console.WriteLine("new Width: " + newWidth);
            Console.WriteLine("new Height: " + newHeight);



            Console.WriteLine("Convert to RGB? Good for internet(smaller size), bad for print (yes/no)");
            string RGBcheck = Console.ReadLine();
            if (RGBcheck.Equals("yes"))
                {
                 destImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb); 
                Console.WriteLine("converted to RGB");
                }
            

            var destRect = new Rectangle(0, 0, newWidth, newHeight); // Stores a set of four integers that represent the location and size of a rectangle
            destImage = new Bitmap(newWidth, newHeight); // resized Image with newWidth and newHeight

            


            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution); // Sets the resolution for this Bitmap. Mantains resolution of original image.

            using (var graphics = Graphics.FromImage(destImage)) //creates a new graphic from the given image ()
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy; //Gets a value that specifies how composited images are drawn to this Graphics. determines whether pixels from a source image overwrite or are combined with background pixels. SourceCopy: specifies that when a color is rendered, it overwrites the background color.
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality; // determines the rendering quality level of layered images.
                graphics.CompositingQuality = CompositingQuality.AssumeLinear;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; // determines how intermediate values between two endpoints are calculated. no reason to do different quality IMO
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; //antialiasing. Specifies whether lines, curves, and the edges of filled areas use smoothing 
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality; // Gets or set a value specifying how pixels are offset during rendering of this Graphics.


                using (var wrapMode = new ImageAttributes()) // good stuff for border stuff handling, look below. and also draws image out of a photo.
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY); // prevents ghosting around the image borders// wraps image so there are no transparent pixels beyond image borders.
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode); // Draws a resized image, normally in using var graphics /graphics.DrawImage(image, 0, 0, newWidth, newHeight);/-> image-photo; Rectangle class with newWidth and newHeight ; src X, src Y: 0, 0; original width, original height, specifies measure of the given data, wrap mode. 
                }

            }




            return destImage;

        }



    }


}



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
       
        static void Main(string[] args)
        {
            Console.WriteLine("program.cs");
            //Console.WriteLine("Provide source file");
            //var input = Console.ReadLine();
            var input = "e:\\programowanie\\plakat.png";
            var photo = Image.FromFile(input, true);
            Console.WriteLine("Provide value to height or with to resize image with original aspect ratio. Other value write '0'. Write both values to loose aspect ratio but receive the demanded values");

            Console.WriteLine("what is the new width?");
            int width = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("what is the new height?");
            int height = Convert.ToInt32(Console.ReadLine());

            //var convertedPhoto = ResizeImage(photo, height, width);

            Console.WriteLine(" Provide desired format of the photo: 'jpg'/ 'png' / 'gif'/ 'ico'?"); // gif sucks. delete it?
            string formatInput = Console.ReadLine();

        
            //Console.WriteLine(" Provide destination folder");
            //string whereToPutThePhoto = Console.ReadLine()+ formatInput;
            string whereToPutThePhoto = "e:\\programowanie\\plakatNowy."+ formatInput;
            //var photo = Image.FromFile(input, true);


            var convertedPhoto = ResizeImage(photo, width, height);


            switch (formatInput)
            {
                case "jpg":
                  var  formatNeeded = System.Drawing.Imaging.ImageFormat.Jpeg;
                    convertedPhoto.Save(whereToPutThePhoto, formatNeeded);
                    break;

                case "png":
                    formatNeeded = System.Drawing.Imaging.ImageFormat.Png;
                    convertedPhoto.Save(whereToPutThePhoto, formatNeeded);
                    break;
                case "gif":
                    formatNeeded = System.Drawing.Imaging.ImageFormat.Gif;
                    convertedPhoto.Save(whereToPutThePhoto, formatNeeded);
                    break;
                case "ico":
                    formatNeeded = System.Drawing.Imaging.ImageFormat.Icon;
                    convertedPhoto.Save(whereToPutThePhoto, formatNeeded);
                    break;

                default:
                    formatNeeded = System.Drawing.Imaging.ImageFormat.Jpeg;
                    convertedPhoto.Save(whereToPutThePhoto, formatNeeded);
                    break;
            }



        }



        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name= "destRect"> resized Rectangle </param>
        /// <param name= "destImage"> resized Bitmap</param>
        /// <param name="maxWidth">resize width.</param>
        /// <param name="maxHeight">resize height.</param>
        /// <param name="ratio">aspect ratio e.g 16:9</param>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        /// 

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            /// <additional code for aspect ratio>
            /// 

            int newWidth;
            int newHeight;
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            if ((height == 0) && (width != 0))
            {
                newWidth = width;
                newHeight = originalHeight * (newWidth / originalWidth);

            }
            else if ((height != 0) && (width == 0))
            {
                newHeight = height;
                newWidth = originalWidth * (newHeight / originalHeight);

            }
            else
            {
                newWidth = width;
                newHeight = height;
            }

            // Get the image's original width and height

            Console.WriteLine("originalWidth: " + originalWidth);
            Console.WriteLine("originalHeight: " + originalHeight);

            if (originalWidth / originalHeight == 16 / 9)
                Console.WriteLine("originalHeight aspect ratio is: 16:9");
            else if (image.Width / image.Height == 4 / 3)
                Console.WriteLine("originalHeight aspect ratio is: 16:9");
            else
                Console.WriteLine("unable to determine aspect ratio") ;


            /*
            float ratioX = (float)width / (float)originalWidth; // a need to zaokrąglić liczbę do całości.
            float ratioY = (float)height / (float)originalHeight;
            Console.WriteLine("ratioX: " + ratioX);
            Console.WriteLine("ratioY: " + ratioY);


            var ratio =  Math.Min(ratioX, ratioY); // returns the smaller of integers. this is the problem? depending on input it will take X or Y which is smaller. 
                                                   //var ratio = Math.Round(ratioToRound); //causes problem too. damn it.
                                                   //  use table of predefined ((float)x)/y ?


            Console.WriteLine("ratio: " + ratio);
            */

            /*
            // New width and height based on aspect ratio
            int newWidth = (int)(originalWidth * ratio );  
            int newHeight = (int)(originalHeight * ratio );
            Console.WriteLine("newWidth: " + newWidth);
            Console.WriteLine("newHeight: " + newHeight);

            /// </additional code for aspect ratio>
            */

            // Convert other formats (including CMYK) to RGB. - good for internet usage, lowering size of the file... may change the colours slightly.  do we need it?
            //Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

            var destRect = new Rectangle(0, 0, newWidth, newHeight); // Stores a set of four integers that represent the location and size of a rectangle
            var destImage = new Bitmap(newWidth, newHeight); // resized Image with newWidth and newHeight

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution); // Sets the resolution for this Bitmap. Mantains resolution of original image.

            using (var graphics = Graphics.FromImage(destImage)) //creates a new graphic from the given image ()
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy; //Gets a value that specifies how composited images are drawn to this Graphics. determines whether pixels from a source image overwrite or are combined with background pixels. SourceCopy: specifies that when a color is rendered, it overwrites the background color.
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality; // determines the rendering quality level of layered images.
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; // determines how intermediate values between two endpoints are calculated
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; //antialiasing. Specifies whether lines, curves, and the edges of filled areas use smoothing 
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality; // Gets or set a value specifying how pixels are offset during rendering of this Graphics.


                using (var wrapMode = new ImageAttributes() ) // good stuff for border stuff handling, look below. and also draws image out of a photo.
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY); // prevents ghosting around the image borders// wraps image so there are no transparent pixels beyond image borders.
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode); // Draws a resized image, normally in using var graphics /graphics.DrawImage(image, 0, 0, newWidth, newHeight);/-> image-photo; Rectangle class with newWidth and newHeight ; src X, src Y: 0, 0; original width, original height, specifies measure of the given data, wrap mode. 
                }

            }



            return destImage;

        }



    }


}



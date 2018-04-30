using System;
using System.IO; 

// test comment to see how thigs will roll.
//
//
//
// ... yeah...
//
//
// 
//test again as.... well, names.
//
//
//
//

namespace MorzkulcPhotoConverterUpgrade
{
    class Program
    {
        static void Main(string[] args)
        {
            int width = 0;
            int height = 0;
            string format = "";
            string loadingXML;

            Console.WriteLine("            Welcome to this amazing \n                Photo Converter! \n               Let's get started!\n");
            
            loadingXML = OtherMethodsContainer.CheckForXmlConfig();
  
            ImageMetaDataContainer myContainer;

            if (loadingXML.Equals("yes"))
            {
                string xmlFilePath = "XML file\\PhotoConverterCommands.xml";
                //Defining the Container.
                myContainer = new ImageMetaDataContainer(xmlFilePath);
                //input = myContainer.input; // not needed, as it is just repackaging stuff, while it can be done in the class1 in method. and here the whole method will be simply called.
               
            }
            else
            {
                Tuple<string, string, string> getValueOfSourceMethod = OtherMethodsContainer.SourceMethod(); // this thing will initialise SourceMethod once and will enable to get the values of it for following variables.

                Console.WriteLine("\nProvide value to height or with to resize image with original aspect ratio for this size. \nOther value write '0' or press enter. \nWrite both values to loose aspect ratio but receive the demanded values");
                width = OtherMethodsContainer.WidthMethod();
                height = OtherMethodsContainer.HeightMethod();
                format = OtherMethodsContainer.FormatMethodCheck();

                Tuple<string, string, string> getValueOfDestinationMethod = OtherMethodsContainer.DestinationMethod(format);

                //Defining the Container.
                myContainer = new ImageMetaDataContainer(getValueOfSourceMethod, width, height, format, getValueOfDestinationMethod);

            }

            string joinedformatOfFileInRootFolder = "*.jpg|*.png|*.gif|*.tiff";
            string[] files = OtherMethodsContainer.GetFiles(myContainer.sourceOfFolder, joinedformatOfFileInRootFolder,SearchOption.AllDirectories);


            //No Constructor, only one method call.
            PhotoConversionContainer.ImageConversionMethod(myContainer, files);


            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }




        //////////////////////////////////////////////////////////////////////////////////////////////
        /*
         * Here I would simply put all the methods from Class3.
         * 
         * You may wonder why I do this... well, TECHNICALLY you do not have to. It works this way just fine.
         * 
         * My personal style would however be to group all user input, as far as is anyhow logical, together. In my oppinion, this Program
         * class would do just fine for this purpose.
         * 
         * But to not make you feel insecure, I do not dismiss this idea entirely. One can very well do separate classes for this too, 
         * naturally.
         * Here I am slightly against it because I know the further purpose of this program. Your plan is to make in the end not just a 
         * console version, but also a Desktop and a Web version. For these you intend to make a DLL with the core functionality, which is
         * common among all three of these variations of the program. In fact, those are ultimately three completely separate programs, which
         * just happen to do the same thing.
         * 
         * Let that sink in. Three separate programs. Separate in terms of user input. And here is my issue. Class3 you will not be able to
         * use in all of these, there is no use to put it in the DLL.
         * 
         * Then again, you technically do not have to do this with Class3. You can also freely keep Class3 as a separate class within your
         * console application. And that is PERFECTLY LEGAL (speaking of coding convention) and PERFECTLY FINE to do.
         * 
         * So... ultimately, if you feel that you want to have a separate class handling user input via console... well actually, go for it.
         * That is in the end a clear goal for a class. A perfectly fine one. In that case I would have to advise you though that I would 
         * then let that class handle really ALL user input. Right now you seem to have used it only for the case when you have no XML file
         * to read from. This is somewhat problematic for reusability or, more perhaps, for finding stuff in your code. In the end you simply
         * have two places to look for, when you want to check your user input handling.
         * 
         * Conclusion - at first I wanted to kill that class, but now I will simply change the way the methods are used. I would STRONGLY
         * advise you though to make it fully handle all user input, or ditch the idea - unless you have a clear reason to split it up
         * that way (and not in methods for instance), which I right now just do not see. If so, convince me, I may miss something, even if
         * it is obvious.
         */
        //////////////////////////////////////////////////////////////////////////////////////////////
    }
}
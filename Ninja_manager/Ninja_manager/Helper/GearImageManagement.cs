using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_manager.Helper
{
    public class GearImageManagement
    {
        public static string SourceFolder => @"C:\Users\Evelien\Documents\GitHub\Prog5\Prog5\Ninja_manager\Ninja_manager\Assets\img\gear\";
        public static void SaveImage(int id, string path)
        {
            string destination = Path.Combine(SourceFolder, id + Path.GetExtension(path));
            File.Copy(path, destination, true);  
        }
        public static void DeleteImage(int id)
        {
            try
            {
                if (File.Exists(SourceFolder + id + ".png"))
                {
                    File.Delete(SourceFolder + id + ".png");
                }
                else if (File.Exists(SourceFolder + id + ".jpg"))
                {
                    File.Delete(SourceFolder + id + ".jpg");
                }
                else if (File.Exists(SourceFolder + id + ".jpeg"))
                {
                    File.Delete(SourceFolder + id + ".jpeg");
                }
                else if (File.Exists(SourceFolder + id + ".gif"))
                {
                    File.Delete(SourceFolder + id + ".gif");
                }
            }
            catch (Exception) { }
        }
    }
}

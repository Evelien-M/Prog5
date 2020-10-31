using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ninja_manager.Helper
{
    public class GearImageManagement
    {
        public static string SourceFolder => Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, @"Assets\img\gear\"); 
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

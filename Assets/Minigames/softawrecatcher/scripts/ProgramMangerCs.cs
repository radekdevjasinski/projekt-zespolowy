// See https://aka.ms/new-console-template for more information


using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using UnityEngine;


     public class IconExtractor: MonoBehaviour
    {
        static string softwarePath = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
    static string[] getLnkFilesInDirectroy(string path)
    {


        return Directory.GetFiles(path, "*.lnk", SearchOption.AllDirectories);
    }
    static string[] getDirectoriesinDirectroy(string path)
    {
        return Directory.GetDirectories(path, "*.*", SearchOption.AllDirectories);
    }

    public static void getAllFiles(string path, ref List<string> files)
    {
        string[] directories = getDirectoriesinDirectroy(path);
        foreach (string dir in directories)
        {
            getAllFiles(dir, ref files);
        }

        files.AddRange(IconExtractor.getLnkFilesInDirectroy(path));
    }

    public static List<string> getAllSoftwareFiles()
    {
        List<string> list = new List<string>();
        getAllFiles(softwarePath,ref list);
        return list;
    }

    public static List<string> getAllSoftwareNamesFile()
    {
        List<string> fileList = getAllSoftwareFiles();
        List<string> fileNames = fileList.ConvertAll<string>(obj => Path.GetFileNameWithoutExtension(obj));
        List<string> filteredList = (fileNames.Where(element => !element.ToUpper().Contains("UNINSTALL"))).ToList();
        return filteredList;
    }


    //static byte[] iconToBitmap(Icon icon)
    //{
    //    using (var memoryStream = new MemoryStream())
    //    {
    //        icon.ToBitmap().Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
    //        return memoryStream.ToArray();
    //    }
    //}

    //static HashSet<byte[]> getSetOfIconFrompaths(IEnumerable<string> paths)
    //{
    //HashSet<byte[]> set = new HashSet<byte[]>();
    //    foreach (string var in paths)
    //    {


    //    Icon icon = Icon.ExtractAssociatedIcon(var);
    //    icon.ToBitmap().Save("F:\\projects\\ProgramMangerCs\\ProgramMangerCs\\tmp\\010.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
    //    Debug.Log("extracitng icon form: " + icon.ToBitmap().RawFormat);
    //    set.Add(iconToBitmap(icon));
    //    }
    //    return set;

    //}

    //static public HashSet<byte[]> getSoftwareIcon()
    //{
    //    string path = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
    //    List<string> allFiles = new List<string>();
    //    getAllFiles(path, ref allFiles);

    //HashSet<byte[]> IconsBytes = getSetOfIconFrompaths(allFiles);



    ////Debug.Log("-------------------extracitng icon form: " + icon.ToBitmap().RawFormat);

    //return IconsBytes;
    //}


}

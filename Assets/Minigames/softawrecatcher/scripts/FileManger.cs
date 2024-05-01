

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using static Unity.Burst.Intrinsics.X86.Avx;

public class FileManger : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject sprtieRnder;
    void Start()
    {
        List<string> fileList= IconExtractor.getAllSoftwareFiles();
        List<string> fileNames = fileList.ConvertAll<string>(obj => Path.GetFileNameWithoutExtension(obj));
        List<string> filteredList= (fileNames.Where(element => !element.ToUpper().Contains("UNINSTALL"))).ToList();
        int i = 0;
        foreach (string element in filteredList)
        {
            i++;
            Debug.Log("elemnt: "+ element);
       
        }

        //ImageConverter converter = new ImageConverter();
        //;

        //Texture2D tex = new Texture2D(32, 32);
        
        //Icon icon = Icon.ExtractAssociatedIcon(@"projekt-zespolowy.sln");


        //Bitmap ic= icon.ToBitmap();

        //Rectangle rect = new Rectangle(0, 0, ic.Width, ic.Height);
        //System.Drawing.Imaging.BitmapData bmpData =
        //    ic.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, ic.PixelFormat);







        //byte[] testVytes = (byte[])converter.ConvertTo(ic, typeof(byte[]));
        //tex.LoadImage(testVytes);


        //Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
        //string fileTosave = "F:\\projects\\ProgramMangerCs\\ProgramMangerCs\\tmp\\" + "_00000000" + ".bmp";
        //File.WriteAllBytes(fileTosave, testVytes);
        //GameObject obj = Instantiate(sprtieRnder, transform);
        //obj.GetComponent<SpriteRenderer>().sprite = sprite;
        //ic.UnlockBits(bmpData);
    }
}


   





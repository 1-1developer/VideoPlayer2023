using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;



public class LoadFileManager : MonoBehaviour
{
    public DirectoryInfo directory;
    public string Streamingpath = Application.streamingAssetsPath;
    public List<string> videoNames = new List<string>();
    public TextMeshProUGUI tx;


    // Start is called before the first frame update
    void Awake()
    {
        directory = new DirectoryInfo(Streamingpath);
        FileInfo[] info = directory.GetFiles("*.*");
        foreach (FileInfo f in info)
        {
            Debug.Log(f.ToString());
        }
        //for (int i = 0; i < (directory.GetFiles(Streamingpath).Length); i++)
        //{
        //    videoNames.Add(directory.GetFiles(Streamingpath)[i].Name);
        //}
        //tx.text = directory.FullName;
    }
}

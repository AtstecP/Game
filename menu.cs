using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class menu : MonoBehaviour
{
    public TextMeshProUGUI record;
    private string path = (Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+@"\RecordsTable.txt");

    // Создает файл и выводит из него данные на экран 
    void Start()
    {
        if (!File.Exists(path))
        {
            File.Create(path).Close();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@path))
            {
                file.Write("1:Record -\n2:Record -\n3:Record -");
            }      
        }
        var str = new List<string>();
        using (System.IO.StreamReader reader = System.IO.File.OpenText(@path))
        {
            str.AddRange(reader.ReadLine().Split(' '));
            str.AddRange(reader.ReadLine().Split(' '));  
            str.AddRange(reader.ReadLine().Split(' '));
        } 
        record.text = $"1:Record {str[1]} sec\n2:Record {str[3]} sec\n3:Record {str[5]} sec";
    }
    
    // меняет уровни и переходи в меню 
    public void choisescene(int numb)
    {
        SceneManager.LoadScene(numb);
    }
    

}

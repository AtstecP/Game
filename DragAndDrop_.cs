using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DragAndDrop_ : MonoBehaviour
{ 
    public int amountPiece;
    public GameObject SelectedPiece;  
    public int placedPieces = 0;
    private float timeValue;
    public TextMeshProUGUI timerText;
    private bool signal;
    public bool[] onPosition = new bool[11];
    private string path = (Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+@"\RecordsTable.txt");

    // Заполняет масив false, кроме 0 элемнта, он true. Запускает таймер 
    void Start()
    {
        timeValue = 0;
        signal = true;
        onPosition[0] = true;
        for (int i = 1; i < 11; ++i)
        {
            onPosition[i] = false;
        }
    }
    // меняет уровни и переходи в меню 
    public void choisescene(int numb)
    {
        SceneManager.LoadScene(numb);
    }
    
    // Запускается каждый кадр 
    void Update()
    {
        if (signal) // таймер 
        {
            timeValue += Time.deltaTime;
            timerText.text = Mathf.Round(timeValue).ToString( ) + " sec";
        }
        if ((Input.GetMouseButtonDown(0)) && (placedPieces < amountPiece))  //движение объектов по экрану 
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform.CompareTag("Piece"))
            {
                if (!hit.transform.GetComponent<piceseScript>().InRightPosition)
                {
                    SelectedPiece = hit.transform.gameObject;
                    SelectedPiece.GetComponent<piceseScript>().Selected = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0)) // Передает данные в скрипт объекта о том что его отпустили 
        {
            if (SelectedPiece != null)
            {
                SelectedPiece.GetComponent<piceseScript>().Selected = false;
                SelectedPiece = null;
            }
        }
        if (SelectedPiece != null) //Двигает объект за мышкой 
        {
            Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SelectedPiece.transform.position = new Vector3(MousePoint.x,MousePoint.y,0);
        }   
        if (placedPieces == amountPiece) // останавливает таймер, запускает проверку рекорда 
        {
            signal = false;
            timeValue = Mathf.Round(timeValue);
            timerText.text = timeValue.ToString( ) + " sec\n stop";
            ++placedPieces; 
            checkRecord(timeValue);
        }
    }
    // проверяет рекорд и записывает в файл 
    private void checkRecord(float timeValue)
    {
        var str = new List<string>();
        using (System.IO.StreamReader reader = System.IO.File.OpenText(@path))
        {
            str.AddRange(reader.ReadLine().Split(' '));
            str.AddRange(reader.ReadLine().Split(' ')); 
            str.AddRange(reader.ReadLine().Split(' ')); 
        }
        if (amountPiece == 4) 
        {
            if ((str[1] == "-") || (float.Parse(str[1]) > timeValue)) 
            {
                str[1] = timeValue.ToString();
                timerText.text = timeValue.ToString() + " sec\nNew record";
            }
        }else if (amountPiece == 8)
        {
            if ((str[3] == "-") || (float.Parse(str[3]) > timeValue)) 
            {
                +.ToString();
                timerText.text = timeValue.ToString() + " sec\nNew record";
            }
        }else 
        {
            if ((str[5] == "-") || (float.Parse(str[5]) > timeValue)) 
            {
                str[5] = timeValue.ToString();
                timerText.text = timeValue.ToString() + " sec\nNew record";
            }
        }
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@path))
        {
            file.Write($"1:Record {str[1]}\n2:Record {str[3]}\n3:Record {str[5]}");
        }   
    }
}
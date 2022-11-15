using System.Collections.Generic;
using System.Collections;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class main : MonoBehaviour
{
    public Button startBut, restartBut;
    public GameObject walpaper;
    public Rigidbody2D massObj;
    public TextMeshProUGUI periodText;
    public TMP_InputField  massInput, rigidityInput;
    public SpringJoint2D[] spring = new SpringJoint2D[66];
    public Rigidbody2D[] body = new Rigidbody2D[33]; 
    public HingeJoint2D[] breakForceHinge = new HingeJoint2D[33];

    //Запускаекться при старте выводит текст и включает\выключает кнопки 
    void Start()
    {
        startBut.gameObject.SetActive(true);
        restartBut.gameObject.SetActive(false);
        periodText.text = "0";
    }

    //Перезапускает игру 
    public void SwapScene(int x)
    {
        SceneManager.LoadScene(x);
    }

    //Запускается при начале симуляции
    public void StartSimulation()
    {
        float mass = float.Parse(massInput.text);
        float delX = mass*10/float.Parse(rigidityInput.text);
        float frecensy;
        Destroy(walpaper);
        startBut.gameObject.SetActive(false);
        restartBut.gameObject.SetActive(true);
        frecensy = (1 / (2* Mathf.PI)) *  Mathf.Pow( float.Parse(rigidityInput.text)/ mass,0.5f);
        periodText.text =$"Period = {Mathf.Round(1/frecensy* 1000.0f) * 0.001f} sec\n\ndelX = {Mathf.Round(delX* 1000.0f) * 0.001f} m";
        massObj.mass = mass;
        if (delX >= 0.15)
        {
            periodText.text = "ERROR\n(delX >= 0.15 M)";
            foreach (var obj in breakForceHinge)
            {
                obj.breakForce = frecensy*100/(mass);
            }
        }
        else if ( float.Parse(rigidityInput.text) >= 1000)
            periodText.text = "ERROR\n(rigidity >= 1000)";
        foreach (var obj in spring)
        {
            obj.frequency = frecensy;
        }
        foreach (var obj in body)
        {
            obj.isKinematic = false;
        }
        
    }
}

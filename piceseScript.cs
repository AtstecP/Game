using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class piceseScript : MonoBehaviour
{
    private Vector3 RightPosition;
    public bool InRightPosition;
    public bool Selected;
    public int previous;
    // Перемешает кольца в случаное место на экране
    void Start()
    {
        RightPosition = transform.position;
        transform.position = new Vector3(Random.Range(6f, 0f), Random.Range(2.5f, -2), 0);
    }
    // Дотягивает кольцо до нужной ячейки и фиксирует его
    void Update()
    {
        if ((Vector3.Distance(transform.position, RightPosition) < 0.5f) && (Camera.main.GetComponent<DragAndDrop_>().onPosition[previous]))
        {   
            if (!Selected)
            {
                if (InRightPosition == false)
                {
                    Camera.main.GetComponent<DragAndDrop_>().placedPieces += 1;
                    transform.position = new Vector3(RightPosition.x, RightPosition.y, 2);
                    InRightPosition = true;
                    GetComponent<SortingGroup>().sortingOrder = 0;
                    Camera.main.GetComponent<DragAndDrop_>().onPosition[previous+1] = true;
                }
            }
        }
    }
}

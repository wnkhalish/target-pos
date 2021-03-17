using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisController : MonoBehaviour
{
    [SerializeField]
    private GameObject xAxis;
    [SerializeField]
    private GameObject yAxis;


    // Update is called once per frame
    void Update()
    {
        Vector2 currentMousePosition = GetMousePosition();
        UpdateXAxis(currentMousePosition);
        UpdateYAxis(currentMousePosition);
    }

    void UpdateXAxis(Vector2 mousePos)
    {
        xAxis.transform.position = new Vector3(xAxis.transform.position.x,mousePos.y, xAxis.transform.position.z);
    }

    void UpdateYAxis(Vector2 mousePos)
    {
        yAxis.transform.position = new Vector3(mousePos.x, yAxis.transform.position.y, yAxis.transform.position.z);
    }

    Vector2 GetMousePosition()
    {
        Vector2 currentMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        return Camera.main.ScreenToWorldPoint(currentMousePosition);
    }
}

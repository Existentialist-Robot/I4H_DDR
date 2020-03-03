using System.Collections;
using UnityEngine;
using System.IO.Ports;

public class ArduinoController : MonoBehaviour
{
    public static ArduinoController Instance;

    SerialPort stream = new SerialPort("COM3", 9600);

    int buttonState = 0;

    void Start()
    {
        Instance = this;
        stream.Open();
    }

    private void Update()
    {
        string value = stream.ReadLine();
        Debug.Log(value);
        buttonState = int.Parse(value);
    }

    private void OnGUI()
    {
        string newString = "Connected: " + buttonState;
        GUI.Label(new Rect(10, 10, 300, 100), newString);
    }

}

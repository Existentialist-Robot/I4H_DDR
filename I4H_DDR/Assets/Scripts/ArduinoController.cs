using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoController : MonoBehaviour
{
    public static ArduinoController Instance;

    public string portName;
    SerialPort arduino;
    private string[] configTiles;

    void Start()
    {
        Instance = this;
        arduino = new SerialPort(portName, 9600);
        arduino.Open();
    }

    public void UpdateGameState()
    {
        if (arduino.IsOpen)
        {
            arduino.Write("9");
        }
    }

    public void SendConfiguration(string[] tiles)
    {
        configTiles = tiles;
        StartCoroutine("SendTiles");
    }

    public void SendTile(string tile)
    {
        arduino.Write(tile);
    }

    IEnumerator SendTiles()
    {
        for(int i = 0; i < configTiles.Length; i++)
        {
            SendTile(configTiles[i]);
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}

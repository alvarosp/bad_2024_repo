using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Position
{
    public string name;
    public float Timestamp;
    public Vector3 position;

    public Position(string name, float timestamp, Vector3 position)
    {
        this.name = name;
        Timestamp = timestamp;
        this.position = position;
    }

    public Position()
    {

    }

    public string ToCSV()
    {
        return $"{name};{Timestamp};{position.x};{position.y};{position.z}"; //Interpolated String
    }
}

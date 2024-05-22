using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public GameObject Player;
    private List<Position> PositionList = new List<Position>();
    private float LogInterval;
    private float SaveInterval;
    private float PrevTime;
    private float PrevSaveTime;

    // Start is called before the first frame update
    void Start()
    {
        LogInterval = 1;
        SaveInterval = 5;
        PrevTime = Time.realtimeSinceStartup;
        PrevSaveTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        float CurrentTime = Time.realtimeSinceStartup;
        if (CurrentTime > PrevTime + LogInterval)
        {
            Debug.Log("Saved");
            Position pos = new Position("Player", CurrentTime, Player.transform.position);
            PositionList.Add(pos);
            PrevTime += LogInterval;
        }
        if (CurrentTime > PrevSaveTime + SaveInterval)
        {
            Debug.Log("File saved");
            PrevSaveTime += SaveInterval;
            SaveCSVToFile();
        }
        
    }

    private void SaveCSVToFile()
    {
        string data = "Nombre;Timestamp;x;y;z\n";
        foreach (Position pos in PositionList)
        {
            data += pos.ToCSV() + "\n";
        }
        Debug.Log(data);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class DataManager : MonoBehaviour
{
    public GameObject Player;
    private List<Position> PositionList = new List<Position>();
    private Positions positions = new Positions();
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
            positions.list.Add(pos);
            PrevTime += LogInterval;
        }
        if (CurrentTime > PrevSaveTime + SaveInterval)
        {
            Debug.Log("File saved");
            PrevSaveTime += SaveInterval;
            SaveCSVToFile();
            SaveJSONToFile();
            SaveXMLToFile();
        }
    }

    private void SaveCSVToFile()
    {
        string data = "Nombre;Timestamp;x;y;z\n";
        foreach (Position pos in PositionList)
        {
            data += pos.ToCSV() + "\n";
        }
        FileManager.WriteToFile("positions.csv", data);
    }

    private void SaveJSONToFile()
    {
        /*string data = "[";
        foreach (Position pos in PositionList)
        {
            data += JsonUtility.ToJson(pos) + ",";
        }
        data = data.Substring(0, data.Length - 1);
        //data = data.Remove(data.Length - 1));
        data += "]";*/
        FileManager.WriteToFile("positions.json", JsonUtility.ToJson(positions));
    }

    private void SaveXMLToFile()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Positions));
        using (FileStream stream = new FileStream("positions.xml", FileMode.Create))
        {
            serializer.Serialize(stream, positions);
        }
    }
}

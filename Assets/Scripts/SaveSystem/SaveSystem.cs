using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    // Should be set up while creating level
    public int localLevelNum = -1;
    public string saveFile = "Checkpoint.txt";

    // Will be set during gameplay
    public int localCheckpontNum = 1;

    // Will be read from file
    public int savedLevelNum = -1;
    public int savedCheckpontNum = -1;

    // Start is called before the first frame update
    void Start()
    {
        ReadCheckpoint();
    }
    
    public void ReadCheckpoint()
    {
        // Read
        StreamReader reader = new StreamReader(saveFile);
        string checkpointDataRaw = reader.ReadLine();
        string[] checkpointDataArray = checkpointDataRaw.Split(',');
        savedLevelNum = int.Parse(checkpointDataArray[0]);
        savedCheckpontNum = int.Parse(checkpointDataArray[1]);
        reader.Close();
    }

    public void WriteCheckpoint()
    {
        savedLevelNum = savedLevelNum > localLevelNum ? savedLevelNum : localLevelNum;
        savedCheckpontNum = savedCheckpontNum > localCheckpontNum ? savedCheckpontNum : localCheckpontNum;

        // Write to disk
        string serializedData = savedLevelNum.ToString() + "," + savedCheckpontNum.ToString();

        StreamWriter writer = new StreamWriter(saveFile, false);
        writer.Write(serializedData);
        writer.Close();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    // Should be set up while creating level
    public int localLevelNum = -1;
    public string saveFile = "Checkpoint.txt";
    public GameObject[] platforms;
    public GameObject[] bridges;
    public GameObject golemObject;
    public GameObject mushroomObject;

    private Character golem;
    private Character mushroom;
    


    // Will be set during gameplay
    public int localCheckpontNum = 1;

    // Will be read from file
    public int savedLevelNum = -1;
    public int savedCheckpontNum = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (LoaderWatchDog.wasLoaded)
        {
            LoadGame(LoaderWatchDog.saveNum);
            LoaderWatchDog.wasLoaded = false;
        }
        else
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

    public SaveData CreateSaveObject()
    {
        SaveData save = new SaveData();
        int i = 0;
        foreach (GameObject targetGameObject in platforms)
        {
            MovingPlatform target = targetGameObject.GetComponent<MovingPlatform>();
            save.platformsActive.Add(target.active);
            save.platformsOnlyOneInteractionUsed.Add(target.onlyOneInteractionUsed);
            save.platformsPointNumber.Add(target.pointNumber);
            save.platformsCurrentTargetX.Add(target._currentTarget.x);
            save.platformsCurrentTargetY.Add(target._currentTarget.y);
            save.platformsCurrentTargetZ.Add(target._currentTarget.z);
            save.platformsPreviousOperationForward.Add(target.previousOperationForward);
            save.platformsTolerance.Add(target.tolerance);
            save.platformsM_HitDetect.Add(target.m_HitDetect);
            Debug.Log(target.transform.position);
            save.platformsPositionX.Add(target.transform.position.x);
            save.platformsPositionY.Add(target.transform.position.y);
            save.platformsPositionZ.Add(target.transform.position.z);
            i++;
        }

        int j = 0;
        foreach (GameObject targetGameObject in bridges)
        {
            Bridge target = targetGameObject.GetComponent<Bridge>();
            save.bridgesActive.Add(target.active);
            save.bridgesPointNumber.Add(target.pointNumber);
            save.bridgesSign.Add(target.sign);
            save.bridgesCurrentTarget.Add(target._currentTarget);
            save.bridgesTolerance.Add(target.tolerance);
            Debug.Log(target.transform.eulerAngles);
            save.bridgesRotation.Add(target.transform.eulerAngles.z);
            j++;
        }

        if (golemObject != null)
        {
            Character golem = golemObject.GetComponent<Character>();

            save.golemPositionX = golem.transform.position.x;
            save.golemPositionY = golem.transform.position.y;
            save.golemPositionZ = golem.transform.position.z;
            save.golemRotationX = golem.transform.GetChild(0).transform.rotation.x;
            save.golemRotationY = golem.transform.GetChild(0).transform.rotation.y;
            save.golemRotationZ = golem.transform.GetChild(0).transform.rotation.z;
            save.golemRotationW = golem.transform.GetChild(0).transform.rotation.w;
            save.golemBools.Add(golem.isActive);
            save.golemBools.Add(golem.isGrounded);
            save.golemBools.Add(golem.isCombined);
            save.golemBools.Add(golem.isOnTopOfGolem);
            save.golemBools.Add(golem.isPulling);
            save.golemBools.Add(golem.isFacingRight);
            save.golemBools.Add(golem.isUsingMechanism);
            save.golemBools.Add(golem.isGlueToMechanism);
            save.golemBools.Add(golem.isOnTopOfMovable);
        }

        Character mushroom = mushroomObject.GetComponent<Character>();

        save.mushroomPositionX = mushroom.transform.position.x;
        save.mushroomPositionY = mushroom.transform.position.y;
        save.mushroomPositionZ = mushroom.transform.position.z;
        save.mushroomRotationX = mushroom.transform.GetChild(0).transform.rotation.x;
        save.mushroomRotationY = mushroom.transform.GetChild(0).transform.rotation.y;
        save.mushroomRotationZ = mushroom.transform.GetChild(0).transform.rotation.z;
        save.mushroomRotationW = mushroom.transform.GetChild(0).transform.rotation.w;
        save.mushroomBools.Add(mushroom.isActive);
        save.mushroomBools.Add(mushroom.isGrounded);
        save.mushroomBools.Add(mushroom.isCombined);
        save.mushroomBools.Add(mushroom.isOnTopOfGolem);
        save.mushroomBools.Add(mushroom.isPulling);
        save.mushroomBools.Add(mushroom.isFacingRight);
        save.mushroomBools.Add(mushroom.isUsingMechanism);
        save.mushroomBools.Add(mushroom.isGlueToMechanism);
        save.mushroomBools.Add(mushroom.isOnTopOfMovable);

        save.localLevelNum = localLevelNum;
        save.localCheckpontNum = localCheckpontNum;
        save.savedLevelNum = savedLevelNum;
        save.savedCheckpontNum = savedCheckpontNum;

        save.sceneName = SceneManager.GetActiveScene().name;

        return save;
    }

    public void SaveGame(int num)
    {
        // 1
        SaveData save = CreateSaveObject();

        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(num + ".save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved");
    }


    public void LoadGame(int num)
    {
        // 1
        if (File.Exists(num + ".save"))
        {
            // 2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(num + ".save", FileMode.Open);
            SaveData save = (SaveData)bf.Deserialize(file);
            Debug.Log(save);
            file.Close();

            // 3
            int i = 0;
            foreach (GameObject targetGameObject in platforms)
            {
                MovingPlatform target = targetGameObject.GetComponent<MovingPlatform>();
                target.active = save.platformsActive[i];
                target.onlyOneInteractionUsed = save.platformsOnlyOneInteractionUsed[i];
                target.pointNumber = save.platformsPointNumber[i];
                target._currentTarget = new Vector3(save.platformsCurrentTargetX[i], save.platformsCurrentTargetY[i], save.platformsCurrentTargetZ[i]);
                target.previousOperationForward = save.platformsPreviousOperationForward[i];
                target.tolerance = save.platformsTolerance[i];
                target.m_HitDetect = save.platformsM_HitDetect[i];
                target.transform.position = new Vector3(save.platformsPositionX[i], save.platformsPositionY[i], save.platformsPositionZ[i]);
                i++;
            }

            int j = 0;
            foreach (GameObject targetGameObject in bridges)
            {
                Bridge target = targetGameObject.GetComponent<Bridge>();
                target.active = save.bridgesActive[j];
                target.pointNumber = save.bridgesPointNumber[j];
                target.sign = save.bridgesSign[j];
                target._currentTarget = save.bridgesCurrentTarget[j];
                target.tolerance = save.bridgesTolerance[j];
                target.transform.eulerAngles = new Vector3(target.transform.eulerAngles.x, target.transform.eulerAngles.y, save.bridgesRotation[j]);
                j++;
            }

            if (golemObject != null)
            {
                Character golem = golemObject.GetComponent<Character>();

                golem.transform.position = new Vector3(save.golemPositionX, save.golemPositionY, save.golemPositionZ);
                golem.transform.rotation = new Quaternion(save.golemRotationX, save.golemRotationY, save.golemRotationZ, save.golemRotationW);
                golem.isActive = save.golemBools[0];
                golem.isGrounded = save.golemBools[1];
                golem.isCombined = save.golemBools[2];
                golem.isOnTopOfGolem = save.golemBools[3];
                golem.isPulling = save.golemBools[4];
                golem.isFacingRight = save.golemBools[5];
                golem.isUsingMechanism = save.golemBools[6];
                golem.isGlueToMechanism = save.golemBools[7];
                golem.isOnTopOfMovable = save.golemBools[8];
            }

            Character mushroom = mushroomObject.GetComponent<Character>();
            mushroom.transform.position = new Vector3(save.mushroomPositionX, save.mushroomPositionY, save.mushroomPositionZ);
            mushroom.transform.rotation = new Quaternion(save.mushroomRotationX, save.mushroomRotationY, save.mushroomRotationZ, save.mushroomRotationW);
            mushroom.isActive = save.mushroomBools[0];
            mushroom.isGrounded = save.mushroomBools[1];
            mushroom.isCombined = save.mushroomBools[2];
            mushroom.isOnTopOfGolem = save.mushroomBools[3];
            mushroom.isPulling = save.mushroomBools[4];
            mushroom.isFacingRight = save.mushroomBools[5];
            mushroom.isUsingMechanism = save.mushroomBools[6];
            mushroom.isGlueToMechanism = save.mushroomBools[7];
            mushroom.isOnTopOfMovable = save.mushroomBools[8];
            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }
}

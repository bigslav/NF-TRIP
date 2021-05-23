using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int localLevelNum;
    public int localCheckpontNum;
    public int savedLevelNum;
    public int savedCheckpontNum;

    public List<bool> platformsActive = new List<bool>();
    public List<bool> platformsOnlyOneInteractionUsed = new List<bool>();
    public List<int> platformsPointNumber = new List<int>();
    public List<float> platformsCurrentTargetX = new List<float>();
    public List<float> platformsCurrentTargetY = new List<float>();
    public List<float> platformsCurrentTargetZ = new List<float>();
    public List<bool> platformsPreviousOperationForward = new List<bool>();
    public List<float> platformsTolerance = new List<float>();
    public List<bool> platformsM_HitDetect = new List<bool>();
    public List<float> platformsPositionX = new List<float>();
    public List<float> platformsPositionY = new List<float>();
    public List<float> platformsPositionZ = new List<float>();

    public List<bool> bridgesActive = new List<bool>();
    public List<int> bridgesPointNumber = new List<int>();
    public List<int> bridgesSign = new List<int>();
    public List<int> bridgesCurrentTarget = new List<int>();
    public List<float> bridgesTolerance = new List<float>();
    public List<float> bridgesRotation = new List<float>();

    public float golemPositionX;
    public float golemPositionY;
    public float golemPositionZ;
    public float golemRotationX;
    public float golemRotationY;
    public float golemRotationZ;
    public float golemRotationW;
    public List<bool> golemBools = new List<bool>();
    
    public float mushroomPositionX;
    public float mushroomPositionY;
    public float mushroomPositionZ;
    public float mushroomRotationX;
    public float mushroomRotationY;
    public float mushroomRotationZ;
    public float mushroomRotationW;
    public List<bool> mushroomBools = new List<bool>();

    public string sceneName;
}

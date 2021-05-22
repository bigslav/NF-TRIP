using UnityEngine;
using Cinemachine;

public class VCamFollower : MonoBehaviour
{
    public string objectToFollowTag;

    private GameObject objectToFollow;
    private CinemachineVirtualCamera VCam;
    
    private void Start()
    {
        VCam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (objectToFollow == null) 
        {
            objectToFollow = GameObject.FindGameObjectWithTag(objectToFollowTag);
            VCam.Follow = objectToFollow.transform;
        }
    }
}

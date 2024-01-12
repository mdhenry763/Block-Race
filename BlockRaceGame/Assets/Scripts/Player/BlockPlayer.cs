using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Netcode;

public class BlockPlayer : NetworkBehaviour
{
    [SerializeField] private CinemachineVirtualCamera followCam;
    [SerializeField] private int camPriority = 15;

    public override void OnNetworkSpawn()
    {
        if(IsOwner)
        {
            followCam.Priority = camPriority;
        }
    }
}

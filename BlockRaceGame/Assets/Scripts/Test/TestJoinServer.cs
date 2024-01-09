using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TestJoinServer : MonoBehaviour
{
    public void JoinSever()
    {
        NetworkManager.Singleton.StartClient();
    }
}

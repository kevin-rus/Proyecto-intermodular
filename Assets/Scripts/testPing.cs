using PurrNet;
using System;
using UnityEngine;

public class testPing : NetworkBehaviour
{
    [ObserversRpc]
    public void RpcPing()
    {
        Debug.Log("CLIENTE recibió RPC");
    }

    [ServerRpc]
    public void ClientRpcPing()
    {
        Debug.Log("SERVER recibió RPC");
        RpcPing();
    }

    void Update()
    {
        if (isServer && Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Servidor envía RPC");
            RpcPing();
        }
        else if (!isServer && Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Cliente envía RPC");
            ClientRpcPing();
        }
    }
}
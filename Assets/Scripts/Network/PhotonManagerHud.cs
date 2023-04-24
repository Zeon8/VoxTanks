using System;
using System.Runtime.CompilerServices;
using Netcode.Transports.PhotonRealtime;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;
using UnityEngine;
using VoxTanks.Tank.Turrets;

[RequireComponent(typeof(NetworkManager))]
[DisallowMultipleComponent]
public class PhotonManagerHud : MonoBehaviour
{
    NetworkManager m_NetworkManager;

    PhotonRealtimeTransport m_Transport;

    GUIStyle m_LabelTextStyle;

    // This is needed to make the port field more convenient. GUILayout.TextField is very limited and we want to be able to clear the field entirely so we can't cache this as ushort.

    public Vector2 DrawOffset = new Vector2(10, 10);

    public Color LabelColor = Color.black;
    

    void Awake()
    {
        // Only cache networking manager but not transport here because transport could change anytime.
        m_NetworkManager = GetComponent<NetworkManager>();
        m_LabelTextStyle = new GUIStyle(GUIStyle.none);
    }

    void OnGUI()
    {
        m_LabelTextStyle.normal.textColor = LabelColor;

        m_Transport = (PhotonRealtimeTransport)m_NetworkManager.NetworkConfig.NetworkTransport;

        GUILayout.BeginArea(new Rect(DrawOffset, new Vector2(200, 200)));

        if (IsRunning(m_NetworkManager))
        {
            DrawStatusGUI();
        }
        else
        {
            DrawConnectGUI();
        }

        GUILayout.EndArea();
    }

    void DrawConnectGUI()
    {

        if (GUILayout.Button("Host (Server + Client)"))
        {
            m_NetworkManager.StartHost();
        }

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Server"))
        {
            m_NetworkManager.StartServer();
        }

        if (GUILayout.Button("Client"))
        {
            m_NetworkManager.StartClient();
        }

        GUILayout.EndHorizontal();
    }

    private bool _showTurrets;

    void DrawStatusGUI()
    {
        if (m_NetworkManager.IsServer)
        {
            var mode = m_NetworkManager.IsHost ? "Host" : "Server";
            GUILayout.Label($"{mode} active on room: {m_Transport.RoomName}", m_LabelTextStyle);
        }
        else
        {
            if (m_NetworkManager.IsConnectedClient)
            {
                GUILayout.Label($"Client on room {m_Transport.RoomName}", m_LabelTextStyle);
            }
        }

        if (GUILayout.Button("Shutdown"))
        {
            m_NetworkManager.Shutdown();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool IsRunning(NetworkManager networkManager) => networkManager.IsServer || networkManager.IsClient;
}
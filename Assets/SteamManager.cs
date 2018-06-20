using UnityEngine;
using System.Collections;

public class SteamManager  {

    private static uint appId = 480;
    public static Facepunch.Steamworks.Client client;


    public static void StartClient()
    {

        if (appId == 0)
            throw new System.Exception("You need to set the AppId to your game");

        //
        // Configure us for this unity platform
        //
        Facepunch.Steamworks.Config.ForUnity(Application.platform.ToString());

        //
        // Create a steam_appid.txt (this seems greasy as fuck, but this is exactly
        // what UE's Steamworks plugin does, so fuck it.
        //
        try
        {
            System.IO.File.WriteAllText("steam_appid.txt", appId.ToString());
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Couldn't write steam_appid.txt: " + e.Message);
        }

        // Create the client
        client = new Facepunch.Steamworks.Client(appId);

        if (!client.IsValid)
        {
            client = null;
            Debug.LogWarning("Couldn't initialize Steam");
            return;
        }

        Debug.Log("Steam Initialized: " + client.Username + " / " + client.SteamId + " time: " + Time.time);
    }

    public static void UpdateClient()
    {
        if (client == null)
            return;

        try
        {
            // UnityEngine.Profiling.Profiler.BeginSample("Steam Update");
            client.Update();
        }
        finally
        {
            Debug.Log("What the fuck");
            // UnityEngine.Profiling.Profiler.EndSample();
        }
    }

    private void DestroyClient()
    {
        if (client != null)
        {
            client.Dispose();
            client = null;
        }
    }

}

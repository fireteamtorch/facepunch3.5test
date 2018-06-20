using UnityEngine;
using System.Collections;

public class TestSteamFunction1 : MonoBehaviour {

    public float timer = 0;
    public float threshold = 4;
    public bool hasClientStarted = false;

	// Use this for initialization
	void Start () {
        SteamManager.StartClient();
	}
	
	// Update is called once per frame
	void Update () {
        /*
        timer += Time.deltaTime;
        if (timer > threshold)
        {
            if (!hasClientStarted)
            {
                SteamManager.StartClient();
                hasClientStarted = true;
            } else
            {
                //var username = Facepunch.Steamworks.Client.Instance.Username;
                //var username = SteamManager.client.Username;
                //Debug.Log("Username = " + username);
            }
            timer -= threshold;
        }
        */
	}
}

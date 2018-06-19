using UnityEngine;
using System.Collections;

public class TestSteamFunction1 : MonoBehaviour {

    public float timer = 0;
    public float threshold = 4;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > threshold)
        {
            var username = Facepunch.Steamworks.Client.Instance.Username;
            Debug.Log("Username = " + username);
            timer -= threshold;
        }
	}
}

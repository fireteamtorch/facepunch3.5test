using UnityEngine;
using System.Collections;

public class TestSteamFunction1 : MonoBehaviour {

    public float timer = 0;
    public float threshold = 4;
    public bool hasClientStarted = false;

    public Facepunch.Steamworks.Leaderboard testLeaderboard;

	// Use this for initialization
	void Start () {
        SteamManager.StartClient();
	}
	
	// Update is called once per frame
	void Update () {
        SteamManager.UpdateClient();

        if (testLeaderboard == null) {
            testLeaderboard = SteamManager.client.GetLeaderboard("TestLeaderboard", Facepunch.Steamworks.Client.LeaderboardSortMethod.Ascending, Facepunch.Steamworks.Client.LeaderboardDisplayType.Numeric);
            //testLeaderboard.FetchScores(Facepunch.Steamworks.Leaderboard.RequestType.Global, 0, 100);
            //Debug.Log(testLeaderboard.Name + ", total entries:" + testLeaderboard.TotalEntries);
        }

        // Add the shit
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Trying to add score");
            int num2add = 20;
            int[] shitToAdd = new int[num2add];
            for (int i = 0; i < num2add; i++)
            {
                shitToAdd[i] = int.MaxValue - i;
            }
            // bool isSuccess = testLeaderboard.AddScore(false, 500, 1,2,33,4,5);
            bool isSuccess = testLeaderboard.AddScore(false, -100, shitToAdd, null, null);
            Debug.Log("Add success: " + isSuccess);
            //uint datuint = 4294967291;
            //int datint = -5;
            uint datuint = uint.MaxValue;
            int datint = int.MinValue;
            Debug.Log("uint = " + datuint + ", cast int = " + (int)datuint);
            Debug.Log("int = " + datint + ", cast uint = " + (uint)datint);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //testLeaderboard = SteamManager.client.GetLeaderboard("TestLeaderboard", Facepunch.Steamworks.Client.LeaderboardSortMethod.Ascending, Facepunch.Steamworks.Client.LeaderboardDisplayType.Numeric);
            //testLeaderboard.FetchScores(Facepunch.Steamworks.Leaderboard.RequestType.Global, 0, 100);d
            if (testLeaderboard.Results != null)
            {
                Debug.Log(testLeaderboard.Name + ", total entries:" + testLeaderboard.TotalEntries);
                foreach (var tempVar in testLeaderboard.Results)
                {
                    Debug.Log(" rank: " + tempVar.GlobalRank + " name: " + tempVar.Name + " score: " + tempVar.Score + " ID: " + tempVar.SteamId);
                    if (tempVar.SubScores != null)
                    {
                        foreach (int currSubScore in tempVar.SubScores)
                        {
                            Debug.Log("Subscore for " + tempVar.Name + ": " + currSubScore);
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {

            testLeaderboard.FetchScores(Facepunch.Steamworks.Leaderboard.RequestType.Global, 0, testLeaderboard.TotalEntries);
            Debug.Log(testLeaderboard.Name + ", total entries:" + testLeaderboard.TotalEntries);
        }
        /*
        if(!testLeaderboard.IsQuerying && testLeaderboard.Results != null)
        {

        }
        */
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

    public void onAddSuccess(bool i)
    {
        Debug.Log("Added score successfully");
    }

    public void onAddFailure(bool i)
    {
        Debug.Log("Score add unsuccessful");
    }

}

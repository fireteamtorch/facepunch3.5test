using UnityEngine;
using System.Collections;

public class TestSteamFunction1 : MonoBehaviour {

    public float timer = 0;
    public float threshold = 4;
    public bool hasClientStarted = false;

    public Facepunch.Steamworks.Leaderboard testLeaderboard;

    public string outputFileString;

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
            
            const string attachment = "123 facepunch! \n testing \t testlkjlkaegjl \n \n aflkhlaghjla";
            var tempRemoteFile = Facepunch.Steamworks.Client.Instance.RemoteStorage.CreateFile("remoteVideo.mp4");
            //file.WriteAllText(attachment);

            byte[] videoBytes = System.IO.File.ReadAllBytes("Assets/testVideoBB.mp4");
            Debug.Log(videoBytes.Length);

            byte[] miniArray = new byte[3000];
            System.Array.Copy(videoBytes, 0, miniArray, 0, miniArray.Length);

            tempRemoteFile.WriteAllBytes(miniArray);

            bool tempAttachBool = testLeaderboard.AttachRemoteFile(tempRemoteFile, null, null);
            Debug.Log("Attach: " + tempAttachBool.ToString());
            

            Debug.Log("Trying to add score");
            int num2add = 20;
            int[] shitToAdd = new int[num2add];
            for (int i = 0; i < num2add; i++)
            {
                shitToAdd[i] = int.MaxValue - i;
            }
            // bool isSuccess = testLeaderboard.AddScore(false, 500, 1,2,33,4,5);
            bool isSuccess = testLeaderboard.AddScore(false, -500, shitToAdd, null, null);
            Debug.Log("Add success: " + isSuccess);
            /*
            uint datuint = 4294967291;
            int datint = -5;
            uint datuint = uint.MaxValue;
            int datint = int.MinValue;
            Debug.Log("uint = " + datuint + ", cast int = " + (int)datuint);
            Debug.Log("int = " + datint + ", cast uint = " + (uint)datint);
            */

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
                    Debug.Log("rank: " + tempVar.GlobalRank + "\tname: " + tempVar.Name + "\tscore: " + tempVar.Score + "\tID: " + tempVar.SteamId);
                    if(tempVar.AttachedFile != null)
                    {
                        Debug.Log(tempVar.AttachedFile.FileName);
                    }
                    /*
                    if (tempVar.SubScores != null)
                    {
                        foreach (int currSubScore in tempVar.SubScores)
                        {
                            Debug.Log("Subscore for " + tempVar.Name + ": " + currSubScore);
                        }
                    }
                    */
                }
            }
        }

        Facepunch.Steamworks.Leaderboard.Entry tempEntry;

        if (Input.GetKeyDown(KeyCode.X))
        {
            foreach (var tempVar in testLeaderboard.Results)
            {
                if(tempVar.Name == "Silverhsu")
                {
                    //tempVar.AttachedFile.dow
                    tempEntry = tempVar;
                    tempVar.AttachedFile.Download(null, null);
                    Debug.Log("Starting download");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            foreach (var tempVar in testLeaderboard.Results)
            {
                if (tempVar.Name == "Silverhsu")
                {
                    //tempVar.AttachedFile.dow
                    //tempEntry = tempVar;
                    //tempVar.AttachedFile.Download(null, null);
                    int tempDownloadedBytes;
                    int tempTotalBytes;

                    tempVar.AttachedFile.GetDownloadProgress(out tempDownloadedBytes, out tempTotalBytes);
                    Debug.Log(tempDownloadedBytes + " " + tempTotalBytes);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (var tempVar in testLeaderboard.Results)
            {
                if (tempVar.Name == "Silverhsu")
                {
                    //tempVar.AttachedFile.dow
                    //outputFileString = tempVar.AttachedFile.ReadAllText(null);
                    //Debug.Log("file:" + outputFileString);
                    outputFileString = tempVar.AttachedFile.FileName;
                    Debug.Log(tempVar.AttachedFile.FileName + "  " + tempVar.AttachedFile.SizeInBytes + " " + tempVar.AttachedFile.SharingId);
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

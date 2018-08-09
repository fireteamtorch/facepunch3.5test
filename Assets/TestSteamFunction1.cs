using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class TestSteamFunction1 : MonoBehaviour {

    public float timer = 0;
    public float threshold = 4;
    public bool hasClientStarted = false;

    public Facepunch.Steamworks.Leaderboard testLeaderboard;

    public string outputFileString;

    public static string playerName = "jeff";
    private static char fileSep = Path.DirectorySeparatorChar;
    private static string saveFilePath = Application.persistentDataPath + fileSep + "save";
    private static string saveFileName = "savefile_" + playerName + ".dat";

    // Use this for initialization
    void Start () {
        // SteamManager.StartClient();
        Facepunch.Steamworks.Client.Instance.Lobby.OnLobbyJoined += onLobbyJoinSuccessCallback;
        Facepunch.Steamworks.Client.Instance.Achievements.OnUpdated += onAchievementSuccessCallback;
        Facepunch.Steamworks.Client.Instance.Achievements.OnAchievementStateChanged += onAchievementStateCallback;
    }
	
	// Update is called once per frame
	void Update () {
        // SteamManager.UpdateClient();

        if (testLeaderboard == null) {
            Debug.Log("Fetching leaderboard...");
            testLeaderboard = Facepunch.Steamworks.Client.Instance.GetLeaderboard("TestLeaderboard", Facepunch.Steamworks.Client.LeaderboardSortMethod.Ascending, Facepunch.Steamworks.Client.LeaderboardDisplayType.Numeric);
            //testLeaderboard.FetchScores(Facepunch.Steamworks.Leaderboard.RequestType.Global, 0, 100);
            //Debug.Log(testLeaderboard.Name + ", total entries:" + testLeaderboard.TotalEntries);
        }

        // Add the shit
        if (Input.GetKeyDown(KeyCode.J))
        {

            Debug.Log("Adding score...");

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
            //testLeaderboard = Facepunch.Steamworks.Client.Instance.GetLeaderboard("TestLeaderboard", Facepunch.Steamworks.Client.LeaderboardSortMethod.Ascending, Facepunch.Steamworks.Client.LeaderboardDisplayType.Numeric);
            //testLeaderboard.FetchScores(Facepunch.Steamworks.Leaderboard.RequestType.Global, 0, 100);
            Debug.Log("Attempting to fetch leaderboard results...");
            if (testLeaderboard.Results != null)
            {
                Debug.Log(testLeaderboard.Name + ", total entries:" + testLeaderboard.TotalEntries);
                foreach (var tempVar in testLeaderboard.Results)
                {
                    Debug.Log("rank: " + tempVar.GlobalRank + "\tname: " + tempVar.Name + "\tscore: " + tempVar.Score + "\tID: " + tempVar.SteamId);
                    if(tempVar.AttachedFile != null)
                    {
                        Debug.Log("Attached fileame: " + tempVar.AttachedFile.FileName);
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
            } else
            {
                Debug.Log("Leaderboard is null.");
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
            Debug.Log("Fetching leaderboard number of entries...");
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

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Attempting overlay");
            Facepunch.Steamworks.Client.Instance.Overlay.OpenProfile(76561198005817917);
          
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Attempting screenshot");
            Facepunch.Steamworks.Client.Instance.Screenshots.Trigger();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Fetching achievements...");
            Facepunch.Steamworks.Achievement[] allAchs = Facepunch.Steamworks.Client.Instance.Achievements.All;
            foreach (Facepunch.Steamworks.Achievement currAch in allAchs)
            {
                Debug.Log("Achievent name: " + currAch.Name + ", description = " + currAch.Description + ", global percent = " + currAch.GlobalUnlockedPercentage);
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Fetching achievements...");
            Facepunch.Steamworks.Achievement[] allAchs = Facepunch.Steamworks.Client.Instance.Achievements.All;
            foreach (Facepunch.Steamworks.Achievement currAch in allAchs)
            {
                if (currAch.Name == "Interstellar")
                {
                    Debug.Log("Adding achievement Interstellar");
                    currAch.Trigger();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Fetching achievements...");
            Facepunch.Steamworks.Achievement[] allAchs = Facepunch.Steamworks.Client.Instance.Achievements.All;
            foreach (Facepunch.Steamworks.Achievement currAch in allAchs)
            {
                if (currAch.Name == "Interstellar")
                {
                    Debug.Log("Resetting achievement Interstellar");
                    currAch.Reset();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Fetching remote storage quota...");
            ulong used = Facepunch.Steamworks.Client.Instance.RemoteStorage.QuotaUsed;
            ulong total = Facepunch.Steamworks.Client.Instance.RemoteStorage.QuotaTotal;
            ulong available = Facepunch.Steamworks.Client.Instance.RemoteStorage.QuotaRemaining;

            bool isEnabledForAccount = Facepunch.Steamworks.Client.Instance.RemoteStorage.IsCloudEnabledForAccount;
            bool isEnabledForApp = Facepunch.Steamworks.Client.Instance.RemoteStorage.IsCloudEnabledForApp;

            Debug.Log("Steam cloud is enabled for account: " + isEnabledForAccount);
            Debug.Log("Steam cloud is enabled for app: " + isEnabledForApp);

            Debug.Log("Total quota: " + total + " bytes");
            Debug.Log("Used: " + used + " bytes");
            Debug.Log("Available: " + available + " bytes");
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            string fullFileName = saveFilePath + fileSep + saveFileName;
            byte[] data = readBytesFromLocalFile(fullFileName);
            Debug.Log("Reading bytes in from local file. Length = " + data.Length);

            bool success = Facepunch.Steamworks.Client.Instance.RemoteStorage.WriteBytes(saveFileName, data);
            Debug.Log("Writing bytes to remote file. Success = " + success);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            byte[] data = Facepunch.Steamworks.Client.Instance.RemoteStorage.ReadBytes(saveFileName);
            Debug.Log("Reading bytes in from remote file. Length = " + data.Length);

            string fullFileName = saveFilePath + fileSep + saveFileName;
            writeBytesToLocalFile(fullFileName, data);
        }

    }

    public void onAddSuccess(bool i)
    {
        Debug.Log("Added score successfully");
    }

    public void onAddFailure(bool i)
    {
        Debug.Log("Score add unsuccessful");
    }

    void onLobbyJoinSuccessCallback(bool success)
    {
        Debug.Log("Lobby joined successfully.");
    }

    void onAchievementSuccessCallback()
    {
        Debug.Log("Achievement added successfully.");
    }

    void onAchievementStateCallback(Facepunch.Steamworks.Achievement input)
    {
        Debug.Log("Achievement " + input.Name + " changed successfully.");
    }

    public byte[] readBytesFromLocalFile(string filePath)
    {
        byte[] bytesRead = File.ReadAllBytes(filePath);
        return bytesRead;
    }

    public void writeBytesToLocalFile(string filePath, byte[] data)
    {
        File.WriteAllBytes(filePath, data);
    }

}

using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using Facepunch.Steamworks;
using System.Text;

public class SteamTest : MonoBehaviour
{
    private ServerList.Request serverRequest;
    private Leaderboard leaderBoard;

    public SteamAvatar YourAvatar;
    public SteamAvatar[] RandomAvatar;

    IEnumerator Start()
	{
        //
        // Wait for client to start up
        //
        while ( Client.Instance == null )
            yield return null;


        Client.Instance.OnAnyCallback += DebugPrintSteamCallback;

        YourAvatar.Fetch( Client.Instance.SteamId );

        //
        // Request a list of servers
        //
        {
	        serverRequest = Client.Instance.ServerList.Internet();
	    }

        //
        // Request a leaderboard
        //
	    {
            leaderBoard = Client.Instance.GetLeaderboard( "TestLeaderboard", Client.LeaderboardSortMethod.Ascending, Client.LeaderboardDisplayType.Numeric );
        }

        //
        // Chang to random avatar every 20 seconds, to test callbacks
        //
        while ( true )
        {
            ulong steamid = 76561197960279927 + (ulong)UnityEngine.Random.Range( 0, 100000 );

            foreach ( var a in RandomAvatar )
            {
                
                a.Fetch( steamid );
            }

            yield return new WaitForSeconds( 11 );
        }
	}

    private void DebugPrintSteamCallback( object obj )
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine( "<color=#88dd88>" + obj.GetType().Name + "</color>" );

        foreach ( var f in obj.GetType().GetFields( System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance ) )
        {
            sb.AppendLine( "  <color=#aaaaaa>" + f.Name + ":</color>\t <color=#fffff>" + f.GetValue( obj ).ToString() + "</color>" );
        }

        Debug.Log( sb.ToString() );
    }

    private void OnDestroy()
    {
        if ( Client.Instance != null )
        {
            Client.Instance.Dispose();
        }
    }

    void OnGUI()
    {
        GUILayout.BeginArea( new Rect( 16, 16, Screen.width - 32, Screen.height - 32 ) );

        if ( Client.Instance  == null )
        {
            GUILayout.Label( "SteamClient not loaded.." );
            GUILayout.EndArea();
            return;
        }



        GUILayout.Label( "SteamId: " + Client.Instance.SteamId );
        GUILayout.Label( "Username: " + Client.Instance.Username );

        GUILayout.Label( "Friend Count: " + Client.Instance.Friends.AllFriends.Count() );
        GUILayout.Label( "Online Friend Count: " + Client.Instance.Friends.AllFriends.Count( x => x.IsOnline ) );

        if ( Client.Instance.Inventory.Definitions != null )
            GUILayout.Label( "Item Definitions: " + Client.Instance.Inventory.Definitions.Length );

        if ( Client.Instance.Inventory.Items != null )
            GUILayout.Label( "Item Count: " + Client.Instance.Inventory.Items.Length );

        if ( serverRequest != null )
        {
            GUILayout.Label( "Server List: " + (serverRequest.Finished ? "Finished" : "Querying") );
            GUILayout.Label( "Servers Responded: " + serverRequest.Responded.Count );
            GUILayout.Label( "Servers Unresponsive: " + serverRequest.Unresponsive.Count );

            if ( serverRequest.Responded.Count > 0 )
            {
                GUILayout.Label( "Last Server: " + serverRequest.Responded.Last().Name );
            }
        }

        if ( leaderBoard != null )
        {
            GUILayout.Label( "leaderBoard.IsValid: " + leaderBoard.IsValid );
            GUILayout.Label( "leaderBoard.IsError: " + leaderBoard.IsError );

            if ( GUILayout.Button( "Refresh Leaderboard" ) )
            {
                leaderBoard.FetchScores( Leaderboard.RequestType.Global, 0, 100 );

                leaderBoard.AddScore( true, 456, 1, 2, 3, 4, 5, 6 );
            }

            if ( leaderBoard.IsQuerying )
            {
                GUILayout.Label( "QUERYING.." ); 
            }
            else if ( leaderBoard.Results != null )
            {
                foreach ( var result in leaderBoard.Results )
                {
                    GUILayout.Label( string.Format( "{0}. {1} ({2})", result.GlobalRank, result.Name, result.Score ) );
                }
            }
            else
            {
                GUILayout.Label( "No Leaderboard results" );
            }
        }


        GUILayout.EndArea();
    }

    void Update()
    {
        if ( Client.Instance != null )
        {
            Client.Instance.Update();
        }
    }

}

using UnityEngine;
using Facepunch.Steamworks;
using UnityEngine.UI;

//
// To change at runtime call Fetch( steamid )
//
public class SteamAvatar : MonoBehaviour
{
    public ulong SteamId;
    public Friends.AvatarSize Size;
    public Texture FallbackTexture;
    public bool isFetched;
    public bool isTextureApplied;

    public Facepunch.Steamworks.Image thisAvatarImage;

    public float startTimestamp;
    public float endTimestamp;

    public TextMesh nameTxtMesh;
    private string steamUsername;

    public bool hasNoPicture;
    public bool isStillLoading;

    //steam avatar image with an image id of 3 is a ?
    //id of -1 is loading?

    public bool hasDoubleFetched = false;

    public float fetchDelayTimer;
    private float fetchDelayThreshold = 1f;

    void Start()
    {
        //if ( SteamId > 0 )
        //Fetch( SteamId );
        SteamId += (uint)Random.Range(1, 100000);
    }

    void Update()
    {
        fetchDelayTimer += Time.deltaTime;

        if(SteamManager.client != null && !isFetched && SteamId > 0)
        {
            Fetch(SteamId);
            isFetched = true;
        }

        if (!isTextureApplied && isFetched)
        {
            //applyImage();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Fetch(SteamId);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            applyImage();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            CustomGetName();
        }

        if (!hasDoubleFetched && fetchDelayTimer > fetchDelayThreshold)
        {
            Fetch(SteamId);
            hasDoubleFetched = true;
            fetchDelayTimer = 0f;
        }
        if (hasDoubleFetched && isStillLoading)
            {
                Fetch(SteamId);
            }
    }

    public void Fetch( ulong steamid )
    {
        if (steamid == 0)
        {
            Debug.Log("STEAM ID IS ZERO");
            return;
        }

        if (Client.Instance == null)
        {
            Debug.Log("Client is null, using Fallback texture");
            ApplyTexture(FallbackTexture);
            return;
        }
        startTimestamp = Time.time;
        Debug.Log("Start time:" + startTimestamp);
        SteamId = steamid;
        Debug.Log("Fetching Steam ID: " + steamid);
        Client.Instance.Friends.GetAvatar(Size, SteamId, ( i ) => OnImage( i, steamid ));
        CustomGetName();
        
    }

    private void CustomGetName()
    {
        string tempString = SteamManager.client.Friends.GetName(SteamId);
        Debug.Log("tempString received: " + tempString);
        if (tempString != "[unknown]")
        {
            steamUsername = tempString;
        }else
        {
            Debug.Log("did not get a name");
        }
        nameTxtMesh.text = steamUsername;
    }

    private void OnImage( Facepunch.Steamworks.Image image, ulong steamid )
    {
        endTimestamp = Time.time;
        Debug.Log("End time:" + endTimestamp);
        Debug.Log("IMAGE HASHCODE: " +  image.GetHashCode() +" id: " + image.Id);

        if(image.Id == 3)
        {
            hasNoPicture = true;
        }else
        {
            hasNoPicture = false;
        }
        if(image.Id == -1)
        {
            isStillLoading = true;
        }else
        {
            isStillLoading = false;
        }

        if (steamid != SteamId)
        {
            Debug.Log("STEAM ID MISMATCH");
            return;
        }

        if ( image == null )
        {
            Debug.Log("Applied fallback");
            isTextureApplied = false;
            ApplyTexture(FallbackTexture);
            return;
        }


        Debug.Log("SHIT WORKS");

        thisAvatarImage = image;

        applyImage();
        /*
        isTextureApplied = true;

        var texture = new Texture2D(image.Width, image.Height);

        for (int x = 0; x < image.Width; x++)
            for (int y = 0; y < image.Height; y++)
            {
                var p = image.GetPixel(x, y);

                texture.SetPixel(x, image.Height - y, new UnityEngine.Color( p.r / 255.0f, p.g / 255.0f, p.b / 255.0f, p.a / 255.0f ) );
            }

        texture.Apply();
        
        ApplyTexture(texture);
        */
    }

    private void applyImage()
    {
        isTextureApplied = true;

        var texture = new Texture2D(thisAvatarImage.Width, thisAvatarImage.Height);

        for (int x = 0; x < thisAvatarImage.Width; x++)
            for (int y = 0; y < thisAvatarImage.Height; y++)
            {
                var p = thisAvatarImage.GetPixel(x, y);

                texture.SetPixel(x, thisAvatarImage.Height - y, new UnityEngine.Color(p.r / 255.0f, p.g / 255.0f, p.b / 255.0f, p.a / 255.0f));
            }


        texture.Apply();
        ApplyTexture(texture);
    }

    private void ApplyTexture(Texture texture)
    {
        //var rawImage = GetComponent<RawImage>();
        var rawImage = GetComponent<MeshRenderer>();
        if (rawImage != null)
        {
            //rawImage.texture = texture;
            rawImage.material.mainTexture = texture;
        }
        nameTxtMesh.text = SteamManager.client.Friends.GetName(SteamId);
    }
}

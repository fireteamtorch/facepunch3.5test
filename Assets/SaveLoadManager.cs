using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public static class SaveLoadManager {

    public static string name = "jeff";

    private static char fileSep = Path.DirectorySeparatorChar;
    private static string saveFilePath = Application.persistentDataPath + fileSep + "save";
    private static string saveFileName = "savefile_" + name + ".dat";

    // Data to save
    public static BandInfo band = null;
    public static string[] setList = new string[] {"wtf", "bbq"};
    public static List<float> reviews = new List<float>();

    public static void saveAllData()
    {
        Debug.Log("Saving to " + saveFilePath + fileSep + saveFileName + "...");

        if (!Directory.Exists(saveFilePath))
        {
            Directory.CreateDirectory(saveFilePath);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(saveFilePath + fileSep + saveFileName);

        SaveData data = new SaveData();
        data.band = band;
        data.setList = setList;
        data.reviews = reviews;

        bf.Serialize(file, data);
        file.Close();
    }

    public static void loadAllData()
    {
        Debug.Log("Loading from " + saveFilePath + fileSep + saveFileName + "...");

        if (File.Exists(saveFilePath + fileSep + saveFileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(saveFilePath + fileSep + saveFileName, FileMode.Open);
            SaveData data = (SaveData) bf.Deserialize(file);
            file.Close();

            band = data.band;
            setList = data.setList;
            reviews = data.reviews;
        } else
        {
            Debug.Log("No file exists of that name.");
        }
    }

    public static void resetVariables()
    {
        band = null;
        setList = new string[] { "wtf", "bbq" };
        reviews = new List<float>();
    }

    public static void printVariables()
    {
        if (band == null)
        {
            Debug.Log("Band: null");
        } else
        {
            Debug.Log("Band: " + band.ToString());
        }
        foreach(string currSong in setList)
        {
            Debug.Log("Set list contains: " + currSong);
        }
        if (reviews.Count == 0)
        {
            Debug.Log("Reviews are empty.");
        } else
        {
            foreach (float currReview in reviews)
            {
                Debug.Log("Reviews contains score: " + currReview);
            }
        }
    }

    public static void setName(string newName)
    {
        name = newName;
        saveFileName = "savefile_" + name + ".dat";
        Debug.Log("Name is now " + name);
    }

}

[Serializable]
public class SaveData
{

    public BandInfo band;
    public string[] setList;
    public List<float> reviews;

    public SaveData()
    {
        
    }

}

[Serializable]
public enum EInstrumentType { Guitar, Bass, Drums, Piano, Vocals}

[Serializable]
public class BandInfo
{

    public string name;
    public int rank;
    public EInstrumentType[] instruments;

    public BandInfo(string name, int rank, EInstrumentType[] instruments)
    {
        this.name = name;
        this.rank = rank;
        this.instruments = instruments;
    }

    public override string ToString()
    {
        string toReturn = "";
        toReturn += "name: ";
        toReturn += name;
        toReturn += ", rank: ";
        toReturn += rank;
        toReturn += ", instruments: ";
        foreach (EInstrumentType currInst in instruments)
        {
            toReturn += currInst;
            toReturn += ", ";
        }
        return toReturn;
    }

}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestSaveLoadScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
        if (Input.GetKeyDown(KeyCode.R))
        {
            SaveLoadManager.resetVariables();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveLoadManager.printVariables();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            BandInfo newBand = new BandInfo("CollaboDaisakusen", 100, new EInstrumentType[] { EInstrumentType.Guitar, EInstrumentType.Guitar, EInstrumentType.Bass, EInstrumentType.Drums, EInstrumentType.Piano, EInstrumentType.Vocals, EInstrumentType.Vocals });
            string[] newSetList = new string[] {"Ambiguous", "Startear", "Button", "Golden Life" };
            List<float> newReviews = new List<float>();
            newReviews.Add(Random.Range(0f, 1f));
            newReviews.Add(Random.Range(0f, 1f));
            newReviews.Add(Random.Range(0f, 1f));
            SaveLoadManager.band = newBand;
            SaveLoadManager.setList = newSetList;
            SaveLoadManager.reviews = newReviews;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveLoadManager.saveAllData();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveLoadManager.loadAllData();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (SaveLoadManager.name == "jeff")
            {
                SaveLoadManager.setName("david");
            } else if (SaveLoadManager.name == "david")
            {
                SaveLoadManager.setName("yi");
            } else if (SaveLoadManager.name == "yi")
            {
                SaveLoadManager.setName("jeff");
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveHandler : MonoBehaviour
{
    FPSController player;

    string path = "playerData.json";

    void Start()
    {
        player = FindObjectOfType<FPSController>();
    }

    [ContextMenu("Save Player Data")]
    public void Save()
    {
        SaveData sd = new SaveData();
        sd.position = player.transform.position;
        sd.ammo = player.CurrentAmmo;
        sd.health = player.Health;

        string jsonText = JsonUtility.ToJson(sd);
        Debug.Log("Name: " + player.name + ", " + "Saving: " + jsonText);
        File.WriteAllText(path, jsonText);
    }

    [ContextMenu("Load Player Data")]
    public void Load()
    {
        try
        {
            string jsonText = File.ReadAllText(path);
            Debug.Log("Name: " + player.name + ", " + "Loading: " + jsonText);
            SaveData sd = JsonUtility.FromJson<SaveData>(jsonText);
            player.transform.position = sd.position;
            player.CurrentAmmo = sd.ammo;
            player.Health = sd.health;
        }
        catch (System.IO.FileNotFoundException e)
        {
            Debug.Log("Error: Can't find file: " + e.FileName);
        }
    }
}

public class SaveData
{
    public Vector3 position;
    public int ammo;
    public int health;
}
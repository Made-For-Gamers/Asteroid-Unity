using UnityEngine;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Singleton manager to manage static data and methods
/// </summary>
public class Singleton : MonoBehaviour
{
    public static Singleton Instance;

    [Header("Save Game Settings")]
    [SerializeField] private string fileName;  

    [Header("Characters")]
    public SO_Player player;
    public SO_Player npc;

    [HideInInspector] public SaveData saveData;  
    private FileSaveHandler fileSaveHandler;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        this.fileSaveHandler = new FileSaveHandler(Application.persistentDataPath, fileName);     
        LoadGame();
        NewArena();
    }

    public void SaveGame()
    {
        print("Saving game data");       
        fileSaveHandler.Save(saveData);
    }

    public void LoadGame()
    {        
        print("Loading game data");
        saveData = fileSaveHandler.Load();

        if (saveData == null)
        {           
            NewGame();
        }

        //Update player data with loaded values
        player.Score = saveData.score;
        player.Xp = saveData.xp;
        player.ShieldLvl = saveData.shieldLvl;
        player.BulletLvl = saveData.bulletLvl;
        player.Iterium = saveData.iterium;
    }

    public void NewGame()
    {
        print("Loading game defaults");
        saveData = new SaveData();
    }

    public void NewArena()
    {
        player.Health = 100;
    }

    private void OnApplicationQuit()
    {       
        SaveGame();     
    }

}

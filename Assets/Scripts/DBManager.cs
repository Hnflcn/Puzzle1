using System;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DBManager : MonoBehaviour
{
    private DatabaseReference levelDB;
    private readonly string firebaseDburl = "https://hl-bigger-default-rtdb.firebaseio.com/";
    public string lvlName;
    private async void Start()
    {
        await Initialization();
    }
    private async Task Initialization()
    {
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
        Debug.Log(dependencyStatus == DependencyStatus.Available ? "Firebase initialized successfully" : "Could not resolve all Firebase dependencies: " + dependencyStatus);
    }

    private string levelName;

    public async void GetLevelInformation()
    {
        try
        {
            levelDB = FirebaseDatabase.GetInstance(firebaseDburl).GetReference(lvlName);
            var snapshot = await levelDB.GetValueAsync();

            foreach (var level in snapshot.Children)
            {
                levelName = level.Key;
                // SceneManager.LoadScene(levelName);
                Debug.Log(level.Key + " : " + level.Value);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error getting level data: " + e.Message);
        }
    }

}
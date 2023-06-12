using System;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DBManager : MonoBehaviour
{
    private DatabaseReference levelDB;
    private string FirebaseDBURL = "https://hl-bigger-default-rtdb.firebaseio.com/";
    public string lvlName;
    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                Debug.Log("Firebase initialized successfully");
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private string levelName;

    public void GetLevelInformation()
    {
        levelDB = FirebaseDatabase.GetInstance(FirebaseDBURL).GetReference(lvlName);
        levelDB.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error getting level data");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (var level in snapshot.Children)
                {
                    levelName = level.Key;
                 //   SceneManager.LoadScene(levelName);
                    Debug.Log(level.Key + " : " + level.Value);
                }
            }
        });
    }

}
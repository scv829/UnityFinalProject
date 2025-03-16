using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// DB에서 데이터 로딩 및 관리하는 매니저
/// </summary>
public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private FirebaseApp app;
    public static FirebaseApp App => Instance.app;

    private FirebaseDatabase database;
    public static FirebaseDatabase Database => Instance.database;

    [Header("플레이어 ID")]
    [SerializeField] int id;
    [Header("플레이어 선택")]
    [SerializeField] List<int> playerPick;

    private Dictionary<int, CharacterData> casheData = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                app = FirebaseApp.DefaultInstance;
                database = FirebaseDatabase.DefaultInstance;

                Debug.Log("Firebase dependencies check success");
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {task.Result}");

                app = null;
                database = null;
            }
        });
    }

    public void GetPlayerCharacterData()
    {
        List<(int, CharacterData)> playerCharacterList = new();

        foreach(int id in playerPick)
        {
            // 이미 캐쉬가 되어있는 캐릭터라면
            if(casheData.TryGetValue(id, out CharacterData data))
            {
                // 데이터를 가져오기
                playerCharacterList.Add((id, data));
            }
            // 처음 들어오는 캐릭터라면
            else
            {
                // 서버에서 데이터 가져오기
                GetPlayerCharacterDataInServer(ref playerCharacterList, id);
            }
        }
    }

    private void GetPlayerCharacterDataInServer(ref List<(int, CharacterData)> playerCharacterList, int index)
    {
        DatabaseReference data = database.RootReference.Child($"User/{id}/Characters/{index}");

        // 캐릭터 ID로 기본 데이터 불러오기
        // 스텟에서 캐릭터 ID에 맞는 데이터 불러오기
        // 캐릭터 ID의 데이터를 저장 및 레벨에 따른 스텟 증가량을 더해준다.
        // 해당 결과를 CharacterData에 저장하고 casheData에 넣는다.

        // 플레이어가 가지고 있는 캐릭터 불러오기
        data.OrderByValue().GetValueAsync().ContinueWithOnMainThread(task => 
        {
            if (task.IsFaulted)
            {
                Debug.Log("데이터 불러오기 실패!: " + task.Exception);
                return;
            }

            // 캐릭터 ID와 레벨을 가져옴
            DataSnapshot snapshot = task.Result;

            foreach (DataSnapshot child in snapshot.Children)
            {
                Debug.Log($"{child.Key} : {child.Value}");
            }

            CharacterData characterData = new();


        });
    }

}

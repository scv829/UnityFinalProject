using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UnityEditor;
using UnityEditor.Search;
using UnityEditor.U2D.Animation;
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

    static string ipAddress = "127.0.0.1";
    static string db_id = "root";
    static string db_pw = "wjsansrk";
    static string db_name = "game_database";

    private MySqlConnection SqlConn;
    string strConn = string.Format("server={0};uid={1};pwd={2};database={3};charset=utf8 ;", ipAddress, db_id, db_pw, db_name);

    [Header("플레이어 ID")]
    [SerializeField] int id;
    [Header("플레이어 선택")]
    [SerializeField] List<int> playerPick;

    private Dictionary<int, CharacterData> casheData = new();

    private StringBuilder sb;

    private void Awake()
    {
        sb = new StringBuilder();

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

        // 플레이어가 선택한 캐릭터 -> 
        // 들어가는 겂이 플레이어가 소지하고 있을 때의 ID인지, 캐릭터 고유 Id 기준인지
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
        // 캐릭터 ID로 기본 데이터 불러오기
        // 스텟에서 캐릭터 ID에 맞는 데이터 불러오기
        // 캐릭터 ID의 데이터를 저장 및 레벨에 따른 스텟 증가량을 더해준다.
        // 해당 결과를 CharacterData에 저장하고 casheData에 넣는다.

        // 플레이어가 가지고 있는 캐릭터 불러오기
        try
        {
            sb.Clear();
            sb.Append("select c.id, c.name, c.rarity, c.position,")
              .Append("bs.ATK AS base_ATK, bs.Hp AS base_Hp, bs.Def AS base_Def, bs.ATKSpeed AS base_AS, bs.ATKRange AS base_ATKRange, bs.MoveSpeed AS base_MoveSpeed,")
              .Append("gs.Hp AS grow_Hp, gs.Def AS grow_Def, gs.ATK AS grow_ATK, gs.Rarity AS grow_Rarity")
              .Append(" from characters c")
              .Append(" Join basestat bs on c.basestatId = bs.id")
              .Append(" Join growstat gs on c.growstatId = gs.id")
              .Append(" where c.id = @id");

            using (SqlConn = new(strConn))
            {
                SqlConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = SqlConn;
                cmd.CommandText = sb.ToString();

                cmd.Parameters.AddWithValue("@id", index);

                MySqlDataReader rdr = cmd.ExecuteReader();


                // 데이터를 담을 인스턴스 생성
                CharacterData characterData = new();

                while (rdr.Read()) 
                {
                    sb.Clear();

                    sb.AppendLine($"캐릭터 데이터 id: {rdr["id"]} , name:  {rdr["name"]}, rarity: {rdr["rarity"]}, position: {rdr["position"]}");
                    sb.AppendLine($"캐릭터 기본 스탯 base_ATK: {rdr["base_ATK"]}, base_Hp:  {rdr["base_Hp"]}, base_Def: {rdr["base_Def"]},  base_AS: {rdr["base_AS"]}, base_ATKRange: {rdr["base_ATKRange"]}, base_MoveSpeed: {rdr["base_MoveSpeed"]}");
                    sb.AppendLine($"캐릭터 스탯 성장률 grow_hp: {rdr["grow_Hp"]}, grow_Def:  {rdr["grow_Def"]}, grow_ATK: {rdr["grow_ATK"]},  grow_Rarity: {rdr["grow_Rarity"]}");
                
                    Debug.Log(sb.ToString());
                }
                rdr.Close();

                casheData.Add(index, characterData);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
        }
        finally
        {
            SqlConn.Close();
        }
    }

}

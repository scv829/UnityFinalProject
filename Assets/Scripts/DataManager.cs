using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// DB에서 데이터 로딩 및 관리하는 매니저
/// </summary>
public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

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
    }

    public List<(int, CharacterSO)> GetPlayerCharacterData()
    {
        // TODO: DB에서 캐릭터 데이터 가져오기
        // GetPlayerCharacterDataInServer 사용하여 가져오기
        List<(int, CharacterSO)> result = new();

        foreach (int id in playerPick)
        {
            result.Add((id, null));
        }

        return result;
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

    // 현 스테이지에 대한 정보를 호출
    public List<Round> GetStageData(int stageId)
    {
        // TODO: DB에서 라운드와 등장하는 몬스터 불러오기
        List<Round> result = new();

        Round r1 = new()
        {
            enemyList = new()
            {   1   }
        };

        Round r2 = new()
        {
            enemyList = new()
            {   1 , 1  }
        };

        Round r3 = new()
        {
            enemyList = new()
            {   1, 1 , 1  }
        };

        result.Add(r1);
        result.Add(r2);
        result.Add(r3);

        return result;
    }

}

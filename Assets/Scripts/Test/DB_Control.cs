using MySql.Data.MySqlClient;
using System;
using System.Data;
using UnityEngine;

public class DB_Control : MonoBehaviour
{
    public MySqlConnection SqlConn;

    private void Awake()
    {
        Debug.Log($"Connection Test: {ConnectTest()}");
    }

    private void Start()
    {
        //SelectTest();
        //UpdateTest();
        //DeleteTest();
    }

    private bool ConnectTest()
    {
        string strCon = string.Format("Server={0};Port=3306;Database={1};Uid={2};Pwd={3};", "127.0.0.1", "test", "root", "wjsansrk");

        try
        {
            using (SqlConn = new(strCon))
            {
                SqlConn.Open();
            }
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
            return false;
        }
        finally
        {
            SqlConn.Close();
        }
    }

    private void SelectTest()
    {
        string query = "select * from tb_table";
        string strCon = string.Format("Server={0};Port=3306;Database={1};Uid={2};Pwd={3};", "127.0.0.1", "test", "root", "wjsansrk");

        try
        {
            using (SqlConn = new(strCon))
            {
                SqlConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = SqlConn;
                cmd.CommandText = query;

                MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sd.Fill(ds, "tb_table");

                Debug.Log(ds.GetXml());
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
        }
        finally
        {
             SqlConn.Close();  //DB 연결 해제
        }
    }


    private void UpdateTest()
    {
        string query = "update tb_table set Name =  @name Where ID = @id";

        string strCon = string.Format("Server={0};Port=3306;Database={1};Uid={2};Pwd={3};", "127.0.0.1", "test", "root", "wjsansrk");

        try
        {
            using (SqlConn = new(strCon))
            {
                SqlConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = SqlConn;
                cmd.CommandText = query;

                cmd.Parameters.AddWithValue("@name", "test1");
                cmd.Parameters.AddWithValue("@id", 1);
                
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
        }
        finally
        {
            SqlConn.Close();  //DB 연결 해제
        }
    }

    private void DeleteTest()
    {
        string query = "delete from tb_table Where ID = @id";

        string strCon = string.Format("Server={0};Port=3306;Database={1};Uid={2};Pwd={3};", "127.0.0.1", "test", "root", "wjsansrk");

        try
        {
            using (SqlConn = new(strCon))
            {
                SqlConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = SqlConn;
                cmd.CommandText = query;

                cmd.Parameters.AddWithValue("@id", 1);

                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
        }
        finally
        {
            SqlConn.Close();  //DB 연결 해제
        }
    }
}

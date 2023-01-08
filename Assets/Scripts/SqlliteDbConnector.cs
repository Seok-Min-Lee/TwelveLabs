using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public static class SqlliteDbConnector
{
    public static bool isCreate = false;
    public static IEnumerator DbCreate()
    {
        string dbPath = string.Empty;

        if (Application.platform == RuntimePlatform.Android)
        {
            dbPath = Application.persistentDataPath + "/tldb.db";

            // 기존 파일 제거
            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
            }

            // 파일 복사
            if (!File.Exists(dbPath))
            {
                string originPath = "jar:file://" + Application.dataPath + "!/assets/tldb.db";

                UnityWebRequest unityWebRequest = UnityWebRequest.Get(originPath);
                unityWebRequest.downloadedBytes.ToString();
                yield return unityWebRequest.SendWebRequest().isDone;

                File.WriteAllBytes(dbPath, unityWebRequest.downloadHandler.data);
                isCreate = true;
            }
        }
        else
        {
            dbPath = Application.dataPath + "/tldb.db";

            // 기존 파일 제거
            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
            }

            File.Copy(Application.streamingAssetsPath + "/tldb.db", dbPath);
            isCreate = true;
        }
    }

    private static string GetDbPath()
    {
        string path = string.Empty;

        if (Application.platform == RuntimePlatform.Android)
        {
            path = "URI=file:" + Application.persistentDataPath + "/tldb.db";

        }
        else
        {
            path = "URI=file:" + Application.dataPath + "/tldb.db";
        }

        return path;
    }

    public static DataTable GetSelectResultToDataTable(string query)
    {
        // 연결
        IDbConnection conn = new SqliteConnection(GetDbPath());
        conn.Open();
        IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = query;
        IDataReader dr = cmd.ExecuteReader();

        // 결과를 담을 테이블 생성을 위해 스키마 테이블을 가져온다.
        DataTable dt = dr.GetSchemaTable();
        int columnCount = dt.Rows.Count;

        // 테이블 생성
        DataTable result = new DataTable();
        result.TableName = "TB_SETUP";

        // column 추가.
        for (int i = 0; i < columnCount; i++)
        {
            result.Columns.Add(new DataColumn(columnName: dt.Rows[i][0].ToString()));
        }

        // row 추가.
        object[] row;
        while (dr.Read())
        {
            row = new object[columnCount];

            for(int i = 0; i < columnCount; i++)
            {
                row[i] = dr.GetValue(i);
            }

            result.Rows.Add(row);
        }

        // 연결 해제
        dr.Dispose();
        dr = null;
        cmd.Dispose();
        cmd = null;
        conn.Close();
        conn = null;

        return result;
    }
}

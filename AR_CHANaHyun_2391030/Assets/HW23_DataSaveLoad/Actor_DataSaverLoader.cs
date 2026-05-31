using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;


public class Actor_DataSaverLoader : MonoBehaviour
{
    [Header("Objects To Save")]
    [Tooltip("다이얼 등")]
    public List<GameObject> targetObjects = new List<GameObject>();

    private string GetSavePath()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        return Path.Combine(Application.persistentDataPath, currentSceneName + "_worldData.json");
    }
    private string GetFullPath(GameObject obj)
    {
        string path = obj.name;
        Transform current = obj.transform;

        while (current.parent != null)
        {
            current = current.parent;
            path = current.name + "/" + path;
        }
        return path;
    }

    public void Act_SaveData()
    {
        if (targetObjects.Count == 0) return;

        WorldData data = new WorldData();

        foreach (GameObject obj in targetObjects)
        {
            if (obj == null) continue;

            TransformData t = new TransformData();
            t.objectName = GetFullPath(obj); // 그냥 이름 대신 '전체 경로'를 저장
            t.position = obj.transform.position;
            t.rotation = obj.transform.rotation;

            data.objects.Add(t);
        }

        string json = JsonUtility.ToJson(data, true);
        string path = GetSavePath();
        File.WriteAllText(path, json);

        Debug.Log($"[{SceneManager.GetActiveScene().name}] 저장 완료! 경로: {path}");
    }

    public void Act_LoadData()
    {
        string path = GetSavePath();

        if (!File.Exists(path))
        {
            Debug.Log($"[{SceneManager.GetActiveScene().name}] 세이브 파일이 없어 기본 위치를 유지합니다.");
            return;
        }

        string json = File.ReadAllText(path);
        WorldData data = JsonUtility.FromJson<WorldData>(json);

        if (data == null || data.objects == null) return;

        Dictionary<string, TransformData> savedDataMap = new Dictionary<string, TransformData>();
        foreach (var savedObject in data.objects)
        {
            if (!savedDataMap.ContainsKey(savedObject.objectName))
            {
                savedDataMap.Add(savedObject.objectName, savedObject);
            }
        }

        foreach (GameObject obj in targetObjects)
        {
            if (obj == null) continue;

            string fullPath = GetFullPath(obj); // 로드할 때도 내 '전체 경로'를 만들어 검색

            if (savedDataMap.TryGetValue(fullPath, out TransformData targetData))
            {
                obj.transform.position = targetData.position;
                obj.transform.rotation = targetData.rotation;

                if (obj.TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }

        Debug.Log($"[{SceneManager.GetActiveScene().name}] 로드 완료!");
    }

    public void Act_ResetAndClearData()
    {
        string path = GetSavePath();
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log($"{SceneManager.GetActiveScene().name} 씬의 데이터가 초기화되었습니다.");
        }
    }
}
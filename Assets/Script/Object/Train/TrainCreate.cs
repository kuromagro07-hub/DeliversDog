// 生成位置を制御したい場合は、コントローラークラスで調整する

using UnityEngine;
using System.Collections;

public class TrainCreate : MonoBehaviour
{
    [SerializeField] GameObject trainPrefab;
    Vector3 spawnPos = new Vector3(14.5f, -5.0f, -250f);

    private void Start()
    {
        StartCoroutine(DelayCoroutine());
    }

    void Create()
    {
        Instantiate(trainPrefab, spawnPos, Quaternion.identity);
    }

    // コルーチン本体
    private IEnumerator DelayCoroutine()
    {
        // 3秒間待つ
        yield return new WaitForSeconds(3);

        Create();
    }
}

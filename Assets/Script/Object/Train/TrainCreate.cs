using UnityEngine;
using System.Collections;

public class TrainCreate : MonoBehaviour
{
    [SerializeField] GameObject trainPrefab;

    private void Start()
    {
        StartCoroutine(DelayCoroutine());
    }

    void Create()
    {
        Instantiate(trainPrefab);
    }

    // コルーチン本体
    private IEnumerator DelayCoroutine()
    {
        // 3秒間待つ
        yield return new WaitForSeconds(3);

        Create();
    }
}

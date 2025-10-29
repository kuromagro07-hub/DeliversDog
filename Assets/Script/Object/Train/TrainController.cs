using UnityEngine;
using UnityEngine.Splines;

public class TrainController : MonoBehaviour
{
    [SerializeField] SplineContainer splineContainer;
    public float speed = 5f;
    private float startInterval = 0f; // スプライン上の開始位置(0 ~ 1)
    private float totalLength;
    Vector3 trainStartPosition = new Vector3(0f, 0.8f, 0f);

    void Start()
    {
        splineContainer = GameObject.Find("GeneratedSpline").GetComponent<SplineContainer>();
        totalLength = SplineUtility.CalculateLength(splineContainer.Spline, splineContainer.transform.localToWorldMatrix);
    }

    void Update()
    {
        if (splineContainer == null) return;

        startInterval += speed * Time.deltaTime;
        float t = Mathf.Clamp01(startInterval / totalLength);

        Vector3 pos = SplineUtility.EvaluatePosition(splineContainer.Spline, t);
        Vector3 forward = SplineUtility.EvaluateTangent(splineContainer.Spline, t);
        pos += trainStartPosition;
        Debug.Log($"Train Position: {pos}, t: {t}");
        transform.position = pos;
        transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }


}

using UnityEngine;
using UnityEngine.Splines;

public class TrainController : MonoBehaviour
{
    public SplineContainer splineContainer;
    public float speed = 5f;
    private float distance = 0f;
    private float totalLength;

    void Start()
    {
        splineContainer = GameObject.Find("GeneratedSpline").GetComponent<SplineContainer>();
        totalLength = SplineUtility.CalculateLength(splineContainer.Spline, splineContainer.transform.localToWorldMatrix);

    }

    void Update()
    {
        if (splineContainer == null) return;

        distance += speed * Time.deltaTime;
        float t = Mathf.Clamp01(distance / totalLength);

        Vector3 pos = SplineUtility.EvaluatePosition(splineContainer.Spline, t);
        Vector3 forward = SplineUtility.EvaluateTangent(splineContainer.Spline, t);

        Debug.Log($"Train Position: {pos}, Forward: {forward}");
        transform.position = pos;
        transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }


}

using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;

namespace TrueTrackSystem
{
    /// <summary>
    /// スプラインを自動生成し、そのスプラインに沿って線路を構築するマネージャ
    /// 金属部分PrefabをSplineにアタッチし、
    /// そのSpline上に木材Prefabを等間隔で並べます。
    /// </summary>
    public class RailCreateManager : MonoBehaviour
    {
        [Header("金属レール部分（SplineContainer付きPrefab）")]
        public GameObject metalRailPrefab;

        [Header("木材のPrefab（スリーパー部分）")]
        public GameObject woodRailPrefab;

        [Header("木材を配置する間隔 (m)")]
        public float spacing = 1.0f;

        [Header("生成するスプラインの長さ (m)")]
        public float splineLength = 20f;

        [Header("スプラインの分割数（＝制御点数-1）")]
        public int segments = 5;

        private SplineContainer splineContainer;

        // レールの開始地点
        Vector3 railStartPosition= new Vector3(15f, -2.2f, 0f);

        [ContextMenu("Generate Spline and Rails")]


        public void Generate()
        {
            // すでに生成済みなら削除
            foreach (Transform child in transform)
                DestroyImmediate(child.gameObject);

            CreateSpline();
            PlaceWoodRailsAlongSpline();
        }

        /// <summary>
        /// スプライン（SplineContainer付きオブジェクト）を生成する
        /// </summary>
        private void CreateSpline()
        {
            GameObject splineObj = new GameObject("GeneratedSpline");
            splineObj.transform.SetParent(transform, false);


            splineContainer = splineObj.AddComponent<SplineContainer>();
            Spline spline = new Spline();

            // Z軸方向に伸ばす直線スプラインを構築
            for (int i = 0; i <= segments; i++)
            {
                float z = (splineLength / segments) * i;
                Vector3 pos = new Vector3(0, 0, z);
                BezierKnot knot = new BezierKnot(railStartPosition+pos, Vector3.back * 0.5f, Vector3.forward * 0.5f, Quaternion.identity);
                spline.Add(knot);
            }

            splineContainer.Spline = spline;


            Debug.Log("Spline generated successfully.");
        }

        /// <summary>
        /// スプラインに沿って木材Prefabを配置する
        /// </summary>
        private void PlaceWoodRailsAlongSpline()
        {
            if (splineContainer == null || woodRailPrefab == null)
            {
                Debug.LogError("SplineContainer または WoodPrefab が設定されていません。");
                return;
            }

            var spline = splineContainer.Spline;

            // スプラインの長さをワールド変換を含めて取得
            float totalLength = SplineUtility.CalculateLength(spline, transform.localToWorldMatrix);

            // 階層をきれいにするために空オブジェクトを作成(親)
            GameObject parent = new GameObject("WoodRails");
            parent.transform.SetParent(transform, false);
            parent.transform.localScale = new Vector3(5f,5f,1f);

            // 金属も同様
            GameObject metalparent = new GameObject("MetalRails");
            metalparent.transform.SetParent(transform, false);
            metalparent.transform.localScale = new Vector3(5, 5, 5);


            Vector3 offsetWoodRailPos = new Vector3(0f, 0f, 5f);
            int metalRailCount = 0;

            for (float d = 0; d < totalLength; d += spacing)
            {

                float t = d / totalLength;

                // 現在の位置と次の位置を取得
                Vector3 localPos = SplineUtility.EvaluatePosition(spline, t);
                float nextT = Mathf.Min(t + 0.01f, 1f);
                Vector3 nextPos = SplineUtility.EvaluatePosition(spline, nextT);

                // 進行方向から回転を求める
                Vector3 tangent = (nextPos - localPos).normalized;
                Quaternion localRot = Quaternion.LookRotation(tangent, Vector3.up);

                // ワールド座標へ変換
                Vector3 worldPos = transform.TransformPoint(localPos);

                // 木材プレハブを生成
                GameObject sleeper = Instantiate(woodRailPrefab, worldPos+offsetWoodRailPos, localRot, parent.transform);
                sleeper.name = $"WoodRail_{Mathf.RoundToInt(d)}";


                // (木材レール+間隔) / 2 の位置で配置
                if (d % 4 == 0)
                {
                    float metalOffsetZ = (sleeper.transform.localScale.z + spacing) * 8 / 2;
                    Vector3 metalpos= new Vector3(0f, 0f, metalOffsetZ);
                    GameObject metalRail = Instantiate(metalRailPrefab, worldPos+metalpos, localRot * Quaternion.Euler(0f, -90f, 0f), metalparent.transform);
                    metalRail.name = $"MetalRail_{(metalRailCount)}";
                    metalRailCount++;

                }
            }

            Debug.Log("Wood rails placed along spline successfully.");
        }

    }

}


using System.Collections.Generic;

using UnityEngine;

namespace ExoLabs.MeshTools
{
    public static class Batching
    {
        public static void SetupBatch(Transform transform)
        {
            Debug.Assert(Application.isPlaying, "Batching can only be safely performed in Play Mode.");
            if (!Application.isPlaying ) return; // paranoid much ?  Safety check.

            List<GameObject> list = new();
            ScanForBatch(transform, list);
            StaticBatchingUtility.Combine(list.ToArray(), transform.gameObject);
        }
        static void ScanForBatch(Transform transform, List<GameObject> list)
        {

            foreach (Transform child in transform)
            {
                if (child.TryGetComponent<IBatchingMode>(out var batchingMode))
                {
                    if (batchingMode.IsNoBatching) continue;
                }
                list.Add(child.gameObject);
                ScanForBatch(child, list);
            }
        }
    }


}
using Sirenix.OdinInspector;

using UnityEngine;

namespace ExoLabs.MeshTools
{
    /// <summary>
    /// Mesh batching is only really needed on very complex scenes with many meshes.
    /// </summary>
    public class BatchSection : MonoBehaviour
    {
        [InfoBox("This function can only be safely executed in Play Mode.", InfoMessageType.Warning, "IsEditMode")]
        [Button, EnableIf("IsRuntimeMode")]
        void SetupMeshBatches() => Batching.SetupBatch(transform);

        bool IsRuntimeMode=>Application.isPlaying;
        bool IsEditMode=>!Application.isPlaying;
    }
}
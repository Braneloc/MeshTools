using Sirenix.OdinInspector;

using UnityEngine;

namespace ExoLabs.MeshTools
{
    /// <summary>
    /// Mesh batching is only really needed on very complex scenes with many meshes.
    /// </summary>
    public sealed class MeshBatcher : MonoBehaviour
    {
        [InfoBox("This function can only be safely executed in Play Mode.", InfoMessageType.Info, "IsEditMode")]
        [Button, EnableIf("IsRuntimeMode")]
        void SetupMeshBatches() => Batching.SetupBatch(transform);

        bool IsRuntimeMode => Application.isPlaying;
        bool IsEditMode => !Application.isPlaying;
    }
}
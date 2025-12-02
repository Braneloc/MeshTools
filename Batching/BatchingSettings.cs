using UnityEngine;

namespace ExoLabs.MeshTools
{
    public sealed class BatchingSettings : MonoBehaviour, IBatchingMode
    {
        [SerializeField] private BatchingMode batchingMode = BatchingMode.Dynamic;
        public BatchingMode Mode => batchingMode;
    }
    public enum BatchingMode
    {
        NoBatching,
        Dynamic,
        //Static,
    }
    public interface IBatchingMode
    {
        BatchingMode Mode { get; }
        //bool IsStatic => Mode == BatchingMode.Static;
        bool IsDynamic => Mode == BatchingMode.Dynamic;
        bool IsNoBatching => Mode == BatchingMode.NoBatching;
    }
}
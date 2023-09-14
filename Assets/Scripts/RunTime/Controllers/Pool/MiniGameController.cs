using RunTime.Signal;
using UnityEngine;

namespace RunTime.Controllers.Pool
{
    public class MiniGameController : MonoBehaviour
    {
        [SerializeField] private byte miniGameStateLevel;

        private void OnEnable()
        {
            CoreGameSignals.Instance.onWhichMiniGameAreaEntered += WhichMiniGameAreaEntered;
        }

        private byte WhichMiniGameAreaEntered()
        {
            return miniGameStateLevel;
        }

        private void OnDisable()
        {
            CoreGameSignals.Instance.onWhichMiniGameAreaEntered -= WhichMiniGameAreaEntered;
        }
    }
}

using RunTime.Signal;
using UnityEngine;

namespace RunTime.Commands
{
    public class InputReleasedCommand
    {
        public void Execute()
        {
            InputSignals.Instance.onInputReleased?.Invoke();
            Debug.LogWarning("Player is Released");
        }
    }
}
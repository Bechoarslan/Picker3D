using Signal;
using UnityEngine;

namespace Commands
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
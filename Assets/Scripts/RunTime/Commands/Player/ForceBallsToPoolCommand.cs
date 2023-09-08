using RunTime.Data.ValueObjects;
using RunTime.Managers;

namespace RunTime.Commands.Player
{
    public class ForceBallsToPoolCommand
    {
        private PlayerManager _playerManager;
        private PlayerMeshData _playerMeshData;
        public ForceBallsToPoolCommand(PlayerManager playerManager, PlayerMeshData dataPlayerMeshData)
        {
            _playerManager = playerManager;
            _playerMeshData = dataPlayerMeshData;
        }

        internal void Execute()
        {
            
        }
    }
}
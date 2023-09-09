using System.Linq;
using RunTime.Data.ValueObjects;
using RunTime.Managers;
using UnityEngine;

namespace RunTime.Commands.Player
{
    public class ForceBallsToPoolCommand
    {
        private PlayerManager _playerManager;
        private PlayerForceData _playerForceData;
        private readonly string _collectable = "Collectable";
        public ForceBallsToPoolCommand(PlayerManager playerManager, PlayerForceData dataPlayerForceData)
        {
            _playerManager = playerManager;
            _playerForceData = dataPlayerForceData;
        }

        internal void Execute()
        {
            var transform1 = _playerManager.transform;
            var position1 = transform1.position;
            
            var forcePos = new Vector3(position1.x, position1.y - 1f,  position1.z + .9f);
            var collider = Physics.OverlapSphere(forcePos, 1.7f);

            var collectableColliderList = collider.Where(col => col.CompareTag(_collectable)).ToList();


            foreach (var col in collectableColliderList)
            {
                if(col.GetComponent<Rigidbody>() == null) continue;
                var rb = col.GetComponent<Rigidbody>();
                rb.AddForce(new Vector3(0, _playerForceData.ForceParameters.y, _playerForceData.ForceParameters.z),
                    ForceMode.Impulse);

            }
            
            collectableColliderList.Clear();
        }
    }
}
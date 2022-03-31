using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;

namespace HelloWorld
{
    public class Player : NetworkBehaviour
    {
        
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();


        private void Start() {

        }

        public void OnPositionChange(Vector3 previousValue, Vector3 newValue){
            transform.position = Position.Value;
        }

        public override void OnNetworkSpawn()
        {
             if (IsOwner)
            {
                Move();
            }
        }
        

        public void Move()
        {    
            SubmitPositionRequestServerRpc();
        }

        [ServerRpc]
        void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            Position.Value = GetRandomPositionOnPlane();
        }
        

        static Vector3 GetRandomPositionOnPlane()
        {
           return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
        }


        void Update()
        {
            
        }
    }
}
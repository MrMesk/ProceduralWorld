using UnityEngine;
using System.Collections;

namespace Chunk {

    [RequireComponent(typeof(BoxCollider))]
    public class Chunk : MonoBehaviour {

        public int x;
        public int y;

        ChunkTerrainExample chunkManager;

        GameObject terrain;

        void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                //
            }
        }

    }

}
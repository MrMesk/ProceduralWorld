using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ChunkTerrainExample : MonoBehaviour {
    
    public float size = 1;

    void Start() {
        // Reset the chunk related global matrixes
        Chunk.Chunk.ResetGlobals();
    }

    public void ChunkTriggerCalledAt(int x, int y) {
        // Load next by chunks if not created yet
    }

    public void LoadChunk(int x, int y) {

    }

}

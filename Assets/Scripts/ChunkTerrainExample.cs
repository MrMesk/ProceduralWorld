using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ChunkTerrainExample : MonoBehaviour {
    
    public float scale = 1;

    public GameObject prefabChunk;

    void Start() {
        // Initiate Chunk Library
        Chunk.Chunk.Init(this);

        // _deug_
        Chunk.Chunk.CreateAt(prefabChunk, 0, 0);
    }

    public void ChunkTriggerCalledAt(int x, int y) {
        // Load next by chunks if not created yet
        
        Dictionary<int, Chunk.Chunk> yAxis;
        for (int _x = x - 1; _x <= x + 1; _x++) {
            for (int _y = y - 1; _y <= y + 1; _y++) {
                if (!Chunk.Chunk.globalChunkMatrix.TryGetValue(_x, out yAxis) || !yAxis.ContainsKey(_y)) {
                    // Load chunk if it wasn't loaded until now
                    Chunk.Chunk.CreateAt(prefabChunk, _x, _y);
                }
            }
        }

    }

}

using UnityEngine;
using System.Collections.Generic;

public class InfiniteBackground : MonoBehaviour
{
    public GameObject pozadi;
    public Camera cam;
    private float sirka_pozadi;
    private float vyska_pozadi;

    private Dictionary<Vector2Int, GameObject> vytvorene_pozadi = new Dictionary<Vector2Int, GameObject>();
    private HashSet<Vector2Int> existujici_pozadi = new HashSet<Vector2Int>();

    void Start()
    {
        SpriteRenderer sr = pozadi.GetComponent<SpriteRenderer>();
        sirka_pozadi = sr.bounds.size.x;
        vyska_pozadi = sr.bounds.size.y;
    }

    void Update()
    {
        Vector3 camPos = cam.transform.position;
        float camWidth = 2f * cam.orthographicSize * cam.aspect;
        float camHeight = 2f * cam.orthographicSize;

        int minX = Mathf.FloorToInt((camPos.x - camWidth / 2) / sirka_pozadi);
        int maxX = Mathf.FloorToInt((camPos.x + camWidth / 2) / sirka_pozadi) + 1;
        int minY = Mathf.FloorToInt((camPos.y - camHeight / 2) / vyska_pozadi);
        int maxY = Mathf.FloorToInt((camPos.y + camHeight / 2) / vyska_pozadi) + 1;

        existujici_pozadi.Clear();

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                Vector2Int tileCoord = new Vector2Int(x, y);
                existujici_pozadi.Add(tileCoord);

                if (!vytvorene_pozadi.ContainsKey(tileCoord))
                {
                    Vector3 pos = new Vector3(x * sirka_pozadi, y * vyska_pozadi, 0);
                    GameObject tile = Instantiate(pozadi, pos, Quaternion.identity, transform);
                    vytvorene_pozadi.Add(tileCoord, tile);
                }
            }
        }

        // smaž dlaždice, které už nejsou potøeba
        List<Vector2Int> toRemove = new List<Vector2Int>();
        foreach (var pair in vytvorene_pozadi)
        {
            if (!existujici_pozadi.Contains(pair.Key))
            {
                Destroy(pair.Value);
                toRemove.Add(pair.Key);
            }
        }

        foreach (var coord in toRemove)
        {
            vytvorene_pozadi.Remove(coord);
        }
    }
}

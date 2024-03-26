using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoMapGenerate : MonoBehaviour
{
    [SerializeField] bool _initializeMap = true;
    [SerializeField] bool _roundYPos = false;
    [SerializeField] GameObject _cube;
    [SerializeField] Vector2 _mapSize = new Vector2(50, 50);
    [SerializeField] float _worldYLimid = 6; 
    [SerializeField] float _relief = 15f;
    [SerializeField] Text _inputBar;
    int _x = 0, _z = 0;
    public void GenerateMap()
    {
        if(_initializeMap)
        {
            InitializeMap();
        }
        Debug.Log("Start Generate");
        float seedX = Random.value * 100;
        float seedZ = Random.value * 100;

        float _createBoxes = _mapSize.x * _mapSize.y;
        if(_inputBar.text == string.Empty)
        {
            Debug.Log("Generate Map By Random");
            for (int x = 0; x < _mapSize.x; x++)
            {
                for (int z = 0; z < _mapSize.y; z++)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    float xSample = (x + seedX)/ _relief;
                    float zSample = (z + seedZ)/ _relief;
                    float noise = Mathf.PerlinNoise(xSample, zSample);

                    float posY = _worldYLimid * noise;
                    if (_roundYPos)
                    {
                        posY = Mathf.Round(posY);
                    }

                    cube.name = $"Cube x{x}z{z}";
                    cube.transform.position =  new Vector3(x, posY, z);
                }
            }
            Debug.Log("Complete Generate Map");
        }
        else
        {
            Debug.Log("Generate Map By Seed");
        }
    }
    private void InitializeMap()
    {
        Debug.Log("Start Initialize");
        float rayOriginY = _worldYLimid + 1;
        Vector3 rayOriginVec = new Vector3(_x, rayOriginY, _z);
        for (_x = 0; _x < _mapSize.x; _x++)
        {
            for(_z = 0; _z < _mapSize.y; _z++)
            {
                RaycastHit[] hits = Physics.RaycastAll(rayOriginVec, Vector3.down, rayOriginY);
                Debug.Log($"Ray Searched (x{_x} z{_z}). Searched {hits.Length} Objs");
                for (int i = 0; i < hits.Length; i++)
                {
                    Destroy(hits[i].collider.gameObject);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(_x, _worldYLimid + 1, _z), new Vector3(_x, 0, _z));
    }
}

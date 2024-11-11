using System;
using UnityEngine;

namespace Script.Game
{
    public class Minimap: MonoBehaviour
    {
        [SerializeField] private Transform gasCloud, helpTransform;
        [SerializeField] private GameObject minimapObject;
        [SerializeField] private float distance;

        private GameObject mapObject, borderObject;

        private void Start()
        {
            CreateMapObjects();
        }

        private void Update()
        {
            if (Vector3.Distance(mapObject.transform.position, transform.position) > distance)
            {
                helpTransform.LookAt(mapObject.transform);
                borderObject.transform.position = transform.position + distance * helpTransform.forward;
                borderObject.layer = LayerMask.NameToLayer("Minimap");
                mapObject.layer = LayerMask.NameToLayer("Invisible");
            }
            else
            {
                borderObject.layer = LayerMask.NameToLayer("Invisible");
                mapObject.layer = LayerMask.NameToLayer("Minimap");
            }
        }

        private void CreateMapObjects()
        {
            borderObject = Instantiate(minimapObject, gasCloud.transform.position, Quaternion.identity);
            mapObject = Instantiate(minimapObject, gasCloud.transform.position, Quaternion.identity);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFS
{
    public class DestroyAfterTime : MonoBehaviour
    {
        public float lifeSpan = 3f;

        void Start()
        {
            Destroy(gameObject, lifeSpan);
        }
    }
}
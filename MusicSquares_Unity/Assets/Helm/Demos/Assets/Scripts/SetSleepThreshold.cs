﻿using UnityEngine;
using System.Collections;

namespace Helm
{
    [AddComponentMenu("")]
    public class SetSleepThreshold : MonoBehaviour
    {
        public float threshold = 0.0f;

        void Start()
        {
            GetComponent<Rigidbody>().sleepThreshold = threshold;
        }
    }
}

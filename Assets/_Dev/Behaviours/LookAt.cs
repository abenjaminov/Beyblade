using System.Collections;
using UnityEngine;

namespace Assets._Dev.Behaviours
{
    public class LookAt : MonoBehaviour
    {
        public Transform lookAt;

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(lookAt);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningLight : MonoBehaviour {
    [SerializeField] private float spinSpeed = 750f;

    void Update() {
        //rotate the light once per frame
        transform.Rotate(Vector3.right * spinSpeed * Time.deltaTime);
    }
}

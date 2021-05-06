using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnScreen : MonoBehaviour {

    void Update() {

        Camera mainCam = Camera.main;                                           //get the main camera
        Vector3 posInCam = mainCam.WorldToViewportPoint(transform.position);    //get the object's position within the main camera (between 0.0 and 1.0) as a vector


        if (posInCam.x > 1) {                                                   //if the object is outside if the camera in the +x direction (ie. it is moving to the right)
            posInCam.x = 0.01f;                                                 //set the x position to the -x side of the screen
        }
        else if (posInCam.x < 0) {                                              //if the object is outside if the camera in the -x direction (ie. it is moving to the left)
            posInCam.x = 0.99f;                                                 //set the x position to the +x side of the screen
        }

        if (posInCam.y > 1) {                                                   //if the object is outside if the camera in the +y direction (ie. it is moving up)
            posInCam.y = 0.01f;                                                 //set the x position to the -y side of the screen
        }
        else if (posInCam.y < 0) {                                              //if the object is outside if the camera in the -y direction (ie. it is moving down)
            posInCam.y = 0.99f;                                                 //set the x position to the +y side of the screen
        }
        transform.position = mainCam.ViewportToWorldPoint(posInCam);            //change the position to the newly calculated position
    }
}

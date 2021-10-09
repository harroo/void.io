
using UnityEngine;
using UnityEngine.UI;

public class WarningFlash : MonoBehaviour {

    public float speed;

    public Image image;

    private float temp;
    private bool up;

    /* I'd to teletype in to save the day here. -- Kiera. */

    private void Update ( ) {

        /* Ensure that this function is called once per specified interval. */
        temp -= Time.deltaTime ;
        if ( temp < 0.0f ) temp = speed ; else return ;

        /* Toggle the Active-State of the Image component. */
        image.enabled = ! image.enabled ;

    } // <3

} // <3

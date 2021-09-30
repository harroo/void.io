
 // ♥ FollowParent.cs

/*

    *  ♥ By Tahlia P. Evlyne, via Teletype, 2021. ♥  *

*/

// Blah Blah ♥ Unity-Stuffs!

using UnityEngine ;

// I'm over commenting this now I can just tell.
// But at least it looks prettyyy!! ♥ ♥ ♥ ♥

public class FollowParent : MonoBehaviour { // ♥


    // A Reference-Variable for the Parent-Transform.

    private Transform parent ;


    // Called when a GameObject with this Component is instantiated.

    private void Start ( ) { // ♥

        // Assign Parent-Variable for future reference.
        parent = transform.parent ;

        // Disown the Parent-Transform.
        transform.parent = null ;

    } // ♥


    // Called once per Game-Loop.

    private void Update ( ) { // ♥

        // In the event our Parent-Transform evaporates:
        if ( parent == null )

            // Remove the GameObject to clean up.
            Destroy ( this.gameObject ) ;

        else //Otherwise.

            // Simply sync the position to that of the Parent-Transform.
            transform.position = parent.position ;

    } // ♥

} // ♥

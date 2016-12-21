using UnityEngine;
using System.Collections;

public class DestroyArea : MonoBehaviour {

    private void OnTriggerExit2D(Collider2D c)
    {
        Destroy(c.gameObject);
    }
}

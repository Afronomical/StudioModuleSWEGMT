using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideChild : MonoBehaviour
{
    public GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        // Destroys child
        Destroy(child);

        //yes this actually has a use
        //i need guildelines when creating the tiles, so having a tilemap in the background while in edit, but having it removed in runtime is SO MUCH EASIER than what i did last time
        // also destroy child is such a good command
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

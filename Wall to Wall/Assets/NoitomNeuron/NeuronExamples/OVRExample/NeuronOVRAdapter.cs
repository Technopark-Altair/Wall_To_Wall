﻿using UnityEngine;
using System.Collections;

public class NeuronOVRAdapter : MonoBehaviour
{
    public Transform            bindTransform = null;
    
    void FixedUpdate( )
    {        
		// Re-Position the camera to our head bind Target
        transform.position = bindTransform.position;
    }
}

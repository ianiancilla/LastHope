using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class SlidePanel
{
    [field: SerializeField] public GameObject panelGO { get; private set; }
    [field: SerializeField] public float displayTime { get; private set; }
    [field: SerializeField] public bool fadeIn { get; private set; }
    [field: SerializeField] public bool fadeOut { get; private set; }
}


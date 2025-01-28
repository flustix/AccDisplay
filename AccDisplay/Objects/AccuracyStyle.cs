using System;
using UnityEngine;

namespace AccDisplay.Objects;

public abstract class AccuracyStyle : MonoBehaviour
{
    protected GameObject Layer { get; private set; }
    protected AccuracyManager Manager { get; private set; }
    
    public AccuracyStyle(IntPtr intPtr)
        : base(intPtr)
    {
    }
    
    public void Setup(GameObject layer, AccuracyManager manager)
    {
        Layer = layer;
        Manager = manager;
        
        OnSetup();
    }

    protected abstract void OnSetup();
    public abstract void Cleanup();
}
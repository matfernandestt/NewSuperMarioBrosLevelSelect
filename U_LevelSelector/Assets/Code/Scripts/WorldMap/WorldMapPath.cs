using System;
using PathCreation;
using UnityEngine;

public class WorldMapPath : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    public LineRenderer GetLineRendererPoints => lineRenderer;
}

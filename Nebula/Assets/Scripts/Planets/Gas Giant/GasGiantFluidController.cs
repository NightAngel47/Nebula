using System;
using System.Collections;
using System.Collections.Generic;
using FluidDynamics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GasGiantFluidController : MonoBehaviour
{
    private Main_Fluid_Simulation _fluidSimulation;
    public List<Gradient> gasGradients = new List<Gradient>();
    private Gradient _currentGas;
    private bool _updating = false;
    
    void Awake()
    {
        _fluidSimulation = FindObjectOfType<Main_Fluid_Simulation>();
    }

    private void Start()
    {
        // set random starting gradient
        ChangeGradient(Random.Range(0, gasGradients.Count));
    }

    /// <summary>
    /// Updates the fluid simulation to match the selected gradient
    /// </summary>
    /// <param name="selectedGradient">The index of the selected gradient</param>
    public void ChangeGradient(int selectedGradient)
    {
        _updating = true;
        _fluidSimulation.m_updateGradient = true;
        _fluidSimulation.m_colourGradient = gasGradients[selectedGradient];
        _currentGas = gasGradients[selectedGradient];
        _updating = false;
        StartCoroutine(UpdateOff());
    }

    private IEnumerator UpdateOff()
    {
        yield return new WaitWhile(() => _updating);
        _fluidSimulation.m_updateGradient = false;
    }

    public Gradient GetCurrentGas()
    {
        return _currentGas;
    }
}

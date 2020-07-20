using System;
using System.Collections;
using System.Collections.Generic;
using FluidDynamics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GasGiantFluidController : MonoBehaviour
{
    private Main_Fluid_Simulation _fluidSimulation;
    
    public List<Color> gasBaseColors = new List<Color>(8);
    public List<Color> gasBandColors = new List<Color>(8);
    private Gradient _currentGasGradient;
    private GradientColorKey[] _currentGasColorKeys;
    private GradientAlphaKey[] _currentGasAlphaKeys;

    private bool _updating = false;
    
    void Awake()
    {
        _fluidSimulation = FindObjectOfType<Main_Fluid_Simulation>();
    }

    private void Start()
    {
        // sets up gas gradient for fluid dynamics
        _currentGasGradient = new Gradient();
        _currentGasColorKeys = new GradientColorKey[2];
        _currentGasColorKeys[0].time = 0.25f;
        _currentGasColorKeys[1].time = 0.75f;
        
        _currentGasAlphaKeys = new GradientAlphaKey[2];
        _currentGasAlphaKeys[0].alpha = 1.0f;
        _currentGasAlphaKeys[0].time = 0.0f;
        _currentGasAlphaKeys[1].alpha = 1.0f;
        _currentGasAlphaKeys[1].time = 1.0f;
        
        // set random starting gradient
        ChangeGasBaseColor(Random.Range(0, gasBaseColors.Count));
        ChangeGasBandColor(Random.Range(0, gasBandColors.Count));
    }

    /// <summary>
    /// Changes Gas Base Color and Updates Fluid Gradient
    /// </summary>
    /// <param name="selectedBaseColor">index of selected color</param>
    public void ChangeGasBaseColor(int selectedBaseColor)
    {
        _currentGasColorKeys[0].color = gasBaseColors[selectedBaseColor];
        ChangeGradient();
    }
    
    /// <summary>
    /// Changes Gas Band Color and Updates Fluid Gradient
    /// </summary>
    /// <param name="selectedBandColor">index of selected color</param>
    public void ChangeGasBandColor(int selectedBandColor)
    {
        _currentGasColorKeys[1].color = gasBaseColors[selectedBandColor];
        ChangeGradient();
    }

    /// <summary>
    /// Updates the fluid simulation to match the selected gradient
    /// </summary>
    private void ChangeGradient()
    {
        _updating = true;
        _currentGasGradient.SetKeys(_currentGasColorKeys, _currentGasAlphaKeys);
        _fluidSimulation.m_updateGradient = true;
        _fluidSimulation.m_colourGradient = _currentGasGradient;
        _updating = false;
        StartCoroutine(UpdateOff());
    }
    
    private IEnumerator UpdateOff()
    {
        yield return new WaitWhile(() => _updating);
        _fluidSimulation.m_updateGradient = false;
    }

    public Gradient GetCurrentGasGradient()
    {
        return _currentGasGradient;
    }
}

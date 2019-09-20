using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FluidDynamics;

public class fluidUI : MonoBehaviour
{
    public Slider vorticity;
    public Slider viscocity;
    public Slider partLife;
    public Slider resolution;
    public Slider simSpeed;
    public Main_Fluid_Simulation fluid;

    // Update is called once per frame
    void Update()
    {
        fluid.m_speed = simSpeed.value;
        fluid.m_vorticityScale = vorticity.value;
        fluid.m_viscosity = viscocity.value;
        fluid.m_densityDissipation = partLife.value;
        fluid.ParticlesResolution = (int)resolution.value;
    }
}

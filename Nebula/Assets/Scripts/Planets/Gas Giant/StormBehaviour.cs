using System.Collections;
using System.Collections.Generic;
using FluidDynamics;
using UnityEngine;

public class StormBehaviour : MonoBehaviour
{
    private Fluid_Dynamics_Particles_Emitter _particlesEmitter;
    private Fluid_Dynamics_Velocity_Emitter _velocityEmitter;

    void Start()
    {
        _particlesEmitter = GetComponentInChildren<Fluid_Dynamics_Particles_Emitter>();
        _velocityEmitter = GetComponentInChildren<Fluid_Dynamics_Velocity_Emitter>();

        Main_Fluid_Simulation mainFluidSimulation = transform.GetComponentInParent<Main_Fluid_Simulation>();
        _particlesEmitter.m_mainSimulation = mainFluidSimulation;
        _velocityEmitter.m_fluid = mainFluidSimulation;
    }
}

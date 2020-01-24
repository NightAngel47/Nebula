using UnityEngine;
namespace FluidDynamics
{
[RequireComponent(typeof(MeshCollider), typeof(MeshRenderer))]
[AddComponentMenu("Fluid Dynamics/Fluid Simulator")]
public class Main_Fluid_Simulation : MonoBehaviour {

    #region Variables
    public ComputeShader m_simulationShader;
    public ComputeShader m_particleAreaShader;
    public bool m_simulate = true;
	[HideInInspector]
	public float m_speed = 500f;
	public float Speed{get{return m_speed;}set{m_speed = value;}}
	[HideInInspector]
	public int m_iterations = 50;
	public int Iterations{get{return m_iterations;}set{m_iterations = value;}}
	[HideInInspector]
	public float m_velocityDissipation = 1f;
	public float VelocityDissipation{get{return m_velocityDissipation;}set{m_velocityDissipation = value;}}
	[HideInInspector]
	public float m_vorticityScale = 0f;
	public float Vorticity{get{return m_vorticityScale;}set{m_vorticityScale = value;}}
	[HideInInspector]
	public float m_viscosity = 0f;
	public float Viscosity{get{return m_viscosity;}set{m_viscosity = value;}}
	[HideInInspector]
	public int m_nResolution = 512;
    [HideInInspector]
    public bool m_cacheVelocity = false;
    public int Resolution{get{return m_nResolution;}set{m_nResolution = value;}}
	public ComputeBuffer VelocityBuffer{get{return m_velocityBuffer[VELOCITY_READ];}}
	public ComputeBuffer DivergenceBuffer{get{return m_divergenceBuffer;}}
	public ComputeBuffer PressureBuffer{get{return m_pressure[PRESSURE_READ];}}
	public ComputeBuffer ObstaclesBuffer {get {return m_obstaclesBuffer;}}
    private ComputeBuffer m_obstaclesBuffer;
    private ComputeBuffer[] m_velocityBuffer;
    private ComputeBuffer m_divergenceBuffer;
    private ComputeBuffer[] m_pressure;
    private ComputeBuffer m_vorticityBuffer;
    private Vector2[] m_currentVelocity;
    private int m_nNumCells;
    private int m_nNumGroupsX;
    private int m_nNumGroupsY;
    private int m_nWidth = 512;
    private int m_nHeight = 512;
    private int m_addVelocityKernel = 0;
    private int m_initBoundariesKernel = 0;
    private int m_advectVelocityKernel = 0;
    private int m_divergenceKernel = 0;
    private int m_poissonKernel = 0;
    private int m_substractGradientKernel = 0;
    private int m_calcVorticityKernel = 0;
    private int m_applyVorticityKernel = 0;
    private int m_viscosityKernel = 0;
    private int m_addObstacleCircleKernel = 0;
    private int m_addObstacleTriangleKernel = 0;
    private int m_clearBufferKernel = 0;
    private int VELOCITY_READ = 0;
    private int VELOCITY_WRITE = 1;
    private int PRESSURE_READ = 0;
    private int PRESSURE_WRITE = 1;
    #endregion
    //
    #region Variables2
    [Tooltip("If m_ExposeParticles is true the value of the particles will be cached in memory for use by other systems")]
    public bool m_CacheParticles = false;
    public Gradient m_colourGradient;
    public bool m_updateGradient = false;
    [HideInInspector]
    public float m_densityDissipation = 1f;
    public float Dissipation { get { return m_densityDissipation; } set { m_densityDissipation = value; } }
    public ComputeBuffer ParticlesBuffer { get { return m_particlesBuffer[READ]; } }
    private ComputeBuffer m_colourRamp;
    private int m_addParticlesKernel = 0;
    private int m_advectKernel = 0;
    [HideInInspector]
    public int m_nParticlesResolution = 128;
    public int ParticlesResolution
    {
        get
        {
            return m_nParticlesResolution;
        }
        set
        {
            if (value != m_nParticlesResolution)
            {
                m_nParticlesResolution = value;
                if (Application.isPlaying && m_particlesBuffer[0] != null && m_particlesBuffer[1] != null)
                {
                    int nOldHeight = m_nParticlesHeight;
                    int nOldWidth = m_nParticlesWidth;
                    float[] oldParticleData = new float[nOldWidth * nOldHeight];
                    m_particlesBuffer[READ].GetData(oldParticleData);
                    m_particlesBuffer[0].Dispose();
                    m_particlesBuffer[1].Dispose();

                    CalculateSize();

                    float[] newParticleData = new float[m_nParticlesWidth * m_nParticlesHeight];
                    for (int i = 0; i < m_nParticlesHeight; ++i)
                    {
                        for (int j = 0; j < m_nParticlesWidth; ++j)
                        {
                            float normX = (float)j / (float)m_nParticlesWidth;
                            float normY = (float)i / (float)m_nParticlesHeight;
                            int x = (int)(normX * (float)nOldWidth);
                            int y = (int)(normY * (float)nOldHeight);
                            newParticleData[i * m_nParticlesWidth + j] = oldParticleData[y * nOldWidth + x];
                        }
                    }

                    m_particlesBuffer = new ComputeBuffer[2];
                    for (int i = 0; i < 2; ++i)
                    {
                        m_particlesBuffer[i] = new ComputeBuffer(m_nParticlesWidth * m_nParticlesHeight, 4, ComputeBufferType.Default);
                    }
                    m_particlesBuffer[READ].SetData(newParticleData);

                }
            }
        }
    }
    private int m_nColourRampSize = 256;
    private ComputeBuffer[] m_particlesBuffer;
    private int m_nParticlesNumGroupsX;
    private int m_nParticlesNumGroupsY;
    private int m_nParticlesWidth = 512;
    private int m_nParticlesHeight = 512;
    private int READ = 0;
    private int WRITE = 1;
    private float[] m_currentParticles;
    private Renderer m_tempRend;
    #endregion


    private void Start()
    {
        m_simulationShader = (ComputeShader)Instantiate(Resources.Load("FLUID_DYNAMICS_SIMULATION"));
        m_particleAreaShader = (ComputeShader)Instantiate(Resources.Load("FLUID_DYNAMICS_PARTICLES"));
        m_tempRend = GetComponent<Renderer>();
        m_tempRend.material = new Material(Shader.Find("FluidDynamics/MatRenderer"));
        if (SystemInfo.supportsComputeShaders)
        {
            m_nNumCells = m_nWidth * m_nHeight;
            m_addVelocityKernel = m_simulationShader.FindKernel("AddVelocity");
            m_initBoundariesKernel = m_simulationShader.FindKernel("InitBoundaries");
            m_advectVelocityKernel = m_simulationShader.FindKernel("AdvectVelocity");
            m_divergenceKernel = m_simulationShader.FindKernel("Divergence");
            m_clearBufferKernel = m_simulationShader.FindKernel("ClearBuffer");
            m_poissonKernel = m_simulationShader.FindKernel("Poisson");
            m_substractGradientKernel = m_simulationShader.FindKernel("SubstractGradient");
            m_calcVorticityKernel = m_simulationShader.FindKernel("CalcVorticity");
            m_applyVorticityKernel = m_simulationShader.FindKernel("ApplyVorticity");
            m_viscosityKernel = m_simulationShader.FindKernel("Viscosity");
            m_addObstacleCircleKernel = m_simulationShader.FindKernel("AddCircleObstacle");
            m_addObstacleTriangleKernel = m_simulationShader.FindKernel("AddTriangleObstacle");
            CalculateSize();
            LinkToFluidSimulation();
            m_advectKernel = m_particleAreaShader.FindKernel("Advect");
            m_addParticlesKernel = m_particleAreaShader.FindKernel("AddParticles");
        }
        else
        {
            m_simulate = false;
            Debug.LogError("Seems like your target Hardware does not support Compute Shaders. 'Fluid Dynamics' needs support for Compute Shaders to work.");
        }
    }
    private void Update()
	{
		if (m_simulate)
		{
			UpdateParameters();
			CreateBuffersIfNeeded();
			m_simulationShader.SetBuffer(m_initBoundariesKernel, "_VelocityIn", m_velocityBuffer[VELOCITY_READ]);
            m_simulationShader.Dispatch(m_initBoundariesKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
            m_simulationShader.SetBuffer(m_advectVelocityKernel, "_Obstacles", m_obstaclesBuffer);
            m_simulationShader.SetBuffer(m_advectVelocityKernel, "_VelocityIn", m_velocityBuffer[VELOCITY_READ]);
            m_simulationShader.SetBuffer(m_advectVelocityKernel, "_VelocityOut", m_velocityBuffer[VELOCITY_WRITE]);
            m_simulationShader.Dispatch(m_advectVelocityKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
            FlipVelocityBuffers();
            m_simulationShader.SetBuffer(m_calcVorticityKernel, "_VelocityIn", m_velocityBuffer[VELOCITY_READ]);
            m_simulationShader.SetBuffer(m_calcVorticityKernel, "_Vorticity", m_vorticityBuffer);
            m_simulationShader.Dispatch(m_calcVorticityKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
			m_simulationShader.SetBuffer(m_applyVorticityKernel, "_VelocityIn", m_velocityBuffer[VELOCITY_READ]);
			m_simulationShader.SetBuffer(m_applyVorticityKernel, "_Vorticity", m_vorticityBuffer);
			m_simulationShader.SetBuffer(m_applyVorticityKernel, "_VelocityOut", m_velocityBuffer[VELOCITY_WRITE]);
            m_simulationShader.Dispatch(m_applyVorticityKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
            FlipVelocityBuffers();
			if (m_viscosity > 0.0f)
			{
                for (int i = 0; i < m_iterations; ++i)
				{
                    m_simulationShader.SetBuffer(m_viscosityKernel, "_VelocityIn", m_velocityBuffer[VELOCITY_READ]);
                    m_simulationShader.SetBuffer(m_viscosityKernel, "_VelocityOut", m_velocityBuffer[VELOCITY_WRITE]);
                    m_simulationShader.Dispatch(m_viscosityKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
                    FlipVelocityBuffers();
				}
			}
			m_simulationShader.SetBuffer(m_divergenceKernel, "_VelocityIn", m_velocityBuffer[VELOCITY_READ]);
			m_simulationShader.SetBuffer(m_divergenceKernel, "_Obstacles", m_obstaclesBuffer);
			m_simulationShader.SetBuffer(m_divergenceKernel, "_Divergence", m_divergenceBuffer);
            m_simulationShader.Dispatch(m_divergenceKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
            m_simulationShader.SetBuffer(m_clearBufferKernel, "_Buffer", m_pressure[PRESSURE_READ]);
            m_simulationShader.Dispatch(m_clearBufferKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
            m_simulationShader.SetBuffer(m_poissonKernel, "_Divergence", m_divergenceBuffer);
            m_simulationShader.SetBuffer(m_poissonKernel, "_Obstacles", m_obstaclesBuffer);
            for (int i = 0; i < m_iterations; ++i)
			{
                m_simulationShader.SetBuffer(m_poissonKernel, "_PressureIn", m_pressure[PRESSURE_READ]);
                m_simulationShader.SetBuffer(m_poissonKernel, "_PressureOut", m_pressure[PRESSURE_WRITE]);
                m_simulationShader.Dispatch(m_poissonKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
				FlipPressureBuffers();
			}
			m_simulationShader.SetBuffer(m_substractGradientKernel, "_PressureIn", m_pressure[PRESSURE_READ]);
			m_simulationShader.SetBuffer(m_substractGradientKernel, "_VelocityIn", m_velocityBuffer[VELOCITY_READ]);
			m_simulationShader.SetBuffer(m_substractGradientKernel, "_VelocityOut", m_velocityBuffer[VELOCITY_WRITE]);
			m_simulationShader.SetBuffer(m_substractGradientKernel, "_Obstacles", m_obstaclesBuffer);
            m_simulationShader.Dispatch(m_substractGradientKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
            FlipVelocityBuffers();
            m_simulationShader.SetBuffer(m_clearBufferKernel, "_Buffer", m_obstaclesBuffer);
            m_simulationShader.Dispatch(m_clearBufferKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
            if (m_cacheVelocity)
            {
                m_velocityBuffer[VELOCITY_READ].GetData(m_currentVelocity);
            }
		}
        if (m_particlesBuffer == null)
        {
            //Debug.Log("null");
            m_particlesBuffer = new ComputeBuffer[2];
            for (int i = 0; i < 2; ++i)
            {
                m_particlesBuffer[i] = new ComputeBuffer(m_nParticlesWidth * m_nParticlesHeight, 4, ComputeBufferType.Default);
            }
        }
        m_particleAreaShader.SetFloat("_Dissipation", m_densityDissipation);
        m_particleAreaShader.SetFloat("_ElapsedTime", Time.deltaTime);
        m_particleAreaShader.SetFloat("_Speed", GetSimulationSpeed());
        m_particleAreaShader.SetBuffer(m_advectKernel, "_Obstacles", ObstaclesBuffer);
        m_particleAreaShader.SetBuffer(m_advectKernel, "_Velocity", VelocityBuffer);
        m_particleAreaShader.SetBuffer(m_advectKernel, "_ParticlesIn", m_particlesBuffer[READ]);
        m_particleAreaShader.SetBuffer(m_advectKernel, "_ParticlesOut", m_particlesBuffer[WRITE]);
        m_particleAreaShader.Dispatch(m_advectKernel, m_nParticlesNumGroupsX, m_nParticlesNumGroupsY, 1);
        FlipBuffers();
        if (m_colourRamp == null)
        {
            m_colourRamp = new ComputeBuffer(m_nColourRampSize, 16, ComputeBufferType.Default);
            UpdateGradient();
        }
        if (m_updateGradient)
        {
            UpdateGradient();
        }
        m_tempRend.material.SetBuffer("_Particles", m_particlesBuffer[READ]);
        m_tempRend.material.SetBuffer("_ColourRamp", m_colourRamp);
        m_tempRend.material.SetVector("_Size", new Vector2(m_nParticlesWidth, m_nParticlesHeight));
        if (m_CacheParticles)
        {
            m_particlesBuffer[READ].GetData(m_currentParticles);
        }
    }
    private void OnDisable()
    {
        if (m_velocityBuffer != null && m_velocityBuffer.Length == 2)
        {
            if (m_velocityBuffer[0] != null)
            {
                m_velocityBuffer[0].Dispose();
            }
            if (m_velocityBuffer[1] != null)
            {
                m_velocityBuffer[1].Dispose();
            }
        }
        if (m_divergenceBuffer != null)
        {
            m_divergenceBuffer.Dispose();
        }
        if (m_pressure != null && m_pressure.Length == 2)
        {
            if (m_pressure[0] != null)
            {
                m_pressure[0].Dispose();
            }
            if (m_pressure[1] != null)
            {
                m_pressure[1].Dispose();
            }
        }
        if (m_obstaclesBuffer != null)
        {
            m_obstaclesBuffer.Dispose();
        }
        if (m_vorticityBuffer != null)
        {
            m_vorticityBuffer.Dispose();
        }
        if (m_colourRamp != null)
        {
            m_colourRamp.Dispose();
        }
        if (m_particlesBuffer != null && m_particlesBuffer.Length == 2)
        {
            if (m_particlesBuffer[0] != null)
            {
                m_particlesBuffer[0].Dispose();
            }
            if (m_particlesBuffer[1] != null)
            {
                m_particlesBuffer[1].Dispose();
            }
        }
    }

    public void SetSize(int nWidth, int nHeight)
    {
        uint groupSizeX = 8;
        uint groupSizeY = 8;
        uint groupSizeZ = 8;
        m_simulationShader.GetKernelThreadGroupSizes(0, out groupSizeX, out groupSizeY, out groupSizeZ);
        m_nWidth = nWidth;
        m_nHeight = nHeight;
        m_nNumCells = m_nWidth * m_nHeight;
        m_nNumGroupsX = Mathf.CeilToInt((float)m_nWidth / (float)groupSizeX);
        m_nNumGroupsY = Mathf.CeilToInt((float)m_nHeight / (float)groupSizeX);
    }
    public int GetWidth()
    {
        return m_nWidth;
    }
    public int GetHeight()
    {
        return m_nHeight;
    }
    public void AddVelocity(Vector2 position, Vector2 velocity, float fRadius)
    {
        if (m_simulationShader != null && m_velocityBuffer != null && m_velocityBuffer.Length >= 2)
        {
            float[] pos = { position.x, position.y };
            m_simulationShader.SetFloats("_Position", pos);
            float[] val = { velocity.x, velocity.y };
            m_simulationShader.SetFloats("_Value", val);
            m_simulationShader.SetFloat("_Radius", fRadius);
            m_simulationShader.SetInts("_Size", new int[] { m_nWidth, m_nHeight });
            m_simulationShader.SetBuffer(m_addVelocityKernel, "_VelocityIn", m_velocityBuffer[VELOCITY_READ]);
            m_simulationShader.SetBuffer(m_addVelocityKernel, "_VelocityOut", m_velocityBuffer[VELOCITY_WRITE]);
            m_simulationShader.Dispatch(m_addVelocityKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
            FlipVelocityBuffers();
        }
    }
    public void AddObstacleCircle(Vector2 position, float fRadius, bool bStatic)
    {
        float[] pos = { position.x, position.y };
        m_simulationShader.SetFloats("_Position", pos);
        m_simulationShader.SetFloat("_Radius", fRadius);
        m_simulationShader.SetInt("_Static", bStatic ? 1 : 0);
        m_simulationShader.SetBuffer(m_addObstacleCircleKernel, "_Obstacles", m_obstaclesBuffer);
        m_simulationShader.Dispatch(m_addObstacleCircleKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
    }
    public void AddObstacleTriangle(Vector2 p1, Vector2 p2, Vector2 p3, bool bStatic = false)
    {
        float[] pos1 = { p1.x, p1.y };
        float[] pos2 = { p2.x, p2.y };
        float[] pos3 = { p3.x, p3.y };
        m_simulationShader.SetFloats("_P1", pos1);
        m_simulationShader.SetFloats("_P2", pos2);
        m_simulationShader.SetFloats("_P3", pos3);
        m_simulationShader.SetInt("_Static", bStatic ? 1 : 0);
        m_simulationShader.SetBuffer(m_addObstacleTriangleKernel, "_Obstacles", m_obstaclesBuffer);
        m_simulationShader.Dispatch(m_addObstacleTriangleKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
    }
    public Vector2 GetVelocity(int x, int y)
    {
        return m_currentVelocity[y * m_nWidth + x] * m_speed;
    }
    public void InitShaders()
	{
		CreateBuffersIfNeeded();
		UpdateParameters();
		int[] size = new int[] { m_nWidth, m_nHeight };
		m_simulationShader.SetInts("_Size", size);
		m_currentVelocity = new Vector2[m_nNumCells];
	}
	public float GetSimulationSpeed()
	{
		return m_speed;
	}
    private void CreateBuffersIfNeeded()
    {
        if (m_velocityBuffer == null)
        {
            m_velocityBuffer = new ComputeBuffer[2];
            for (int i = 0; i < 2; ++i)
            {
                m_velocityBuffer[i] = new ComputeBuffer(m_nNumCells, 8, ComputeBufferType.Default);
            }
        }
        if (m_divergenceBuffer == null)
        {
            m_divergenceBuffer = new ComputeBuffer(m_nNumCells, 4, ComputeBufferType.Default);
        }
        if (m_pressure == null)
        {
            m_pressure = new ComputeBuffer[2];
            for (int i = 0; i < 2; ++i)
            {
                m_pressure[i] = new ComputeBuffer(m_nNumCells, 4, ComputeBufferType.Default);
            }
        }
        if (m_obstaclesBuffer == null)
        {
            m_obstaclesBuffer = new ComputeBuffer(m_nNumCells, 8, ComputeBufferType.Default);
        }
        if (m_vorticityBuffer == null)
        {
            m_vorticityBuffer = new ComputeBuffer(m_nNumCells, 4, ComputeBufferType.Default);
        }
    }
    private void UpdateParameters()
    {
        m_simulationShader.SetFloat("_Dissipation", m_velocityDissipation);
        m_simulationShader.SetFloat("_ElapsedTime", Time.deltaTime);
        m_simulationShader.SetFloat("_Speed", m_speed);
        m_simulationShader.SetFloat("_VorticityScale", m_vorticityScale);

        float centreFactor = 1.0f / (m_viscosity);
        float stencilFactor = 1.0f / (4.0f + centreFactor);
        m_simulationShader.SetFloat("_Alpha", centreFactor);
        m_simulationShader.SetFloat("_rBeta", stencilFactor);
    }
    private void FlipVelocityBuffers()
	{
        int aux = VELOCITY_READ;
        VELOCITY_READ = VELOCITY_WRITE;
        VELOCITY_WRITE = aux;
	}
    private void FlipPressureBuffers()
    {
        int aux = PRESSURE_READ;
        PRESSURE_READ = PRESSURE_WRITE;
        PRESSURE_WRITE = aux;
    }
    public float GetParticles(int x, int y)
    {
        return m_currentParticles[y * m_nParticlesWidth + x];
    }
    private void CalculateSize()
    {
        float x = gameObject.transform.lossyScale.x;
        float y = gameObject.transform.lossyScale.z;
        if (x > y)
        {
            float fHeight = (y / x) * m_nParticlesResolution;
            m_nParticlesWidth = m_nParticlesResolution;
            m_nParticlesHeight = (int)fHeight;
        }
        else
        {
            float fWidth = (x / y) * m_nParticlesResolution;
            m_nParticlesWidth = (int)fWidth;
            m_nParticlesHeight = m_nParticlesResolution;
        }
        SetSizeInShaders();
        uint groupSizeX = 8;
        uint groupSizeY = 8;
        uint groupSizeZ = 8;
        m_particleAreaShader.GetKernelThreadGroupSizes(0, out groupSizeX, out groupSizeY, out groupSizeZ);
        m_nParticlesNumGroupsX = Mathf.CeilToInt((float)m_nParticlesWidth / (float)groupSizeX);
        m_nParticlesNumGroupsY = Mathf.CeilToInt((float)m_nParticlesHeight / (float)groupSizeX);
        m_currentParticles = new float[m_nParticlesWidth * m_nParticlesHeight];
    }
    private void LinkToFluidSimulation()
    {
        float fResolutionRatio = Resolution / (float)m_nParticlesResolution;
        SetSize((int)(m_nParticlesWidth * fResolutionRatio), (int)(m_nParticlesHeight * fResolutionRatio));

        InitShaders();
        m_particleAreaShader.SetInts("_VelocitySize", new int[] { GetWidth(), GetHeight() });
    }
    private void FlipBuffers()
    {
        int aux = READ;
        READ = WRITE;
        WRITE = aux;
    }
    public Vector2 GetRenderScale()
    {
        return transform.lossyScale;
    }
    public Vector2 GetRenderSize()
    {
        return m_tempRend.bounds.size;
    }
    public int GetParticlesWidth()
    {
        return m_nParticlesWidth;
    }
    public int GetParticlesHeight()
    {
        return m_nParticlesHeight;
    }
    private void UpdateGradient()
    {
        Vector4[] colourData = new Vector4[m_nColourRampSize];
        for (int i = 0; i < m_nColourRampSize; ++i)
        {
            colourData[i] = m_colourGradient.Evaluate(i / 255.0f);
        }
        m_colourRamp.SetData(colourData);
    }
    void SetSizeInShaders()
    {
        m_particleAreaShader.SetInts("_ParticleSize", new int[] { m_nParticlesWidth, m_nParticlesHeight });
        m_tempRend.material.SetVector("_Size", new Vector2(m_nParticlesWidth, m_nParticlesHeight));
    }
    public void AddParticles(Vector2 position, float fRadius, float fStrength)
    {
        if (m_particleAreaShader != null && m_particlesBuffer != null && m_particlesBuffer.Length >= 2)
        {
            float[] pos = { position.x, position.y };
            m_particleAreaShader.SetFloats("_Position", pos);
            m_particleAreaShader.SetFloat("_Value", fStrength);
            m_particleAreaShader.SetFloat("_Radius", fRadius);
            m_particleAreaShader.SetInts("_Size", new int[] { m_nParticlesWidth, m_nParticlesHeight });
            m_particleAreaShader.SetBuffer(m_addParticlesKernel, "_ParticlesIn", m_particlesBuffer[READ]);
            m_particleAreaShader.SetBuffer(m_addParticlesKernel, "_ParticlesOut", m_particlesBuffer[WRITE]);
            m_particleAreaShader.Dispatch(m_addParticlesKernel, m_nParticlesNumGroupsX, m_nParticlesNumGroupsY, 1);
            FlipBuffers();
            m_tempRend.material.SetBuffer("_Particles", m_particlesBuffer[READ]);
        }
    }
}
}
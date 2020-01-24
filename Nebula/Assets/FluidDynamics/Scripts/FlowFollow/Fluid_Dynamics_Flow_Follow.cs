using UnityEngine;
namespace FluidDynamics
{
    [AddComponentMenu("Fluid Dynamics/Flow/Flow Follower")]
    public class Fluid_Dynamics_Flow_Follow : MonoBehaviour
    {
        public bool useRigidbody = false;
        public float lerpSpeed;
        public states updateMode = states.FixedUpdate;
        public Main_Fluid_Simulation m_fluid;
        [Range(0.01f, 1)]
        public float m_Weight = 0.1F;
        public float distance = 10;
        private Collider m_tempCol;
        private Ray ray;
        private RaycastHit m_HitInfo;
        private Vector2 m_FluidSSize;
        private Vector2 m_velocityInFluidSpace;
        private Vector2 m_PosInFluidSpace;
        private Vector2 m_WorldVelocity;
        private Rigidbody2D rig;

        private void Start()
        {
            if (useRigidbody)
            {
                rig = GetComponent<Rigidbody2D>();
            }
            m_tempCol = m_fluid.GetComponent<Collider>();
            if (m_fluid && !m_fluid.m_cacheVelocity)
            {
                Debug.LogWarning("\"Cache Velocity\" must be set to true on the FluidSumulator component to use the Fluid Follower.");
            }
        }

        private void Update()
        {
            if (updateMode == states.Update)
                UpdatePosition();
        }
        private void FixedUpdate()
        {
            if (updateMode == states.FixedUpdate)
                UpdatePosition();
        }
        private void LateUpdate()
        {
            if (updateMode == states.LateUpdate)
                UpdatePosition();
        }
        private void UpdatePosition()
        {
            if (m_fluid)
            {
                ray = new Ray(gameObject.transform.position, Vector3.forward);
                if (m_tempCol.Raycast(ray, out m_HitInfo, distance))
                {
                    m_FluidSSize = new Vector2(m_fluid.GetWidth(), m_fluid.GetHeight());
                    m_PosInFluidSpace = new Vector2(m_HitInfo.textureCoord.x * m_FluidSSize.x, m_HitInfo.textureCoord.y * m_FluidSSize.y);
                    m_velocityInFluidSpace = m_fluid.GetVelocity((int)m_PosInFluidSpace.x, (int)m_PosInFluidSpace.y) * Time.deltaTime;
                    m_WorldVelocity = new Vector2((m_velocityInFluidSpace.x * m_fluid.GetRenderSize().x) / m_FluidSSize.x, (m_velocityInFluidSpace.y * m_fluid.GetRenderSize().y) / m_FluidSSize.y);
                    if (!useRigidbody)
                        transform.position += Vector3.Lerp(transform.position, m_WorldVelocity, lerpSpeed) * Time.deltaTime / m_Weight;
                    else
                        rig.velocity += Vector2.Lerp(transform.position, m_WorldVelocity, lerpSpeed) * Time.deltaTime / m_Weight;
                }
            }
        }
    }
}
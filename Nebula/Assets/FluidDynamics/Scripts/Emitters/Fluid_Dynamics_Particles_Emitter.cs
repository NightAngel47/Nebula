using UnityEngine;
namespace FluidDynamics
{
    public enum states
    {
        Update = 0, LateUpdate = 1, FixedUpdate = 2
    }
    [AddComponentMenu("Fluid Dynamics/Emitters/Fluid Particles Emitter")]
    [ExecuteInEditMode]
    public class Fluid_Dynamics_Particles_Emitter : MonoBehaviour
    {
        public states updateMode;
        public Main_Fluid_Simulation m_mainSimulation;
        [HideInInspector]
        public bool m_useScaleAsSize = true;
        [HideInInspector]
        public float m_radius = 0.1f;
        [HideInInspector]
        public float m_strength = 1f;
        [HideInInspector]
        public bool m_showGizmo = false;
        private Collider m_tempCol;
        private Renderer m_tempRend;
        private Ray ray;
        private RaycastHit hitInfo;
        private bool bdone = false;
        private float fWidth;
        private float fRadius;

        private void Start()
        {
            m_tempCol = m_mainSimulation.GetComponent<Collider>();
            m_tempRend = m_mainSimulation.GetComponent<Renderer>();
        }
        private void Update()
        {
            if (updateMode == states.Update)
                ManipulateParticles();
        }
        void LateUpdate()
        {
            if (updateMode == states.LateUpdate)
                ManipulateParticles();
        }
        private void FixedUpdate()
        {
            if (updateMode == states.FixedUpdate)
                ManipulateParticles();
        }
        public float GetRadius()
        {
            if (m_useScaleAsSize)
            {
                return Mathf.Max(transform.localScale.x, transform.localScale.y);
            }
            return m_radius;
        }
        private void ManipulateParticles()
        {
            if (m_mainSimulation && !bdone)
            {
                ray = new Ray(transform.position, Vector3.forward);
                if (m_tempCol.Raycast(ray, out hitInfo, 10))
                {
                    fWidth = m_tempRend.bounds.extents.x * 2f;
                    fRadius = (GetRadius() * m_mainSimulation.GetParticlesWidth()) / fWidth;
                    m_mainSimulation.AddParticles(hitInfo.textureCoord, fRadius, m_strength * Time.deltaTime);
                }
            }
        }
        private void DrawGizmo()
        {
            float col = m_strength / 10000.0f;
            Gizmos.color = Color.Lerp(Color.yellow, Color.red, col);
            Gizmos.DrawWireSphere(transform.position, GetRadius());
        }
        private void OnDrawGizmosSelected()
        {
            DrawGizmo();
        }
        private void OnDrawGizmos()
        {
            if (m_showGizmo)
            {
                DrawGizmo();
            }
        }
    }
}
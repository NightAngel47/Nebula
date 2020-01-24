using UnityEngine;
namespace FluidDynamics
{
    [AddComponentMenu("Fluid Dynamics/Obstacles/Fluid Circle Obstacle")]
    public class Fluid_Dynamics_Circle_Obstacle : MonoBehaviour
    {

        public Main_Fluid_Simulation m_FluidSimulator;
        [HideInInspector]
        public bool m_useScaleAsSize = true;
        [HideInInspector]
        public float m_radius = 0.1f;
        [HideInInspector]
        public bool m_showGizmo = false;
        private Collider m_tempCol;
        private Renderer m_tempRend;
        private Ray ray;
        private RaycastHit hitInfo;
        private float fWidth;
        private float fRadius;

        private void Start()
        {
            m_tempCol = m_FluidSimulator.GetComponent<Collider>();
            m_tempRend = m_FluidSimulator.GetComponent<Renderer>();
        }
        void LateUpdate()
        {
            ray = new Ray(transform.position, Vector3.forward);
            if (m_tempCol.Raycast(ray, out hitInfo, 10))
            {
                fWidth = m_tempRend.bounds.extents.x * 2f;
                fRadius = (GetRadius() * m_FluidSimulator.GetWidth()) / fWidth;
                m_FluidSimulator.AddObstacleCircle(hitInfo.textureCoord, fRadius, false);
            }
        }
        public float GetRadius()
        {
            if (m_useScaleAsSize)
            {
                return Mathf.Max(transform.lossyScale.x, transform.lossyScale.y);
            }
            return m_radius;
        }
        void DrawGuidelines()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, GetRadius());
        }
        void OnDrawGizmosSelected()
        {
            DrawGuidelines();
        }
        void OnDrawGizmos()
        {
            if (m_showGizmo)
            {
                DrawGuidelines();
            }
        }
    }
}

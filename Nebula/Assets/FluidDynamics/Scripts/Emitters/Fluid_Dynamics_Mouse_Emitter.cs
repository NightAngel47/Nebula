using UnityEngine;
namespace FluidDynamics
{
    [AddComponentMenu("Fluid Dynamics/Emitters/Fluid Mouse Emitter")]
    public class Fluid_Dynamics_Mouse_Emitter : MonoBehaviour
    {
        #region Variables
        public Main_Fluid_Simulation m_fluid;
        public states updateMode;
        public bool m_alwaysOn = false;
        private Vector3 m_previousMousePosition;
        [HideInInspector]
        public float m_velocityStrength = 10f;
        [HideInInspector]
        public float m_velocityRadius = 5f;
        [HideInInspector]
        public float m_particlesStrength = 1f;
        [HideInInspector]
        public float m_particlesRadius = 5f;
        private Collider m_tempCol;
        private Renderer m_tempRend;
        private Ray ray;
        private RaycastHit hitInfo;
        private float fWidth;
        private float fRadius;
        private Vector3 direction;
        private Vector3 m_mousePos;
        #endregion
        private void Start()
        {
            m_tempCol = m_fluid.GetComponent<Collider>();
            m_tempRend = m_fluid.GetComponent<Renderer>();
        }
        private void DrawGizmo()
        {
            float col = m_particlesStrength / 10000.0f;
            Gizmos.color = Color.Lerp(Color.yellow, Color.red, col);
            Gizmos.DrawWireSphere(transform.position, m_particlesRadius);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, m_velocityRadius);
        }
        private void OnDrawGizmosSelected()
        {
            DrawGizmo();
        }
        private void OnDrawGizmos()
        {
            DrawGizmo();
        }
        private void Update()
        {
            if (updateMode == states.Update)
                ManipulateParticles();
        }
        private void LateUpdate()
        {
            if (updateMode == states.LateUpdate)
                ManipulateParticles();
        }
        private void FixedUpdate()
        {
            if (updateMode == states.FixedUpdate)
                ManipulateParticles();
        }
        private void ManipulateParticles()
        {
            if (Input.GetMouseButton(0) || m_alwaysOn)
            {
                m_mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
                ray = Camera.main.ScreenPointToRay(m_mousePos);
                if (m_tempCol.Raycast(ray, out hitInfo, 100))
                {
                    fWidth = m_tempRend.bounds.extents.x * 2f;
                    fRadius = (m_particlesRadius * m_fluid.GetParticlesWidth()) / fWidth;
                    m_fluid.AddParticles(hitInfo.textureCoord, fRadius, m_particlesStrength * Time.deltaTime);
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                m_previousMousePosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(1) || m_alwaysOn)
            {
                m_mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
                ray = Camera.main.ScreenPointToRay(m_mousePos);
                if (m_tempCol.Raycast(ray, out hitInfo, 100))
                {
                    direction = (Input.mousePosition - m_previousMousePosition) * m_velocityStrength * Time.deltaTime;
                    fWidth = m_tempRend.bounds.extents.x * 2f;
                    fRadius = (m_velocityRadius * m_fluid.GetWidth()) / fWidth;

                    if (Input.GetMouseButton(0))
                    {
                        m_fluid.AddVelocity(hitInfo.textureCoord, -direction, fRadius);
                    }
                    else
                    {
                        m_fluid.AddVelocity(hitInfo.textureCoord, direction, fRadius);

                    }
                }
                m_previousMousePosition = Input.mousePosition;
            }
        }
    }
}
using UnityEngine;
namespace FluidDynamics
{
    [AddComponentMenu("Fluid Dynamics/Emitters/Fluid Touch Emitter")]
    public class Fluid_Dynamics_Touch_Emitter : MonoBehaviour
    {
        private Vector3 m_previousMousePosition;
        public float m_velocityStrength = 10f;
        public float m_velocityRadius = 5f;
        public float m_particlesStrength = 1f;
        public float m_particlesRadius = 5f;
        public Main_Fluid_Simulation m_fluid;
        private Collider m_tempCol;
        private Renderer m_tempRend;
        private Renderer m_tempRend1;
        private Touch touch;
        private Ray ray;
        private RaycastHit hitInfo;
        private Vector3 direction;
        private float fWidth;
        private float fRadius;
        private void Start()
        {
            m_tempCol = m_fluid.GetComponent<Collider>();
            m_tempRend = m_fluid.GetComponent<Renderer>();
            m_tempRend1 = m_fluid.GetComponent<Renderer>();
        }
        private void LateUpdate()
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                touch = Input.GetTouch(i);
                ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0f));
                if (m_tempCol.Raycast(ray, out hitInfo, 100))
                {
                    fWidth = m_tempRend.bounds.extents.x * 2f;
                    fRadius = (m_particlesRadius * m_fluid.GetParticlesWidth()) / fWidth;
                    m_fluid.AddParticles(hitInfo.textureCoord, fRadius, m_particlesStrength * Time.deltaTime);
                    if (touch.phase == TouchPhase.Moved)
                    {
                        direction = new Vector3(touch.deltaPosition.x, touch.deltaPosition.y) * m_velocityStrength * touch.deltaTime;
                        fWidth = m_tempRend1.bounds.extents.x * 2f;
                        fRadius = (m_velocityRadius * m_fluid.GetWidth()) / fWidth;
                        m_fluid.AddVelocity(hitInfo.textureCoord, direction, fRadius);
                    }
                }
            }
        }
    }
}

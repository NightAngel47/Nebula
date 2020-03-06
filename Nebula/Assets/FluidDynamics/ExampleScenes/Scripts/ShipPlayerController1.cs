using UnityEngine;

public class ShipPlayerController1 : MonoBehaviour
{
    public bool autoMove;
	public float m_speed = 1f;
	public float m_rotationSpeed = 1f;
    public BoxCollider2D m_moveArea;
    float ttimer;
    float tt2;
	void Update()
	{
        if (autoMove)
        {
            ttimer = Time.time;
            if (ttimer > tt2)
            {
                tt2 = ttimer + 1;
                m_speed = Random.Range(0.1f, 2f);
            }
            Vector3 dir = transform.rotation * Vector3.up;
            transform.position += dir * m_speed * Time.deltaTime;

        }
        
        if (!m_moveArea.bounds.Contains(transform.position))
        {
            Vector3 new_pos = transform.position;
            if (transform.position.x > m_moveArea.bounds.max.x)
            {
                new_pos.x = m_moveArea.bounds.min.x;
            }
            else if (transform.position.x < m_moveArea.bounds.min.x)
            {
                new_pos.x = m_moveArea.bounds.max.x;
            }

            if (transform.position.y > m_moveArea.bounds.max.y)
            {
                new_pos.y = m_moveArea.bounds.min.y;
            }
            else if (transform.position.y < m_moveArea.bounds.min.y)
            {
                new_pos.y = m_moveArea.bounds.max.y;
            }

            transform.position = new_pos;
        }
	}
}
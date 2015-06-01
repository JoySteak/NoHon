using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
	public static PoolManager current;

	public GameObject m_pooledObject;
	public int m_amountOfPooledObject = 20;
	public bool m_willGrow = true;

	public List<GameObject> m_pool;

	void Awake()
	{
		current = this;
	}

	// Use this for initialization
	void Start()
	{
		m_pool = new List<GameObject>();
		for(int i = 0; i < m_amountOfPooledObject; i++)
		{
			GameObject bulletInstance = Instantiate(m_pooledObject) as GameObject;
			bulletInstance.SetActive(false);
			m_pool.Add(bulletInstance);
		}
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public GameObject GetBullet()
	{
		for (int i = 0; i < m_amountOfPooledObject; i++)
		{
			if(m_pool[i].activeSelf == false)
			{
				return m_pool[i];
			}

		}

        if(m_willGrow)
        {
            GameObject bulletInstance = Instantiate(m_pooledObject) as GameObject;
            m_pool.Add(bulletInstance);
            
            return bulletInstance;
        }

		return null;
	}
}

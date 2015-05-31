using UnityEngine;
using System.Collections;

public class ComponentHealth : MonoBehaviour
{

	[SerializeField]
	protected int m_MaxHP = 10;
	
	protected int m_CurrHP;
	
	// Getter
	public int CurrHP {	get	{ return m_CurrHP; } }
	public int MaxHP {	get	{ return m_MaxHP; } }
	public float FractionHP { get { return (float)(m_CurrHP)/(float)(m_MaxHP); } }
	
	void Start()
	{
		m_CurrHP = m_MaxHP;
	}
	
	public void HPModifier(int amount)
	{
		m_CurrHP += amount;
		
		if (m_CurrHP > m_MaxHP)
		{
			m_CurrHP = m_MaxHP;
		}
		else if (m_CurrHP <= 0)
		{
			Die();
		}
	}
	
	public void Set(int amount)
	{
		m_CurrHP = amount;
		HPModifier(0); // run thru the checks in HPModifier()
	}
	
	public void Die()
	{
		// Fix for bullet being too fast before enemy instance is being destroyed
		if(m_CurrHP < 0)
			return;
		
//		// Is this an enemy instance ?
//		if(this.gameObject.name.Contains("Enemy"))
//			// Yes, check if there's enemy left and pass the spawnPoint for new Enemy instantiation
//			GameManager.current.spawnEnemyCheck(this.gameObject.GetComponent<ComponentEnemyMovement>().spawnPoint);
//		
//		// Is this Player instance ?
//		if(this.gameObject.name == "Player")
//		{
//			// Game Over!
//			GameManager.current.gameOverText.enabled = true;
//			GameManager.current.gameEnd();
//		}
		
		Destroy(this.gameObject);
	}
}

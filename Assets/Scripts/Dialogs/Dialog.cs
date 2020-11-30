/* Author : Raphaël Marczak - 2018/2020, for MIAMI Teaching (IUT Tarbes)
 * 
 * This work is licensed under the CC0 License. 
 * 
 */

using UnityEngine;

public class Dialog : MonoBehaviour
{
	public Message m_dialogWithPlayer;

	public Message GetDialog()
	{
		return m_dialogWithPlayer;
	}

	// Définie le dialogue à afficher
	public void SetDialog(Message dialogToAdd)
	{
		m_dialogWithPlayer = dialogToAdd;
	}
}

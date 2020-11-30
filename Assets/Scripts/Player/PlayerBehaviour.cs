using UnityEngine;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
	private Dialog m_closestNPCDialog = null; // Contient le dialogue du NPC à coté du joueur
	public DialogManager m_dialogDisplayer = null;
	public TextMeshProUGUI InfoMessage = null;

	void Awake() //Définir les variables de controle
	{
		m_closestNPCDialog = null;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (m_closestNPCDialog != null)
			{
				m_dialogDisplayer.SetDialog(m_closestNPCDialog.GetDialog());
			}
		}
	}


	private void OnTriggerEnter(Collider collision)
	{
		if (collision.GetComponent<Dialog>())
		{
			m_closestNPCDialog = collision.GetComponent<Dialog>();
			InfoMessage.enabled = true;
		}
	}

	private void OnTriggerExit(Collider collision)
	{
		if (collision.GetComponent<Dialog>())
		{
			m_closestNPCDialog = null;
			InfoMessage.enabled = false;
		}
	}
}

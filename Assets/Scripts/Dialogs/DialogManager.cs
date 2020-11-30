/* Author : Raphaël Marczak - 2018/2020, for MIAMI Teaching (IUT Tarbes)
 * 
 * This work is licensed under the CC0 License. 
 * 
 */

using System.Collections;
using System;
using UnityEngine;
using TMPro;


// S'occupe d'afficher un dialogue
public class DialogManager : MonoBehaviour
{
	public TextMeshProUGUI renderText;                                             // Texte dans lequel le dialogue est écrit

	// Si un dialogue est actuellement à l'écran
	public bool IsOnScreen => gameObject.activeSelf;

	private Coroutine _writeDialogue = null;                            // Coroutine du texte permettant de l'arrêter
	private Message dialogToDisplay;                                    // Message à écrire

	public Action OnEndDialogue;                                        // Évènement de fin de dialogue
	public bool Switch;

	// Update is called once per frame
	void Update()
	{
		if (renderText == null)
		{
			gameObject.SetActive(false);
		}

		// Cache le message avec la barre espace
		if (Input.GetKeyDown(KeyCode.Space))
		{
			DisplayDialog();
		}
	}

	// Définie le dialogue à afficher
	public void SetDialog(Message dialogToAdd)
	{
		dialogToDisplay = dialogToAdd;

		if (renderText != null)
		{
			renderText.text = "";
		}
		Switch = true;
		gameObject.SetActive(true);
		DisplayDialog();
	}

	private void DisplayDialog()
	{
		if (Switch)
		{
			Switch = false;

			// Efface la coroutine déjà présente si le joueur skip le texte
			if (_writeDialogue != null)
			{
				StopCoroutine(_writeDialogue);
			}

			_writeDialogue = StartCoroutine(WriteDialogue());

		}
		else
		{
			EndDialogue();
			gameObject.SetActive(false);
		}
	}

	private IEnumerator WriteDialogue()
	{
		renderText.text = "";
		foreach (var letter in dialogToDisplay.GetMessageContent)
		{
			renderText.text += letter;
			yield return new WaitForEndOfFrame();
		}
	}

	public void EndDialogue() => OnEndDialogue?.Invoke();
}

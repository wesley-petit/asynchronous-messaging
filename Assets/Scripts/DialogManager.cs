/* Author : Raphaël Marczak - 2018/2020, for MIAMI Teaching (IUT Tarbes)
 * 
 * This work is licensed under the CC0 License. 
 * 
 */

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Structure représentant la page d'un dialogue
// (texte et la couleur)
[System.Serializable]
public struct DialogPage
{
	public string text;
	public Color color;
}

// S'occupe d'afficher un dialogue
public class DialogManager : MonoBehaviour
{
	public Text renderText;                                             // Texte dans lequel le dialogue est écrit

	// Si un dialogue est actuellement à l'écran
	public bool IsOnScreen => gameObject.activeSelf;
	private bool IsThereAnotherDialog => dialogToDisplay.Count > 0;     // S'il y a un autre dialogue à afficher

	private Coroutine _writeDialogue = null;                            // Coroutine du texte permettant de l'arrêter
	private List<DialogPage> dialogToDisplay;                           // Dialogues à écrire

	public Action OnEndDialogue;                                        // Évènement de fin de dialogue

	// Update is called once per frame
	void Update()
	{
		if (renderText == null)
		{
			gameObject.SetActive(false);
		}

		// Supprime la premiére page quand le joueur appuie sur la barre espace
		if (Input.GetKeyDown(KeyCode.Space))
		{
			dialogToDisplay.RemoveAt(0);    //On suprrime un emplacement de la liste avec son contenu
			DisplayDialog();
		}
	}

	// Définie le dialogue à afficher
	public void SetDialog(List<DialogPage> dialogToAdd)
	{
		dialogToDisplay = new List<DialogPage>(dialogToAdd);

		if (dialogToDisplay.Count > 0)
		{
			if (renderText != null)
			{
				renderText.text = "";
			}

			gameObject.SetActive(true);
			DisplayDialog();
		}
	}

	private void DisplayDialog()
	{
		// Affiche un dialogue tant qu'il en reste dans la liste
		if (IsThereAnotherDialog)
		{
			// Efface la coroutine déjà présente si le joueur skip le texte
			if (_writeDialogue != null)
			{
				StopCoroutine(_writeDialogue);
			}

			_writeDialogue = StartCoroutine(WriteDialogue());
			renderText.color = dialogToDisplay[0].color;    //RenderText.color récupère la couleur qu'il y a dans la liste
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
		foreach (var letter in dialogToDisplay[0].text)
		{
			renderText.text += letter;
			yield return new WaitForEndOfFrame();
		}
	}

	public void EndDialogue() => OnEndDialogue?.Invoke();
}

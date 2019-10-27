using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AnimeKeepers
{
    /// <summary>
    /// Handles Character Selection in the Changing Room Scene
    /// </summary>
    public class CharacterSelector : MonoBehaviour
    {
        [System.Serializable]
        public class CharacterSelectionDatabase
        {
            public GameObject character;
            public int choiceNumber;
            public Vector3 startRotation;
        }

        public CharacterSelectionDatabase[] characterSelector;
        public TextMeshProUGUI characterSelectionText;
        public CharacterCustomizer charCustomization;

        public GameObject intialViewObject;

        private int characterChoiceNumber = 0;

        private void Awake()
        {
            foreach (CharacterSelectionDatabase character in characterSelector)
            {
                character.startRotation = new Vector3(0, 180, 0);
            }
        }

        public void SelectCharacter(int characterSelectionNumber)
        {
            characterChoiceNumber = characterSelectionNumber;

            for (int i = 0; i < characterSelector.Length; i++)
            {
                if (characterSelector[i].choiceNumber == characterChoiceNumber)
                {
                    characterSelectionText.text = characterSelector[i].character.name.ToString();
                }
            }
        }

        public void FinalizeCharacterSelection()
        {
            // Player hasn't chosen a character, but is trying to select a character-- Throw an error message
            if (characterChoiceNumber == 0)
            {
                characterSelectionText.text = "Please select a character first!";
            }
            // Player has selected a character, now finalize the choice.
            else
            {
                for (int i = 0; i < characterSelector.Length; i++)
                {
                    if (characterSelector[i].choiceNumber == characterChoiceNumber)
                    {
                        characterSelector[i].character.transform.rotation = Quaternion.Euler(characterSelector[i].startRotation);
                        characterSelector[i].character.SetActive(true);
                        characterSelectionText.text = characterSelector[i].character.name.ToString();
                        charCustomization.chosenCharacter = characterSelector[i].character.transform;
                        charCustomization.FindCustomizableBodyParts();
                    }
                    else
                    {
                        characterSelector[i].character.SetActive(false);
                    }
                }

                ChangeUIView();
            }
        }

        public void ChangeUIView()
        {
            intialViewObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}

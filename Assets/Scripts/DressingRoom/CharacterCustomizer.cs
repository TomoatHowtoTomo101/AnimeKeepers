using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AnimeKeepers
{
    public class CharacterCustomizer : MonoBehaviour
    {
        public Transform chosenCharacter;
        private CustomizableBodyPart[] customizableBodyParts;

        public TMP_Dropdown dropdownMenu;
        public Slider[] customizerSliders;
        public TextMeshProUGUI errorMessageText;

        public GameObject customizeSettings;
        public GameObject initialView;

        public void FindCustomizableBodyParts()
        {
            customizableBodyParts = chosenCharacter.GetComponentsInChildren<CustomizableBodyPart>(true);
        }

        public void UpdateBodyPartScale(Slider slider)
        {
            switch (dropdownMenu.value)
            {
                case 0: // No selection
                    break;

                case 1: // Boob
                    for (int i = 0; i < customizableBodyParts.Length; i++)
                    {
                        if (customizableBodyParts[i].customizablePart == CustomizableBodyPart.BodyPart.Boobs)
                        {
                            customizableBodyParts[i].transform.localScale = new Vector3(slider.value, slider.value, slider.value);
                        }
                    }
                    break;

                case 2: // Butt
                    for (int i = 0; i < customizableBodyParts.Length; i++)
                    {
                        if (customizableBodyParts[i].customizablePart == CustomizableBodyPart.BodyPart.Butt)
                        {
                            customizableBodyParts[i].transform.localScale = new Vector3(slider.value, slider.value, slider.value);
                        }
                    }
                    break;

                case 3: // Hair
                    for (int i = 0; i < customizableBodyParts.Length; i++)
                    {
                        if (customizableBodyParts[i].customizablePart == CustomizableBodyPart.BodyPart.Hair)
                        {
                            customizableBodyParts[i].transform.localScale = new Vector3(slider.value, slider.value, slider.value);
                        }
                    }
                    break;
            }
        }

        public void SelectBodyPartToCustomize()
        {
            switch (dropdownMenu.value)
            {
                case 0: // No selection
                    foreach (Slider slider in customizerSliders)
                    {
                        slider.gameObject.SetActive(false);
                    }
                    break;

                case 1: // Boob
                    foreach (Slider slider in customizerSliders)
                    {
                        slider.gameObject.SetActive(false);
                    }

                    customizerSliders[0].gameObject.SetActive(true);
                    break;

                case 2: // Butt
                    foreach (Slider slider in customizerSliders)
                    {
                        slider.gameObject.SetActive(false);
                    }

                    customizerSliders[1].gameObject.SetActive(true);
                    break;

                case 3: // Hair
                    foreach (Slider slider in customizerSliders)
                    {
                        slider.gameObject.SetActive(false);
                    }

                    customizerSliders[2].gameObject.SetActive(true);
                    break;
            }
        }

        public void TryToCustomize()
        {
            if (chosenCharacter != null)
            {
                customizeSettings.SetActive(true);
                initialView.SetActive(false);
            }
            else
            {
                SetErrorMessage("Choose a Character first!");
            }
        }

        public void SetErrorMessage(string message)
        {
            errorMessageText.text = message;
            errorMessageText.GetComponent<Animator>().Play("FadeText");
        }
    }
}

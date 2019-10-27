using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimeKeepers
{
    public class CustomizableBodyPart : MonoBehaviour
    {
        [System.Serializable]
        public enum BodyPart
        {
            Boobs,
            Butt,
            Hair,
        }

        public BodyPart customizablePart;
    }
}

// Animancer // Copyright 2019 Kybernetik //

#pragma warning disable IDE0018 // Inline variable declaration.

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Animancer
{
    /// <summary>Plays a single <see cref="AnimationClip"/> on startup.</summary>
    [AddComponentMenu("Animancer/Solo Animation")]
    [HelpURL(AnimancerPlayable.APIDocumentationURL + "/SoloAnimation")]
    [DefaultExecutionOrder(-5000)]// Initialise before anything else tries to use this component.
    public sealed class SoloAnimation : SoloAnimationInternal { }
}

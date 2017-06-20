// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace HoloToolkit.Unity.InputModule.Tests
{
    /// <summary>
    /// This class implements IFocusable to respond to gaze changes.
    /// It highlights the object being gazed at.
    /// </summary>
    public class HighlightGrid : MonoBehaviour, IFocusable
    {
        public Color selectColor;
        public Color deselectColor;
        public Color hitColor;
        public Color missColor;

        private MeshRenderer thisRenderer;

        private void Start()
        {
            thisRenderer = this.GetComponent<MeshRenderer>();
        }

        public void OnFocusEnter()
        {
            thisRenderer.material.color = selectColor;
        }

        public void OnFocusExit()
        {
            thisRenderer.material.color = deselectColor;
        }
    }
}
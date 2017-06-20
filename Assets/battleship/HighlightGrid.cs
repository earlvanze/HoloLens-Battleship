// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace HoloToolkit.Unity.InputModule.Tests
{
    /// <summary>
    /// This class implements IFocusable to respond to gaze changes.
    /// It highlights the object being gazed at.
    /// </summary>
    public class HighlightGrid : MonoBehaviour, IFocusable, IInputClickHandler
    {
        public int row;
        public int column;
        public bool isFireBoard;

        public Color selectColor;
        public Color deselectColor;
        public Color hitColor;
        public Color missColor;

        private MeshRenderer thisRenderer;
        private GameLogic gameLogic;

        private void Start()
        {
            thisRenderer = this.GetComponent<MeshRenderer>();
            gameLogic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
        }

        public void OnFocusEnter()
        {
            thisRenderer.material.color = selectColor;
        }

        public void OnFocusExit()
        {
            thisRenderer.material.color = deselectColor;
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            if (isFireBoard)
            {
                if (gameLogic.playerFire(row, column))
                {
                    thisRenderer.material.color = hitColor;
                }
                else
                {
                    thisRenderer.material.color = missColor;
                }
            }
        }
    }
}
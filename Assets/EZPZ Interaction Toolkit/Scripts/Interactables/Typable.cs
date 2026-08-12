using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using System.Security.Cryptography;
using System;

public class Typable : InteractableGeneral
{
    [Header("Basic Typing Settings")]
    public string cursorText = "_";
    public bool releaseOnEnterKey = true;
    public UnityEvent onReleaseTyping;
    public UnityEvent onEnterKeyNotForWebGL;
    public UnityEvent onTextInput;

    [Header("Text Matching")]
    public string matchText;
    public UnityEvent onTextMatch;
    public TextMatchRelay textMatchRelay;

    [Header("System Stuff - Usually Don't Touch")]
    public string typeTextBuffer;
    public bool typeCapture;
    public TextMeshProUGUI textDisplay;

    public RaycastInteractor raycastInteractor;

    private int lastDeleteFrame = -1;

    private void Start()
    {
        Keyboard.current.onTextInput += OnTextInput;
    }

    private void Update()
    {
        if (!typeCapture || Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current.backspaceKey.wasPressedThisFrame ||
            Keyboard.current.deleteKey.wasPressedThisFrame)
        {
            DeleteLastCharacter();
        }
    }

    public void OnMouseDown()
    {
        raycastInteractor.ReleaseFromTyping();
    }

    private void OnTextInput(char ch)
    {
        if (!typeCapture)
        {
            return;
        }

        if (ch == '\b' || ch == 127)
        {
            DeleteLastCharacter();
            return;
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            Debug.Log("enterKey.wasPressed");
            HandleEnterKey();
        }
        else if (ch == '')
        {
            raycastInteractor.ReleaseFromTyping();
        }
        else if (ch == '`')
        {
            onReleaseTyping.Invoke();
            raycastInteractor.ReleaseFromTyping();
        }
        else
        {
            typeTextBuffer += ch;
        }

        SyncText();
        onTextInput.Invoke();
    }

    private void DeleteLastCharacter()
    {
        // Prevent double deletion if WebGL and onTextInput
        // report the same key during one frame.
        if (lastDeleteFrame == Time.frameCount)
        {
            return;
        }

        lastDeleteFrame = Time.frameCount;

        if (typeTextBuffer.Length > 0)
        {
            typeTextBuffer = typeTextBuffer.Substring(
                0,
                typeTextBuffer.Length - 1
            );
        }

        SyncText();
        onTextInput.Invoke();
    }

    public void HandleEnterKey()
    {
        if (releaseOnEnterKey)
        {
            raycastInteractor.ReleaseFromTyping();
        }
        else
        {
            typeTextBuffer += '\n';
        }

        onEnterKeyNotForWebGL.Invoke();
    }

    public void ClearTypeBuffer()
    {
        typeTextBuffer = "";
        SyncText();
    }

    public void SyncText()
    {
        Debug.Log(name + " SyncText() " + typeTextBuffer);

        if (typeCapture)
        {
            textDisplay.text = typeTextBuffer + cursorText;

            if (typeTextBuffer.Length > 0)
            {
                if (typeTextBuffer.Equals(matchText))
                {
                    onTextMatch.Invoke();

                    if (raycastInteractor != null)
                    {
                        raycastInteractor.ReleaseFromTyping();
                    }
                }
            }

            if (textMatchRelay != null)
            {
                textMatchRelay.CheckMatch();
            }
        }
        else
        {
            textDisplay.text = typeTextBuffer;
        }
    }
}
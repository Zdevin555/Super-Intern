﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;


namespace TMPro
{

    public class TMP_TextEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Serializable]
        public class CharacterSelectionEvent : UnityEvent<char, int> { }

        [Serializable]
        public class SpriteSelectionEvent : UnityEvent<char, int> { }

        [Serializable]
        public class WordSelectionEvent : UnityEvent<string, int, int> { }

        [Serializable]
        public class LineSelectionEvent : UnityEvent<string, int, int> { }

        [Serializable]
        public class LinkSelectionEvent : UnityEvent<string, string, int> { }


           public CharacterSelectionEvent onCharacterSelection
        {
            get { return m_OnCharacterSelection; }
            set { m_OnCharacterSelection = value; }
        }
        [SerializeField]
        private CharacterSelectionEvent m_OnCharacterSelection = new CharacterSelectionEvent();


           public SpriteSelectionEvent onSpriteSelection
        {
            get { return m_OnSpriteSelection; }
            set { m_OnSpriteSelection = value; }
        }
        [SerializeField]
        private SpriteSelectionEvent m_OnSpriteSelection = new SpriteSelectionEvent();


           public WordSelectionEvent onWordSelection
        {
            get { return m_OnWordSelection; }
            set { m_OnWordSelection = value; }
        }
        [SerializeField]
        private WordSelectionEvent m_OnWordSelection = new WordSelectionEvent();


           public LineSelectionEvent onLineSelection
        {
            get { return m_OnLineSelection; }
            set { m_OnLineSelection = value; }
        }
        [SerializeField]
        private LineSelectionEvent m_OnLineSelection = new LineSelectionEvent();


           public LinkSelectionEvent onLinkSelection
        {
            get { return m_OnLinkSelection; }
            set { m_OnLinkSelection = value; }
        }
        [SerializeField]
        private LinkSelectionEvent m_OnLinkSelection = new LinkSelectionEvent();



        private TMP_Text m_TextComponent;

        private Camera m_Camera;
        private Canvas m_Canvas;

        private int m_selectedLink = -1;
        private int m_lastCharIndex = -1;
        private int m_lastWordIndex = -1;
        private int m_lastLineIndex = -1;

        void Awake()
        {
             m_TextComponent = gameObject.GetComponent<TMP_Text>();

             if (m_TextComponent.GetType() == typeof(TextMeshProUGUI))
            {
                m_Canvas = gameObject.GetComponentInParent<Canvas>();
                if (m_Canvas != null)
                {
                    if (m_Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                        m_Camera = null;
                    else
                        m_Camera = m_Canvas.worldCamera;
                }
            }
            else
            {
                m_Camera = Camera.main;
            }
        }


        void LateUpdate()
        {
            if (TMP_TextUtilities.IsIntersectingRectTransform(m_TextComponent.rectTransform, Input.mousePosition, m_Camera))
            {
                #region Example of Character or Sprite Selection
                int charIndex = TMP_TextUtilities.FindIntersectingCharacter(m_TextComponent, Input.mousePosition, m_Camera, true);
                if (charIndex != -1 && charIndex != m_lastCharIndex)
                {
                    m_lastCharIndex = charIndex;

                    TMP_TextElementType elementType = m_TextComponent.textInfo.characterInfo[charIndex].elementType;

                     if (elementType == TMP_TextElementType.Character)
                        SendOnCharacterSelection(m_TextComponent.textInfo.characterInfo[charIndex].character, charIndex);
                    else if (elementType == TMP_TextElementType.Sprite)
                        SendOnSpriteSelection(m_TextComponent.textInfo.characterInfo[charIndex].character, charIndex);
                }
                #endregion


                #region Example of Word Selection
                 int wordIndex = TMP_TextUtilities.FindIntersectingWord(m_TextComponent, Input.mousePosition, m_Camera);
                if (wordIndex != -1 && wordIndex != m_lastWordIndex)
                {
                    m_lastWordIndex = wordIndex;

                     TMP_WordInfo wInfo = m_TextComponent.textInfo.wordInfo[wordIndex];

                     SendOnWordSelection(wInfo.GetWord(), wInfo.firstCharacterIndex, wInfo.characterCount);
                }
                #endregion


                #region Example of Line Selection
                 int lineIndex = TMP_TextUtilities.FindIntersectingLine(m_TextComponent, Input.mousePosition, m_Camera);
                if (lineIndex != -1 && lineIndex != m_lastLineIndex)
                {
                    m_lastLineIndex = lineIndex;

                     TMP_LineInfo lineInfo = m_TextComponent.textInfo.lineInfo[lineIndex];

                     char[] buffer = new char[lineInfo.characterCount];
                    for (int i = 0; i < lineInfo.characterCount && i < m_TextComponent.textInfo.characterInfo.Length; i++)
                    {
                        buffer[i] = m_TextComponent.textInfo.characterInfo[i + lineInfo.firstCharacterIndex].character;
                    }

                    string lineText = new string(buffer);
                    SendOnLineSelection(lineText, lineInfo.firstCharacterIndex, lineInfo.characterCount);
                }
                #endregion


                #region Example of Link Handling
                 int linkIndex = TMP_TextUtilities.FindIntersectingLink(m_TextComponent, Input.mousePosition, m_Camera);

                 if (linkIndex != -1 && linkIndex != m_selectedLink)
                {
                    m_selectedLink = linkIndex;

                     TMP_LinkInfo linkInfo = m_TextComponent.textInfo.linkInfo[linkIndex];

                     SendOnLinkSelection(linkInfo.GetLinkID(), linkInfo.GetLinkText(), linkIndex);
                }
                #endregion
            }
            else
            {
                 m_selectedLink = -1;
                m_lastCharIndex = -1;
                m_lastWordIndex = -1;
                m_lastLineIndex = -1;
            }
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
         }


        public void OnPointerExit(PointerEventData eventData)
        {
         }


        private void SendOnCharacterSelection(char character, int characterIndex)
        {
            if (onCharacterSelection != null)
                onCharacterSelection.Invoke(character, characterIndex);
        }

        private void SendOnSpriteSelection(char character, int characterIndex)
        {
            if (onSpriteSelection != null)
                onSpriteSelection.Invoke(character, characterIndex);
        }

        private void SendOnWordSelection(string word, int charIndex, int length)
        {
            if (onWordSelection != null)
                onWordSelection.Invoke(word, charIndex, length);
        }

        private void SendOnLineSelection(string line, int charIndex, int length)
        {
            if (onLineSelection != null)
                onLineSelection.Invoke(line, charIndex, length);
        }

        private void SendOnLinkSelection(string linkID, string linkText, int linkIndex)
        {
            if (onLinkSelection != null)
                onLinkSelection.Invoke(linkID, linkText, linkIndex);
        }

    }
}

using System.Collections;
using TMPro;
using UnityEngine;

namespace Wanko
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class TypewriterEffect : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private Coroutine _typewriterCoroutine;
        private WaitForSeconds _characterDelay;
        private WaitForSeconds _interpunctuationDelay;

        [field: SerializeField]
        public float CharactersPerSecond { get; private set; } = 20f;
        [field: SerializeField]
        public float InterpunctuationDelay { get; private set; } = 0.5f;

        private void Start()
        {
            _text = GetComponent<TMP_Text>();
            _characterDelay = new WaitForSeconds(1 / CharactersPerSecond);
            _interpunctuationDelay = new WaitForSeconds(InterpunctuationDelay);
        }

        public void SetText(string text)
        {
            if (_typewriterCoroutine != null)
                StopCoroutine(_typewriterCoroutine);

            _typewriterCoroutine = StartCoroutine(Typewriter(text));
        }

        private IEnumerator Typewriter(string text)
        {
            _text.text = text;
            _text.maxVisibleCharacters = 0;

            TMP_TextInfo textInfo = _text.textInfo;

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                char character = textInfo.characterInfo[i].character;
                _text.maxVisibleCharacters++;

                yield return character is '.' or '!' or '?' or ',' or ':' or ';' or '-'
                    ? _interpunctuationDelay
                    : _characterDelay;
            }
        }
    }
}
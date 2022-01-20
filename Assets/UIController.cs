using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Homework
{


    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject startScreen;
        [SerializeField] private GameObject completeScreen;

        private CanvasGroup startCanvasGroup;
        private CanvasGroup completeCanvasGroup;


        private float _interval = 0.1f;
        private float _nextTime = 0f;
        private float _delay = 0f;
        private float _startTime = 0f;
        private bool _isGameStart, _isGameFinish;

        private void Start()
        {
            startCanvasGroup = startScreen.GetComponent<CanvasGroup>();
            completeCanvasGroup = completeScreen.GetComponent<CanvasGroup>();

        }
        private void Update()
        {
            ChangeAlphaUpdate();
        }
        public void OpenMenu()
        {
            completeScreen.SetActive(false);
            startScreen.SetActive(true);
            startCanvasGroup.alpha = 1f;
            _isGameStart = false;
            _isGameFinish = false;
        }
        public void OpenGame()
        {
            completeScreen.SetActive(false);
            startScreen.SetActive(true);
            _isGameStart = true;
            _isGameFinish = false;
            _startTime = Time.time;
            _delay = _startTime + 0.5f;
            _nextTime = _startTime;
        }
        public void OpenComplete()
        {
            startScreen.SetActive(false);
            completeScreen.SetActive(true);
            _isGameStart = false;
            _isGameFinish = true;
            _startTime = Time.time;
            _delay = _startTime + 0.5f;
            _nextTime = _startTime;
        }
        private void ChangeAlphaUpdate()
        {
            if (_isGameStart && Time.time >= _nextTime && Time.time <= _delay)
            {
                _nextTime += _interval;
                startCanvasGroup.alpha -= 0.2f;

            }
            if (_isGameFinish && Time.time >= _nextTime && Time.time <= _delay)
            {
                _nextTime += _interval;
                completeCanvasGroup.alpha += 0.2f;

            }
        }
        /* private IEnumerable ChangeAlpha(float sec)
         {

         }*/
    }
}
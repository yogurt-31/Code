using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace JSY
{
    public class TimeUI : MonoBehaviour
    {
        [SerializeField] private Transform timeUITrm;
        [SerializeField] private Button skipButton;

        private TextMeshProUGUI timeText;
        private Coroutine waveRoutine;
        private Coroutine checkEnemyRoutine;
        private RectTransform rectTrm => timeUITrm as RectTransform;

        private Sequence seq;
        private Sequence bossSeq;

        private void Awake()
        {
            timeText = timeUITrm.Find("Panel").GetComponentInChildren<TextMeshProUGUI>();
            WaveManager.Instance.OnChangeTurnEvent += WaveDelay;
            WaveManager.Instance.OnStartBossTurnEvent += BossTimeLimit;
            skipButton.onClick.AddListener(SkipWaveCoolTime);
        }

        private void WaveDelay()
        {
            bossSeq.Kill();
            skipButton.gameObject.SetActive(true);
            if (waveRoutine != null)
                StopCoroutine(waveRoutine);
            seq = DOTween.Sequence()
            .AppendCallback(() =>
            {
                waveRoutine = StartCoroutine(SetTimePanel(WaveManager.Instance.GetWave().waveDelay));
            })
            .AppendInterval(WaveManager.Instance.GetWave().waveDelay)
            .OnComplete(() =>
            {
                WaveManager.Instance.InvokeStartTurn();
                skipButton.gameObject.SetActive(false);
            });
        }

        private void BossTimeLimit()
        {
            if (waveRoutine != null)
                StopCoroutine(waveRoutine);
            if (checkEnemyRoutine != null)
                StopCoroutine(checkEnemyRoutine);

            waveRoutine = StartCoroutine(SetTimePanel(WaveManager.Instance.GetWave().bossTimeLimit));
            checkEnemyRoutine = StartCoroutine(CheckEnemy());
        }

        private IEnumerator CheckEnemy()
        {
            float startTime = Time.time;
            yield return new WaitForSeconds(1f);
            while (true)
            {
                if (Time.time - startTime >= WaveManager.Instance.GetWave().bossTimeLimit + 1)
                {
                    UIManager.Instance.EnemyCountUI.GameOver();
                }
                yield return new WaitForSeconds(0.5f);
                if (UIManager.Instance.EnemyCountUI.IsAllDead())
                {
                    WaveManager.Instance.TurnEnd();
                    yield break;
                }
                yield return null;
            }
        }

        private IEnumerator SetTimePanel(int time)
        {
            rectTrm.DOAnchorPosY(-100, 0.5f);
            var waitTime = new WaitForSeconds(1f);
            for (int i = 0; i <= time; i++)
            {
                timeText.text = time - i + "초"; 
                yield return waitTime;
            }
            rectTrm.DOAnchorPosY(0, 0.5f);
        }

        public void SkipWaveCoolTime()
        {
            StopCoroutine(waveRoutine);
            rectTrm.DOAnchorPosY(0, 0.5f);
            WaveManager.Instance.InvokeStartTurn();
            skipButton.gameObject.SetActive(false);
            
            seq.Kill();
        }
    }
}

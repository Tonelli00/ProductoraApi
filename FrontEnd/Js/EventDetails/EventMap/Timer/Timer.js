import { TIMER_DURATION } from '../Utils/Constants.js';
import { formatTime } from '../Utils/Helpers.js';


function createTimerBar() {
  const bar = document.createElement('div');
  bar.id = 'seat-timer-bar';
  bar.style.cssText = `
    position: sticky;
    top: 64px;
    z-index: 40;
    display: none;
    align-items: center;
    justify-content: center;
    gap: 10px;
    background: #111827;
    color: #f9fafb;
    padding: 10px 20px;
    font-size: 13px;
    font-weight: 500;
    border-bottom: 1px solid #374151;
    transition: opacity 0.3s;
  `;

  const icon = document.createElement('span');
  icon.textContent = '⏱';
  icon.style.fontSize = '15px';

  const label = document.createElement('span');
  label.textContent = 'Tu asiento está reservado por ';

  const countdown = document.createElement('span');
  countdown.id = 'seat-timer-countdown';
  countdown.style.cssText = `
    font-family: monospace;
    font-size: 14px;
    color: #fbbf24;
    font-weight: 700;
  `;
  countdown.textContent = formatTime(TIMER_DURATION);

  const suffix = document.createElement('span');
  suffix.textContent = ' — completá tu compra antes de que expire.';
  suffix.style.color = '#9ca3af';

  bar.append(icon, label, countdown, suffix);

  const header = document.querySelector('header');
  if (header?.nextSibling) {
    header.parentNode.insertBefore(bar, header.nextSibling);
  } else {
    document.body.prepend(bar);
  }

  return bar;
}

export function createTimer(onExpire) {
  const bar = document.getElementById('seat-timer-bar') ?? createTimerBar();
  const countdown = document.getElementById('seat-timer-countdown');

  let intervalId = null;
  let remaining = TIMER_DURATION;

  function tick() {
    remaining--;
    countdown.textContent = formatTime(remaining);
    countdown.style.color = remaining <= 60 ? '#ef4444' : '#fbbf24';

    if (remaining <= 0) {
      stop();
      onExpire();
    }
  }

  function show() {
    remaining = TIMER_DURATION;
    countdown.textContent = formatTime(remaining);
    countdown.style.color = '#fbbf24';
    bar.style.display = 'flex';
    clearInterval(intervalId);
    intervalId = setInterval(tick, 1000);
  }

  function stop() {
    clearInterval(intervalId);
    intervalId = null;
    bar.style.display = 'none';
    countdown.style.color = '#fbbf24';
  }

  return { show, stop };
}
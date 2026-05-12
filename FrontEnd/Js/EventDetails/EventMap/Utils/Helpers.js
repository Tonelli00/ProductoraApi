
export function chunkSeats(seats = [], size) {
  const rows = [];
  for (let i = 0; i < seats.length; i += size) {
    rows.push(seats.slice(i, i + size));
  }
  return rows;
}

export function formatTime(seconds) {
  const m = Math.floor(seconds / 60);
  const s = seconds % 60;
  return `${m}:${s.toString().padStart(2, '0')}`;
}
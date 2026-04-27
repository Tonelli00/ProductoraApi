export function createEventCard(event) {
  const card = document.createElement('div');
  card.className = `
    bg-white border border-gray-200 rounded-xl overflow-hidden
    hover:-translate-y-0.5 hover:border-gray-400
    transition-all duration-200 cursor-pointer flex flex-col
  `;

  const date = new Date(event.eventDate).toLocaleDateString('es-AR', {
    weekday: 'long', day: 'numeric', month: 'long', year: 'numeric'
  });

  const sectors = (event.sectors ?? [])
    .map(s => `<span class="text-xs font-normal px-2.5 py-1 rounded-full border border-gray-200 bg-gray-50 text-gray-500 whitespace-nowrap">${s.name}</span>`)
    .join('');

  const statusClass = event.status === 'Activo'
    ? 'bg-green-50 text-green-700'
    : 'bg-gray-100 text-gray-400';

  card.innerHTML = `
    <div class="px-6 pt-5 pb-4 border-b border-gray-100 flex flex-col gap-2">
      <p class="text-[11px] font-medium uppercase tracking-widest text-gray-400">${date}</p>
      <h3 class="font-serif text-xl font-normal text-gray-900 leading-snug">${event.name}</h3>
      <p class="text-sm font-light text-gray-400 flex items-center gap-1.5">
        <span class="inline-block w-1 h-1 rounded-full bg-gray-300"></span>
        ${event.venue}
      </p>
    </div>

    ${sectors ? `
    <div class="px-6 py-3 border-b border-gray-100 flex flex-wrap gap-1.5">
      ${sectors}
    </div>` : ''}

    <div class="px-6 py-3.5 flex items-center justify-between mt-auto">
      <span class="text-[11px] font-medium uppercase tracking-wide px-2.5 py-1 rounded-md ${statusClass}">
        ${event.status}
      </span>
      <button class="ver-mas text-[11px] font-medium uppercase tracking-widest text-gray-300 hover:text-gray-900 transition-colors duration-200">
        Ver más →
      </button>
    </div>
  `;

  card.querySelector('.ver-mas').addEventListener('click', (e) => {
    e.stopPropagation();
    localStorage.setItem('event_id', event.id);
    window.location.href = 'Event.html';
  });

  return card;
}
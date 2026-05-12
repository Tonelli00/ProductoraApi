import { SEATS_PER_ROW } from '../Utils/Constants.js';
import { chunkSeats } from '../Utils/Helpers.js';

export function createMapCard(sectors, sectorColors, onSeatSelect) {
  const card = document.createElement('div');
  card.className = 'flex-1 bg-white border border-gray-200 rounded-xl overflow-hidden';
  card.innerHTML = `
    <div class="px-5 py-4 border-b border-gray-100 flex items-center justify-between">
      <span class="text-sm font-medium text-gray-900">Mapa de asientos</span>
      <div class="flex items-center gap-4 text-[11px] text-gray-400">
        <div class="flex items-center gap-1.5">
          <div class="w-3 h-3 rounded-sm bg-blue-100 border border-blue-200"></div>Disponible
        </div>
        <div class="flex items-center gap-1.5">
          <div class="w-3 h-3 rounded-sm bg-blue-500"></div>Seleccionado
        </div>
        <div class="flex items-center gap-1.5">
          <div class="w-3 h-3 rounded-sm bg-gray-200"></div>Reservado
        </div>
      </div>
    </div>
  `;

  const body = document.createElement('div');
  body.className = 'p-6';
  body.innerHTML = `
    <div class="text-center mb-8">
      <span class="inline-block bg-gray-50 border border-gray-200 rounded px-10 py-1.5 text-[10px] uppercase tracking-widest text-gray-400">
        Escenario
      </span>
    </div>
  `;

  const grid = document.createElement('div');
  grid.className = 'flex flex-col gap-6 items-center';

sectors.forEach((sector, idx) => {
  
  const colors = sectorColors[idx % sectorColors.length];

  const sectorBlock = createSectorBlock(
    sector,
    colors,
    (seat, element, color) => onSeatSelect(seat, sector, element, color)
  );
  grid.appendChild(sectorBlock);

  const esElUltimo = idx === sectors.length - 1;
  if (!esElUltimo) {
    const divider = document.createElement('div');
    divider.style.cssText = 'width:100%; border-top: 1px dashed #e5e7eb;';
    grid.appendChild(divider);
  }
});

  body.appendChild(grid);
  card.appendChild(body);
  return card;
}



export function resetSeatStyle(seatElement, colors) {
  seatElement.style.background = colors.bg;
  seatElement.style.border = `1px solid ${colors.border}`;
}


function selectSeatStyle(seatElement, colors) {
  seatElement.style.background = colors.selected;
  seatElement.style.border = `1px solid ${colors.selected}`;
}


function reservedSeatStyle(seatElement) {
  seatElement.style.background = '#e5e7eb';
  seatElement.style.border = '1px solid #d1d5db';
}


function createSeatElement(seat, colors, onSelect) {
  const seatElement = document.createElement('div');
  seatElement.className="w-5 h-4 rounded-[3px] transition-transform transition-opacity duration-100"

  const isReserved = seat.status?.toLowerCase() === 'reserved';

  if (isReserved) {
    reservedSeatStyle(seatElement);
    return seatElement;
  }

  resetSeatStyle(seatElement, colors);
  seatElement.style.cursor = 'pointer';

  seatElement.addEventListener('mouseenter', () => {
    if (seatElement.dataset.selected !== 'true') {
      seatElement.style.transform = 'scale(1.15)';
      seatElement.style.opacity = '0.85';
    }
  });
  seatElement.addEventListener('mouseleave', () => {
    seatElement.style.transform = 'scale(1)';
    seatElement.style.opacity = '1';
  });
  seatElement.addEventListener('click', () => onSelect(seat, seatElement, colors));

  return seatElement;
}

function createSeatRow(seats, colors, onSelect) {
  const rowEl = document.createElement('div');
  rowEl.className = 'flex gap-1';
  seats.forEach(seat => rowEl.appendChild(createSeatElement(seat, colors, onSelect)));
  return rowEl;
}

function createSectorHeader(sector, colors) {
  const header = document.createElement('div');
  header.className = 'flex items-center justify-between px-1';
  header.innerHTML = `
    <div class="flex items-center gap-2">
      <div style="width:10px;height:10px;border-radius:2px;background:${colors.selected};flex-shrink:0;"></div>
      <span class="text-[11px] font-medium text-gray-600">${sector.name}</span>
    </div>
    <span class="text-[11px] text-gray-400">$${sector.price.toLocaleString('es-AR')}</span>
  `;
  return header;
}

function createSectorBlock(sector, colors, onSelect) {
  const block = document.createElement('div');
  block.className = 'w-full flex flex-col gap-2';

  const rowsContainer = document.createElement('div');
  rowsContainer.className = 'flex flex-col gap-1 items-center';

  chunkSeats(sector.seats, SEATS_PER_ROW).forEach(rowSeats => {
    rowsContainer.appendChild(createSeatRow(rowSeats, colors, onSelect));
  });

  block.appendChild(createSectorHeader(sector, colors));
  block.appendChild(rowsContainer);

  return block;
}



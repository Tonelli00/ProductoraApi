import { makeReservation } from "../Reservation/MakeReservation.js";

const SEATS_PER_ROW = 10;

const SECTOR_COLORS = [
  { bg: '#dbeafe', border: '#93c5fd', selected: '#2563eb' }, // azul
  { bg: '#dcfce7', border: '#86efac', selected: '#16a34a' }, // verde
  { bg: '#fef9c3', border: '#fde047', selected: '#ca8a04' }, // amarillo
  { bg: '#fce7f3', border: '#f9a8d4', selected: '#db2777' }, // rosa
  { bg: '#ede9fe', border: '#c4b5fd', selected: '#7c3aed' }, // violeta
];

export function CreateEventMap(event, userId) {
  const wrapper = document.createElement('div');
  wrapper.className = 'flex gap-6 items-start';

  if (!event?.sectors?.length) {
    const empty = document.createElement('p');
    empty.className = 'text-sm text-gray-500';
    empty.textContent = "No hay sectores disponibles";
    wrapper.appendChild(empty);
    return wrapper;
  }

  const mapCard = document.createElement('div');
  mapCard.className = 'flex-1 bg-white border border-gray-200 rounded-xl overflow-hidden';

  mapCard.innerHTML = `
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

  const mapEl = document.createElement('div');
  mapEl.className = 'p-6';
  mapEl.innerHTML = `
    <div class="text-center mb-8">
      <span class="inline-block bg-gray-50 border border-gray-200 rounded px-10 py-1.5 text-[10px] uppercase tracking-widest text-gray-400">
        Escenario
      </span>
    </div>
  `;

  const gridEl = document.createElement('div');
  gridEl.className = 'flex flex-col gap-6 items-center';

  const sidebar = document.createElement('div');
  sidebar.className = 'w-64 bg-white border border-gray-200 rounded-xl overflow-hidden sticky top-24';

  sidebar.innerHTML = `
    <div class="px-5 py-4 border-b border-gray-100">
      <p class="text-xs font-medium uppercase tracking-widest text-gray-400">Tu selección</p>
    </div>
    <div class="p-5 flex flex-col gap-5">

      <div class="sidebar-empty flex flex-col items-center gap-3 py-4">
        <p class="text-xs text-gray-400 text-center">
          Seleccioná un asiento en el mapa para continuar
        </p>
      </div>

      <div class="sidebar-detail hidden flex-col gap-4">
        <div class="bg-gray-50 rounded-lg p-4 flex flex-col gap-3">
          <div class="flex justify-between items-center">
            <span class="text-[10px] text-gray-400">Sector</span>
            <div class="flex items-center gap-1.5">
              <div class="sidebar-sector-pill w-2 h-2 rounded-sm"></div>
              <span class="sidebar-sector text-xs font-medium text-gray-900">—</span>
            </div>
          </div>
          <div class="flex justify-between">
            <span class="text-[10px] text-gray-400">Asiento</span>
            <span class="sidebar-seat text-xs font-medium text-gray-900">—</span>
          </div>
          <div class="flex justify-between">
            <span class="text-[10px] text-gray-400">Total</span>
            <span class="sidebar-price text-sm font-semibold text-gray-900">—</span>
          </div>
        </div>

        <button class="buy-btn w-full py-3 bg-black text-white text-xs rounded-xl">
          Confirmar compra
        </button>

        <button class="clear-btn w-full py-2 text-[11px] text-gray-400">
          Cancelar selección
        </button>
      </div>

    </div>
  `;

  let selected = null;

  const emptyEl = sidebar.querySelector('.sidebar-empty');
  const detailEl = sidebar.querySelector('.sidebar-detail');
  const sectorEl = sidebar.querySelector('.sidebar-sector');
  const sectorPillEl = sidebar.querySelector('.sidebar-sector-pill');
  const seatEl = sidebar.querySelector('.sidebar-seat');
  const priceEl = sidebar.querySelector('.sidebar-price');
  const buyBtn = sidebar.querySelector('.buy-btn');
  const clearBtn = sidebar.querySelector('.clear-btn');

  function resetSeatStyle(el, colors) {
    el.style.background = colors.bg;
    el.style.border = `1px solid ${colors.border}`;
  }

  function selectSeat(seat, sector, el, colors) {
    if (selected) {
      resetSeatStyle(selected.el, selected.colors);
    }

    if (selected?.seatId === seat.seatId) {
      resetSeatStyle(el, colors);
      selected = null;
      showEmpty();
      return;
    }

    el.style.background = colors.selected;
    el.style.border = `1px solid ${colors.selected}`;

    selected = { seatId: seat.seatId, seat, sector, el, colors };
    showDetail(selected);
  }

  function showEmpty() {
    emptyEl.classList.remove('hidden');
    detailEl.classList.add('hidden');
  }

  function showDetail({ seat, sector, colors }) {
    emptyEl.classList.add('hidden');
    detailEl.classList.remove('hidden');

    sectorEl.textContent = sector.name;
    sectorPillEl.style.background = colors.selected;
    seatEl.textContent = `N° ${seat.seatNumber}`;
    priceEl.textContent = `$${sector.price.toLocaleString('es-AR')}`;
  }

  clearBtn.addEventListener('click', () => {
    if (selected) {
      resetSeatStyle(selected.el, selected.colors);
      selected = null;
      showEmpty();
    }
  });

  buyBtn.addEventListener('click', async () => {
    if (!selected || buyBtn.disabled) return;

    const existingError = sidebar.querySelector('.error-msg');
    if (existingError) existingError.remove();

    try {
      await makeReservation(userId, selected.seatId);

      selected.el.style.background = '#e5e7eb';
      selected.el.style.border = '1px solid #d1d5db';
      selected.el.style.cursor = 'default';

      buyBtn.textContent = "¡Compra realizada!";
      buyBtn.classList.add("bg-green-600");
      buyBtn.disabled = true;

      setTimeout(() => {
        buyBtn.textContent = "Actualizando...";
        setTimeout(() => {
          location.reload();
        }, 800);
      }, 2000);

    } catch (error) {
      buyBtn.disabled = false;
      buyBtn.textContent = "Confirmar compra";

      const errorEl = document.createElement('p');
      errorEl.className = 'error-msg text-xs text-red-600 bg-red-50 p-2 rounded text-center';
      errorEl.textContent = error?.message || "Error al realizar la compra.";

      buyBtn.parentElement.insertBefore(errorEl, buyBtn);

      setTimeout(() => {
        errorEl.remove();
      }, 3000);
    }
  });

  event.sectors.forEach((sector, idx) => {
    const colors = SECTOR_COLORS[idx % SECTOR_COLORS.length];

    const sectorBlock = document.createElement('div');
    sectorBlock.className = 'w-full flex flex-col gap-2';

    const sectorHeader = document.createElement('div');
    sectorHeader.className = 'flex items-center justify-between px-1';
    sectorHeader.innerHTML = `
      <div class="flex items-center gap-2">
        <div style="width:10px;height:10px;border-radius:2px;background:${colors.selected};flex-shrink:0;"></div>
        <span class="text-[11px] font-medium text-gray-600">${sector.name}</span>
      </div>
      <span class="text-[11px] text-gray-400">$${sector.price.toLocaleString('es-AR')}</span>
    `;

    const rowsContainer = document.createElement('div');
    rowsContainer.className = 'flex flex-col gap-1 items-center';

    const rows = chunkSeats(sector.seats, SEATS_PER_ROW);

    rows.forEach(rowSeats => {
      const rowEl = document.createElement('div');
      rowEl.className = 'flex gap-1';

      rowSeats.forEach(seat => {
        const el = document.createElement('div');
        el.style.cssText = 'width:20px;height:16px;border-radius:3px;transition:transform 0.1s,opacity 0.1s;';

        if (seat.status?.toLowerCase() === 'reserved') {
          el.style.background = '#e5e7eb';
          el.style.border = '1px solid #d1d5db';
        } else {
          resetSeatStyle(el, colors);
          el.style.cursor = 'pointer';

          el.addEventListener('mouseenter', () => {
            if (selected?.seatId !== seat.seatId) {
              el.style.transform = 'scale(1.15)';
              el.style.opacity = '0.85';
            }
          });
          el.addEventListener('mouseleave', () => {
            el.style.transform = 'scale(1)';
            el.style.opacity = '1';
          });

          el.addEventListener('click', () => selectSeat(seat, sector, el, colors));
        }

        rowEl.appendChild(el);
      });

      rowsContainer.appendChild(rowEl);
    });

    sectorBlock.appendChild(sectorHeader);
    sectorBlock.appendChild(rowsContainer);
    gridEl.appendChild(sectorBlock);

    if (idx < event.sectors.length - 1) {
      const divider = document.createElement('div');
      divider.style.cssText = 'width:100%;border-top:1px dashed #e5e7eb;';
      gridEl.appendChild(divider);
    }
  });

  mapEl.appendChild(gridEl);
  mapCard.appendChild(mapEl);
  wrapper.appendChild(mapCard);
  wrapper.appendChild(sidebar);

  return wrapper;
}

function chunkSeats(seats = [], size) {
  const rows = [];
  for (let i = 0; i < seats.length; i += size) {
    rows.push(seats.slice(i, i + size));
  }
  return rows;
}
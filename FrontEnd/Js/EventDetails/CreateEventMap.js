import { makeReservation } from "../Reservation/MakeReservation.js";

const SEATS_PER_ROW = 10;

export function CreateEventMap(event,userId) {
  const wrapper = document.createElement('div');
  wrapper.className = 'flex gap-6 items-start';

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
        <svg xmlns="http://www.w3.org/2000/svg" width="28" height="28" viewBox="0 0 24 24"
             fill="none" stroke="#d1d5db" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round">
          <path d="M3 7v2a3 3 0 0 0 0 6v2c0 1.1.9 2 2 2h14a2 2 0 0 0 2-2v-2a3 3 0 0 0 0-6V7a2 2 0 0 0-2-2H5a2 2 0 0 0-2 2z"/>
        </svg>
        <p class="text-xs text-gray-400 text-center leading-relaxed">
          Seleccioná un asiento en el mapa para continuar
        </p>
      </div>

      <div class="sidebar-detail hidden flex-col gap-4">
        <div class="bg-gray-50 rounded-lg p-4 flex flex-col gap-3">
          <div class="flex justify-between items-start">
            <span class="text-[10px] uppercase tracking-widest text-gray-400">Sector</span>
            <span class="sidebar-sector text-xs font-medium text-gray-900 text-right">—</span>
          </div>
          <div class="flex justify-between items-start">
            <span class="text-[10px] uppercase tracking-widest text-gray-400">Asiento</span>
            <span class="sidebar-seat text-xs font-medium text-gray-900">—</span>
          </div>
          <div class="h-px bg-gray-200"></div>
          <div class="flex justify-between items-center">
            <span class="text-[10px] uppercase tracking-widest text-gray-400">Total</span>
            <span class="sidebar-price text-sm font-semibold text-gray-900">—</span>
          </div>
        </div>

        <button class="buy-btn w-full flex items-center justify-center gap-2 px-4 py-3
                       bg-black text-white text-xs font-medium rounded-xl
                       hover:bg-gray-800 active:scale-95 transition-all duration-150">
          <svg xmlns="http://www.w3.org/2000/svg" width="13" height="13" viewBox="0 0 24 24"
               fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
            <path d="M6 2 3 6v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2V6l-3-4z"/>
            <line x1="3" y1="6" x2="21" y2="6"/>
            <path d="M16 10a4 4 0 0 1-8 0"/>
          </svg>
          Confirmar compra
        </button>

        <button class="clear-btn w-full py-2 text-[11px] text-gray-400 hover:text-gray-700
                       transition-colors tracking-wide">
          Cancelar selección
        </button>
      </div>

    </div>
  `;

  let selected = null;

  const emptyEl = sidebar.querySelector('.sidebar-empty');
  const detailEl= sidebar.querySelector('.sidebar-detail');
  const sectorEl= sidebar.querySelector('.sidebar-sector');
  const seatEl = sidebar.querySelector('.sidebar-seat');
  const priceEl= sidebar.querySelector('.sidebar-price');
  const buyBtn = sidebar.querySelector('.buy-btn');
  const clearBtn  = sidebar.querySelector('.clear-btn');

  function selectSeat(seat, sector, el) {
    if (selected) {
      selected.el.style.background = '#dbeafe';
      selected.el.style.border = '1px solid #bfdbfe';
    }

    if (selected?.seatId === seat.seatId) {
      selected = null;
      showEmpty();
    } else {
      el.style.background = '#3b82f6';
      el.style.border = '1px solid #2563eb';
      selected = { seatId: seat.seatId, seat, sector, el };
      showDetail(selected);
    }
  }

  function showEmpty() {
    emptyEl.classList.remove('hidden');
    emptyEl.classList.add('flex');
    detailEl.classList.add('hidden');
    detailEl.classList.remove('flex');
  }

  function showDetail({ seat, sector }) {
    emptyEl.classList.add('hidden');
    emptyEl.classList.remove('flex');
    detailEl.classList.remove('hidden');
    detailEl.classList.add('flex');
    sectorEl.textContent = sector.name;
    seatEl.textContent   = `N° ${seat.seatNumber}`;
    priceEl.textContent  = `$${sector.price.toLocaleString('es-AR')}`;
  }

  clearBtn.addEventListener('click', () => {
    if (selected) {
      selected.el.style.background = '#dbeafe';
      selected.el.style.border = '1px solid #bfdbfe';
      selected = null;
      showEmpty();
    }
  });

buyBtn.addEventListener('click', async () => {
    if (!selected) return;

    buyBtn.disabled = true;
    buyBtn.textContent = "Procesando...";

    try {
        await makeReservation(userId, selected.seatId);

        buyBtn.textContent = "¡Compra realizada!";
        
        buyBtn.classList.remove("bg-black", "hover:bg-gray-800");
        buyBtn.classList.add("bg-green-600", "cursor-default");
      setTimeout(() => {
        selected.el.style.background = '#e5e7eb';
        selected.el.style.border = 'none';
        selected.el.style.cursor = 'default';
        selected = null;
        showEmpty();
       
        buyBtn.textContent = "Confirmar compra";
        buyBtn.disabled = false;
        buyBtn.classList.add("bg-black", "hover:bg-gray-800");
        buyBtn.classList.remove("bg-green-600", "cursor-default");
    }, 2000)
       

    } catch (error) {
        buyBtn.disabled = false;
        buyBtn.textContent = "No se pudo realizar la compra..";

        const errorEl = document.createElement('p');
        errorEl.className = 'text-xs text-red-600 bg-red-50 px-3 py-2 rounded text-center';
        errorEl.textContent = error.message || "Error al realizar la compra.";
        buyBtn.parentElement.insertBefore(errorEl, buyBtn);

        setTimeout(() => errorEl.remove(), 3000);
    }
});

  event.sectors.forEach(sector => {
    const sectorLabel = document.createElement('div');
    sectorLabel.className = 'w-full flex items-center gap-3';
    sectorLabel.innerHTML = `
      <span class="text-[10px] font-medium uppercase tracking-widest text-gray-400 whitespace-nowrap">${sector.name}</span>
      <div class="flex-1 h-px bg-gray-100"></div>
      <span class="text-[10px] text-gray-400 whitespace-nowrap">$${sector.price.toLocaleString('es-AR')}</span>
    `;
    gridEl.appendChild(sectorLabel);

    const rows = chunkSeats(sector.seats, SEATS_PER_ROW);
    rows.forEach((rowSeats, rowIndex) => {
      const rowEl = document.createElement('div');
      rowEl.className = 'flex items-center gap-1';

      const label = document.createElement('span');
      label.className = 'text-[10px] text-gray-400 w-5 text-right mr-1 select-none';
      label.textContent = String.fromCharCode(65 + rowIndex);
      rowEl.appendChild(label);

      rowSeats.forEach(seat => {
        const el = document.createElement('div');
        el.style.cssText = 'width:20px;height:16px;border-radius:3px;flex-shrink:0;transition:opacity 0.1s;';

        if (seat.status === 'Reserved') {
          el.style.background = '#e5e7eb';
          el.style.cursor = 'default';
          el.title = `${sector.name} · Asiento ${seat.seatNumber} — Reservado`;
        } else {
          el.style.background = '#dbeafe';
          el.style.border = '1px solid #bfdbfe';
          el.style.cursor = 'pointer';
          el.title = `${sector.name} · Asiento ${seat.seatNumber} · $${sector.price.toLocaleString('es-AR')}`;
          el.addEventListener('click', () => selectSeat(seat, sector, el));
          el.addEventListener('mouseenter', () => {
            if (selected?.seatId !== seat.seatId) el.style.opacity = '0.7';
          });
          el.addEventListener('mouseleave', () => el.style.opacity = '1');
        }

        rowEl.appendChild(el);
      });

      const label2 = label.cloneNode(true);
      label2.className = 'text-[10px] text-gray-400 w-5 ml-1 select-none';
      rowEl.appendChild(label2);
      gridEl.appendChild(rowEl);
    });
  });

  mapEl.appendChild(gridEl);
  mapCard.appendChild(mapEl);
  wrapper.appendChild(mapCard);
  wrapper.appendChild(sidebar);
  return wrapper;
}

function chunkSeats(seats, size) {
  const rows = [];
  for (let i = 0; i < seats.length; i += size) {
    rows.push(seats.slice(i, i + size));
  }
  return rows;
}
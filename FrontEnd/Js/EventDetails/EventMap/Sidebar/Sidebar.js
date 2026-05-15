import { PaymentModal } from '../Utils/PaymentModal.js';

const SIDEBAR_TEMPLATE = `
  <div class="px-5 py-4 border-b border-gray-100">
    <p class="text-xs font-medium uppercase tracking-widest text-gray-400">
      Tu selección
    </p>
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
          <span class="text-[10px] text-gray-400">
            Sector
          </span>

          <div class="flex items-center gap-1.5">
            <div class="sidebar-sector-pill w-2 h-2 rounded-sm"></div>

            <span class="sidebar-sector text-xs font-medium text-gray-900">
              —
            </span>
          </div>
        </div>

        <div class="flex justify-between">
          <span class="text-[10px] text-gray-400">
            Asiento
          </span>

          <span class="sidebar-seat text-xs font-medium text-gray-900">
            —
          </span>
        </div>

        <div class="flex justify-between">
          <span class="text-[10px] text-gray-400">
            Total
          </span>

          <span class="sidebar-price text-sm font-semibold text-gray-900">
            —
          </span>
        </div>

      </div>

      <button
        class="buy-btn w-full py-3 bg-black text-white text-xs rounded-xl"
      >
        Pasar a finalizar compra
      </button>

      <button
        class="clear-btn w-full py-2 text-[11px] text-gray-400"
      >
        Cancelar selección
      </button>

    </div>

  </div>
`;

function showTemporaryMessage(
  container,
  text,
  className,
  duration = 4000
) {
  const existing = container.querySelector('.temp-msg');

  existing?.remove();

  const msg = document.createElement('p');

  msg.className = `temp-msg ${className}`;
  msg.textContent = text;

  container.appendChild(msg);

  setTimeout(() => {
    msg.remove();
  }, duration);
}

export function createSidebar({ onClear, onBuy }) {

  const sidebar = document.createElement('div');

  sidebar.className =
    'w-64 bg-white border border-gray-200 rounded-xl overflow-hidden sticky top-24';

  sidebar.innerHTML = SIDEBAR_TEMPLATE;

  const emptyEl  = sidebar.querySelector('.sidebar-empty');
  const detailEl = sidebar.querySelector('.sidebar-detail');

  const sectorEl = sidebar.querySelector('.sidebar-sector');
  const pillEl   = sidebar.querySelector('.sidebar-sector-pill');

  const seatEl   = sidebar.querySelector('.sidebar-seat');
  const priceEl  = sidebar.querySelector('.sidebar-price');

  const buyBtn   = sidebar.querySelector('.buy-btn');
  const clearBtn = sidebar.querySelector('.clear-btn');

  const bodyEl   = sidebar.querySelector('.p-5');

  function showEmpty() {

    emptyEl.classList.remove('hidden');

    detailEl.classList.add('hidden');
    detailEl.classList.remove('flex');
  }

  function showDetail({ seat, sector, colors }) {

    emptyEl.classList.add('hidden');

    detailEl.classList.remove('hidden');
    detailEl.classList.add('flex');

    sectorEl.textContent = sector.name;

    pillEl.style.background = colors.selected;

    seatEl.textContent = `N° ${seat.seatNumber}`;

    priceEl.textContent =
      `$${sector.price.toLocaleString('es-AR')}`;
  }

  clearBtn.addEventListener('click', onClear);

  buyBtn.addEventListener('click', async () => {

    if (buyBtn.disabled) return;

    buyBtn.disabled = true;
    buyBtn.textContent = 'Procesando...';

    sidebar.querySelector('.error-msg')?.remove();

    try {

      const result = await onBuy();
      await PaymentModal(
        result.reservation.id,
        result.eventName,
        result.selected
      );
      buyBtn.disabled = false;
      buyBtn.textContent = 'Pasar a finalizar compra';
    } catch (error) {

      buyBtn.disabled = false;

      buyBtn.textContent =
        'Pasar a finalizar compra';

      
    Swal.fire({
        toast: true,
        position: "bottom-end",
        icon: "error",
        title: error?.Message ?? 'Error al realizar la compra.',
        showConfirmButton: false,
        timer: 5000,
        timerProgressBar: true
    });
    setTimeout(() => {
          location.reload();
      }, 5000);
    }
  });

  return {
    element: sidebar,
    showEmpty,
    showDetail,
    showExpiredMessage: () => showTemporaryMessage(
        bodyEl, 'El tiempo expiró. Por favor, elegí otro asiento.',
        'text-xs text-amber-600 bg-amber-50 p-2 rounded text-center'),
  };
}
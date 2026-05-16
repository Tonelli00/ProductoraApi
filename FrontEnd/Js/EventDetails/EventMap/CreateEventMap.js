import { SECTOR_COLORS } from './Utils/Constants.js';
import { createTimer } from './Timer/Timer.js';
import { createSidebar } from './Sidebar/Sidebar.js';
import { createMapCard, resetSeatStyle } from './Map/mapCard.js';
import { makeReservation } from '../../Reservation/MakeReservation.js';

export function syncSeats(updatedEvent) {
    updatedEvent.sectors.forEach(sector => {

        sector.seats.forEach(seat => {
            const seatElement =
                document.querySelector(
                    `[data-seat-id="${seat.seatId}"]`
                );

            if (!seatElement) return;

            if (seat.status === 'Reserved' || seat.status === 'Sold') {

                seatElement.style.background = '#e5e7eb';
                seatElement.style.border = '1px solid #d1d5db';

                seatElement.classList.add(
                    'pointer-events-none'
                );
            }
        });
    });
}

export function CreateEventMap(event, userId) {
  const Container = document.createElement('div');
  Container.className = 'flex gap-6 items-start';

  if (!event?.sectors?.length) {
    const empty = document.createElement('p');
    empty.className = 'text-sm text-gray-500';
    empty.textContent = 'No hay sectores disponibles';
    Container.appendChild(empty);
    return Container;
  }

  let selected = null;

  const timer = createTimer(() => {
    if (selected) {
      resetSeatStyle(selected.el, selected.colors);
      selected = null;
    }
    sidebar.showEmpty();
    sidebar.showExpiredMessage();
  });

  const sidebar = createSidebar({
    onClear: () => {
      if (!selected) return;
      resetSeatStyle(selected.el, selected.colors);
      selected = null;
      sidebar.showEmpty();
      timer.stop();
    },
    onBuy: async () => {
      const reservation=await makeReservation(userId, selected.seatId)
      return {reservation,eventName:event.name,selected}
    },
  });

  function handleSeatSelect(seat, sector, seatElement, colors) {
    const isSameSeat = selected?.seatId === seat.seatId;

    if (selected) resetSeatStyle(selected.el, selected.colors);

    if (isSameSeat) {
      selected = null;
      sidebar.showEmpty();
      timer.stop();
      return;
    }

    seatElement.style.background = colors.selected;
    seatElement.style.border = `1px solid ${colors.selected}`;

    selected = { seatId: seat.seatId, seat, sector, el: seatElement, colors };
    sidebar.showDetail(selected);
    timer.show();
  }

  const mapCard = createMapCard(event.sectors, SECTOR_COLORS, handleSeatSelect);

  Container.appendChild(mapCard);
  Container.appendChild(sidebar.element);

  return Container;
}




import { ConfirmReservation } from "../../../Reservation/ConfirmReservation.js";

export async function PaymentModal(reservationId,eventName,selected)
{
    const existingModal = document.getElementById("payment-modal");
    if (existingModal)
    {
        existingModal.remove();
    }
    const modal = document.createElement("div");
    modal.id = "payment-modal";

    modal.className ="fixed inset-0  flex items-center justify-center bg-black/50 backdrop-blur-sm";
    modal.innerHTML = `
        <div class="w-full max-w-md rounded-2xl bg-white p-6 shadow-2xl animate-fadeIn">
            
            <div class="mb-6 flex items-center justify-between">
                <div>
                    <h2 class="text-2xl font-bold text-gray-800">
                        Confirmar pago
                    </h2>

                    <p class="text-sm text-gray-500">
                        Completa los datos para finalizar la compra
                    </p>
                </div>

                <button 
                    id="close-payment-modal"
                    class="rounded-full p-2 transition hover:bg-gray-100"
                >
                    ✕
                </button>
            </div>

            <div class="mb-5 rounded-xl bg-gray-100 p-4">
                
                <div class="flex justify-between text-sm">
                    <span class="text-gray-500">Evento</span>
                    <span class="font-semibold text-gray-800">
                        ${eventName}
                    </span>
                </div>
                <div class="mt-2 flex justify-between text-sm">
                    <span class="text-gray-500">Asiento</span>

                    <span class="font-semibold text-gray-800">
                        ${selected.seat.seatNumber} - ${selected.sector.name}
                    </span>
                </div>
                <div class="mt-2 flex justify-between text-sm">
                    <span class="text-gray-500">Total</span>

                    <span class="font-bold text-emerald-600">
                        $${selected.sector.price}
                    </span>
                </div>
            </div>
            <form id="payment-form" class="space-y-4">

                <div>
                    <label class="mb-1 block text-sm font-medium text-gray-700">
                        Titular de la tarjeta
                    </label>
                    <input
                        type="text"
                        placeholder="Juan Pérez"
                        class="w-full rounded-xl border border-gray-300 px-4 py-3 outline-none transition focus:border-emerald-500"
                        required
                    />
                </div>
                <div>
                    <label class="mb-1 block text-sm font-medium text-gray-700">
                        Número de tarjeta
                    </label>
                    <input
                        type="text"
                        maxlength="16"
                        placeholder="1234123412341234"
                        class="w-full rounded-xl border border-gray-300 px-4 py-3 outline-none transition focus:border-emerald-500"
                        required
                    />
                </div>
                <div class="grid grid-cols-2 gap-4">

                    <div>
                        <label class="mb-1 block text-sm font-medium text-gray-700">
                            Expiración
                        </label>
                        <input
                            type="text"
                            placeholder="MM/YY"
                            class="w-full rounded-xl border border-gray-300 px-4 py-3 outline-none transition focus:border-emerald-500"
                            required
                        />
                    </div>

                    <div>
                        <label class="mb-1 block text-sm font-medium text-gray-700">
                            CVV
                        </label>
                        <input
                            type="password"
                            maxlength="3"
                            placeholder="123"
                            class="w-full rounded-xl border border-gray-300 px-4 py-3 outline-none transition focus:border-emerald-500"
                            required
                        />
                    </div>

                </div>

                <button
                    type="submit"
                    class="mt-2 w-full rounded-xl bg-emerald-500 px-4 py-3 font-semibold text-white transition hover:bg-emerald-600"
                >
                    Pagar ahora
                </button>

            </form>
        </div>
    `;

    document.body.appendChild(modal);
    document.body.classList.add('overflow-hidden');

    const closeButton = modal.querySelector("#close-payment-modal");
    
    closeButton.addEventListener("click", () =>
    {
        modal.remove();
        document.body.classList.remove('overflow-hidden');
    });

    modal.addEventListener("click", (e) =>
    {
        if (e.target === modal)
        {
            modal.remove();
            document.body.classList.remove('overflow-hidden');
        }
    });

    const form = modal.querySelector("#payment-form");
    
    form.addEventListener("submit", async (e) =>
    {   
        e.preventDefault();

        try {
        await ConfirmReservation(reservationId);

        modal.remove();

        Swal.fire({
            toast: true,
            position: "bottom-end",
            icon: "success",
            title: "Reserva pagada con éxito",
            showConfirmButton: false,
            timer: 5000,
            timerProgressBar: true
        });
       
        
    }
     catch (error) {
        Swal.fire({
            toast: true,
            position: "bottom-end",
            icon: "error",
            title: "No se pudo realizar la reserva, intente nuevamente.",
            showConfirmButton: false,
            timer: 5000
        });
        setTimeout(() => {
            location.reload();
        }, 5000);
    }
    });
}

import { createUser } from "../User/CreateUser.js";


function validateName(name) {
    if (!name) return "El nombre es requerido.";
    if (name.trim().length < 2) return "Debe tener al menos 2 caracteres.";
    return null;
}

function validateEmail(email) {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!email) return "El email es requerido.";
    if (!regex.test(email)) return "Formato inválido.";
    return null;
}

function validatePassword(password) {
    if (!password) return "La contraseña es requerida.";
    if (password.length < 6) return "Mínimo 6 caracteres.";
    if (!/[A-Z]/.test(password)) return "Debe tener una mayúscula.";
    if (!/[0-9]/.test(password)) return "Debe tener un número.";
    return null;
}


function setFieldError(id, msg) {
    const input = document.getElementById(id);
    const error = document.getElementById(`${id}-error`);

    input.classList.add("border-red-400");
    input.classList.remove("border-gray-200");

    error.textContent = msg;
    error.classList.remove("hidden");
}

function clearFieldError(id) {
    const input = document.getElementById(id);
    const error = document.getElementById(`${id}-error`);

    input.classList.remove("border-red-400");
    input.classList.add("border-gray-200");

    error.classList.add("hidden");
}

function setGlobalError(msg) {
    const el = document.getElementById("register-error");
    el.textContent = msg;
    el.classList.remove("hidden");
}

function setGlobalSuccess(msg) {
    const el = document.getElementById("register-success");
    el.textContent = msg;
    el.classList.remove("hidden");
}

function clearGlobalMessages() {
    document.getElementById("register-error").classList.add("hidden");
    document.getElementById("register-success").classList.add("hidden");
}


function createModalHTML() {
    if (document.getElementById("modal-register")) return;

    document.body.insertAdjacentHTML("beforeend", `
        <div id="modal-register" class="hidden fixed inset-0 bg-black/50 z-50 flex items-center justify-center">
            <div class="bg-white rounded-lg border p-8 w-full max-w-md mx-4">

                <div class="flex justify-between mb-6">
                    <h2 class="font-semibold">Crear cuenta</h2>
                    <button id="modal-close">✕</button>
                </div>

                <form id="register-form" class="flex flex-col gap-4" novalidate>

                    <div>
                        <input id="register-name" placeholder="Nombre" class="border px-3 py-2 w-full"/>
                        <span id="register-name-error" class="hidden text-red-500 text-xs"></span>
                    </div>

                    <div>
                        <input id="register-email" placeholder="Email" class="border px-3 py-2 w-full"/>
                        <span id="register-email-error" class="hidden text-red-500 text-xs"></span>
                    </div>

                    <div>
                        <input id="register-password" type="password" placeholder="Contraseña" class="border px-3 py-2 w-full"/>
                        <span id="register-password-error" class="hidden text-red-500 text-xs"></span>
                    </div>

                    <p id="register-success" class="hidden text-green-600 text-xs"></p>
                    <p id="register-error" class="hidden text-red-600 text-xs"></p>

                    <button type="submit" class="bg-black text-white py-2 rounded">
                        Registrarse
                    </button>
                </form>
            </div>
        </div>
    `);
}


function bindValidationEvents() {
    const fields = [
        { id: "register-name", fn: validateName },
        { id: "register-email", fn: validateEmail },
        { id: "register-password", fn: validatePassword },
    ];

    fields.forEach(({ id, fn }) => {
        const input = document.getElementById(id);

        input.addEventListener("blur", () => {
            const err = fn(input.value);
            err ? setFieldError(id, err) : clearFieldError(id);
        });

        input.addEventListener("input", () => clearFieldError(id));
    });
}


function openModal() {
    document.getElementById("modal-register").classList.remove("hidden");
}

function closeModal() {
    document.getElementById("modal-register").classList.add("hidden");
    document.getElementById("register-form").reset();

    ["register-name", "register-email", "register-password"].forEach(clearFieldError);
    clearGlobalMessages();
}


async function handleSubmit(e) {
    e.preventDefault();
    clearGlobalMessages();

    const name = document.getElementById("register-name").value;
    const email = document.getElementById("register-email").value;
    const password = document.getElementById("register-password").value;

    const nameErr = validateName(name);
    const emailErr = validateEmail(email);
    const passErr = validatePassword(password);

    if (nameErr) setFieldError("register-name", nameErr);
    if (emailErr) setFieldError("register-email", emailErr);
    if (passErr) setFieldError("register-password", passErr);

    if (nameErr || emailErr || passErr) return;

    try {
        const result = await createUser(name, email, password);

        if (!result || result.error) {
            throw new Error(result?.message || "No se pudo registrar");
        }

        setGlobalSuccess("Cuenta creada correctamente");

        setTimeout(() => {
            closeModal();
        }, 1200);

    } catch (err) {
        setGlobalError(err.message || "Error al registrarse");
    }
}


export function initRegisterModal() {
    createModalHTML();
    bindValidationEvents();

    document.getElementById("btn-register").addEventListener("click", openModal);
    document.getElementById("modal-close").addEventListener("click", closeModal);
    document.getElementById("register-form").addEventListener("submit", handleSubmit);

    document.getElementById("modal-register").addEventListener("click", (e) => {
        if (e.target.id === "modal-register") closeModal();
    });
}
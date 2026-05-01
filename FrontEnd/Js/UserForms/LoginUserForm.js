import { login } from "../User/LoginUser.js";


function validateEmail(email) {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!email) return "El email es requerido.";
    if (!regex.test(email)) return "El email no tiene un formato válido.";
    return null;
}

function validatePassword(password) {
    if (!password) return "La contraseña es requerida.";
    return null;
}


function setFieldError(inputId, message) {
    const input = document.getElementById(inputId);
    const errorEl = document.getElementById(`${inputId}-error`);

    input.classList.add("border-red-400");
    input.classList.remove("border-gray-200");

    errorEl.textContent = message;
    errorEl.classList.remove("hidden");
}

function clearFieldError(inputId) {
    const input = document.getElementById(inputId);
    const errorEl = document.getElementById(`${inputId}-error`);

    input.classList.remove("border-red-400");
    input.classList.add("border-gray-200");

    errorEl.classList.add("hidden");
}

function setGlobalError(message) {
    const el = document.getElementById("login-error");
    el.textContent = message;
    el.classList.remove("hidden");
}

function setGlobalSuccess(message) {
    const el = document.getElementById("login-success");
    el.textContent = message;
    el.classList.remove("hidden");
}

function clearGlobalMessages() {
    document.getElementById("login-error").classList.add("hidden");
    document.getElementById("login-success").classList.add("hidden");
}


function createModalHTML() {
    if (document.getElementById("modal-login")) return;
    document.body.insertAdjacentHTML("beforeend", `
        <div id="modal-login" class="hidden fixed inset-0 bg-black/50 z-50 flex items-center justify-center">
            <div class="bg-white rounded-lg border border-gray-200 p-8 w-full max-w-md mx-4">
                <div class="flex justify-between items-center mb-6">
                    <h2 class="text-lg font-semibold">Iniciar sesión</h2>
                    <button id="login-close" class="text-gray-400 hover:text-black text-xl">✕</button>
                </div>
                <form id="login-form" class="flex flex-col gap-4" novalidate>
                    <div class="flex flex-col gap-1">
                        <label class="text-xs text-gray-500">Email</label>
                        <input id="login-email" type="email"
                            class="border border-gray-200 rounded px-3 py-2 text-sm" />
                        <span id="login-email-error" class="hidden text-xs text-red-500"></span>
                        <span class="text-xs text-black-400">Email precargado: Proyecto2026@gmail.com</span>
                    </div>
                    <div class="flex flex-col gap-1">
                        <label class="text-xs text-gray-500">Contraseña</label>
                        <input id="login-password" type="password"
                            class="border border-gray-200 rounded px-3 py-2 text-sm" />
                        <span id="login-password-error" class="hidden text-xs text-red-500"></span>
                        <span class="text-xs text-black-400">Contraseña precargada: Proyecto123</span>
                    </div>
                    <p id="login-error" class="hidden text-xs text-red-600"></p>
                    <p id="login-success" class="hidden text-xs text-green-600"></p>
                    <button type="submit" class="w-full bg-black text-white py-2 rounded">
                        Iniciar sesión
                    </button>
                </form>
            </div>
        </div>
    `);
}


function bindValidationEvents() {
    const emailInput = document.getElementById("login-email");
    const passInput  = document.getElementById("login-password");

    emailInput.addEventListener("blur", () => {
        const error = validateEmail(emailInput.value);
        error ? setFieldError("login-email", error) : clearFieldError("login-email");
    });

    passInput.addEventListener("blur", () => {
        const error = validatePassword(passInput.value);
        error ? setFieldError("login-password", error) : clearFieldError("login-password");
    });

    emailInput.addEventListener("input", () => clearFieldError("login-email"));
    passInput.addEventListener("input", () => clearFieldError("login-password"));
}


function openModal() {
    const isLogged = localStorage.getItem("IsLogged") === "true";
    if (isLogged) return;

    document.getElementById("modal-login").classList.remove("hidden");
}

function closeModal() {
    document.getElementById("modal-login").classList.add("hidden");
    document.getElementById("login-form").reset();

    clearFieldError("login-email");
    clearFieldError("login-password");
    clearGlobalMessages();
}


async function handleSubmit(e) {
    e.preventDefault();
    clearGlobalMessages();

    const email    = document.getElementById("login-email").value;
    const password = document.getElementById("login-password").value;

    const emailError = validateEmail(email);
    const passError  = validatePassword(password);

    if (emailError) setFieldError("login-email", emailError);
    if (passError)  setFieldError("login-password", passError);

    if (emailError || passError) return;

    try {
        const user = await login(email, password);

        if (!user || !user.id) {
            throw new Error("Credenciales incorrectas");
        }

        localStorage.setItem("UserId", String(user.id));
        localStorage.setItem("UserName", user.name);
        localStorage.setItem("IsLogged", "true");

        setGlobalSuccess("¡Cuenta logueada con éxito!");

        setTimeout(() => {
            closeModal();
            updateNav();
            location.reload();
        }, 1000);

    } catch (error) {
        setGlobalError(error.message || "Error al iniciar sesión");
    }
}


export function initLoginModal() {
    createModalHTML();
    bindValidationEvents();

    document.getElementById("btn-login").addEventListener("click", openModal);
    document.getElementById("login-close").addEventListener("click", closeModal);
    document.getElementById("login-form").addEventListener("submit", handleSubmit);

    document.getElementById("modal-login").addEventListener("click", (e) => {
        if (e.target.id === "modal-login") closeModal();
    });
}


export function updateNav() {
    const isLogged = localStorage.getItem("IsLogged") === "true";
    const userName = localStorage.getItem("UserName");

    const btnLogin    = document.getElementById("btn-login");
    const btnRegister = document.getElementById("btn-register");

    if (isLogged && userName) {
        btnLogin.textContent = `Hola, ${userName}`;
        btnRegister.classList.add("hidden");
    }
}
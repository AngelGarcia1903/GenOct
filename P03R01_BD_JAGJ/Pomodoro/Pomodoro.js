// ==========================================
// ARRAY DE ACTIVIDADES
// ==========================================

const pomodoro = [];

// ==========================================
// ELEMENTOS DEL HTML
// ==========================================

const actividadInput = document.getElementById("actividad");

const horasInput = document.getElementById("horas");
const minutosInput = document.getElementById("minutos");
const segundosInput = document.getElementById("segundos");

const botonAgregar = document.getElementById("agregar");
const botonIniciar = document.getElementById("iniciar");
const botonDetener = document.getElementById("detener");

const listaActividades = document.getElementById("listaActividades");

const actividadActualTexto = document.getElementById("actividadActual");

const temporizador = document.getElementById("temporizador");

// ==========================================
// VARIABLES DEL TEMPORIZADOR
// ==========================================

let posicion = 0;

let tiempoRestante = 0;

let intervalo = null;

// ==========================================
// AGREGAR ACTIVIDAD
// ==========================================

botonAgregar.addEventListener("click", () => {
  const actividad = actividadInput.value;

  const horas = Number(horasInput.value);
  const minutos = Number(minutosInput.value);
  const segundos = Number(segundosInput.value);

  // Convertimos todo a segundos

  const tiempoTotal = horas * 3600 + minutos * 60 + segundos;

  // Validaciones sencillas

  if (actividad === "") {
    alert("Ingresa una actividad");
    return;
  }

  if (tiempoTotal <= 0) {
    alert("Ingresa un tiempo válido");
    return;
  }

  // Creamos y guardamos el objeto

  pomodoro.push({
    actividad: actividad,
    tiempo: tiempoTotal,
  });

  mostrarActividades();

  // Limpiar formulario

  actividadInput.value = "";

  horasInput.value = 0;
  minutosInput.value = 0;
  segundosInput.value = 0;
});

// ==========================================
// MOSTRAR ACTIVIDADES
// ==========================================

function mostrarActividades() {
  listaActividades.innerHTML = "";

  for (let i = 0; i < pomodoro.length; i++) {
    const elemento = document.createElement("li");

    elemento.textContent = `${pomodoro[i].actividad} - ${formatearTiempo(pomodoro[i].tiempo)}`;

    listaActividades.appendChild(elemento);
  }
}

// ==========================================
// FORMATEAR SEGUNDOS → HH:MM:SS
// ==========================================

function formatearTiempo(segundosTotales) {
  const horas = Math.floor(segundosTotales / 3600);

  const minutos = Math.floor((segundosTotales % 3600) / 60);

  const segundos = segundosTotales % 60;

  const horasTexto = horas.toString().padStart(2, "0");

  const minutosTexto = minutos.toString().padStart(2, "0");

  const segundosTexto = segundos.toString().padStart(2, "0");

  return `${horasTexto}:${minutosTexto}:${segundosTexto}`;
}

// ==========================================
// INICIAR
// ==========================================

botonIniciar.addEventListener("click", () => {
  if (pomodoro.length === 0) {
    alert("Primero agrega una actividad");
    return;
  }

  // Evitamos crear varios intervalos
  // si presionamos iniciar varias veces.

  if (intervalo !== null) {
    return;
  }

  iniciarActividad();
});

// ==========================================
// INICIAR ACTIVIDAD ACTUAL
// ==========================================

function iniciarActividad() {
  const actividad = pomodoro[posicion];

  tiempoRestante = actividad.tiempo;

  actividadActualTexto.textContent = actividad.actividad;

  temporizador.textContent = formatearTiempo(tiempoRestante);

  intervalo = setInterval(() => {
    tiempoRestante--;

    temporizador.textContent = formatearTiempo(tiempoRestante);

    if (tiempoRestante <= 0) {
      clearInterval(intervalo);

      intervalo = null;

      siguienteActividad();
    }
  }, 1000);
}

// ==========================================
// SIGUIENTE ACTIVIDAD
// ==========================================

function siguienteActividad() {
  posicion++;

  // Si llegamos al final
  // volvemos al principio.

  if (posicion >= pomodoro.length) {
    posicion = 0;
  }

  iniciarActividad();
}

// ==========================================
// DETENER
// ==========================================

botonDetener.addEventListener("click", () => {
  clearInterval(intervalo);

  intervalo = null;
});

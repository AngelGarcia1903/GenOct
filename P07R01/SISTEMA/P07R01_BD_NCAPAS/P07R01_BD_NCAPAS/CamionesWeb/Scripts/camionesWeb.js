document.addEventListener("DOMContentLoaded", function () {
    // Obtenemos la ruta actual
    var currentPath = window.location.pathname;

    // Seleccionamos todos los enlaces dentro de .sidebar-nav
    var navLinks = document.querySelectorAll('.sidebar-nav .nav-link');

    navLinks.forEach(function (link) {
        // ASP.NET convierte "~/Rutas" en un href relativo ("Rutas" o "./"),
        // link.pathname siempre devuelve la ruta completa ("/Rutas" o "/")
        var cleanHref = link.pathname;

        // Si la ruta actual coincide con el href, agregamos la clase
        if (currentPath === cleanHref || (currentPath === '/' && cleanHref === '/')) {
            link.classList.add('nav-link-active');
        }
    });

    // Indicador de carga: al hacer clic en un boton .js-cargando (Filtrar, Actualizar, Refrescar)
    // se le agrega la clase is-loading (spinner en CSS) mientras la pagina se recarga.
    // OJO: no se usa "disabled", porque un boton deshabilitado no se envia y su evento Click no se ejecutaria.
    document.querySelectorAll('.js-cargando').forEach(function (btn) {
        btn.addEventListener('click', function () {
            btn.classList.add('is-loading');
        });
    });

    // El DropDownList de filtro hace AutoPostBack: se marca el boton Filtrar como cargando
    var filtro = document.querySelector('.filtros select');
    if (filtro) {
        filtro.addEventListener('change', function () {
            var btnFiltrar = document.querySelector('.filtros .js-cargando');
            if (btnFiltrar) btnFiltrar.classList.add('is-loading');
        });
    }
});


/**
 * * @param {boolean} visible Controla la visibilidad del modal
 */
function mostrarModalFormulario(visible) {
    const element = document.getElementById('modalFormulario');
    let instance = bootstrap.Modal.getInstance(element);
    if (!instance) {
        instance = new bootstrap.Modal(element);
    }
    if (visible) {
        instance.show();
    }
    else {
        instance.hide();
        // Limpieza forzada para eliminar rastros que el UpdatePanel deja huérfanos
        document.body.classList.remove('modal-open');
        document.body.style.overflow = '';
        document.body.style.paddingRight = '';

        // Buscar y destruir todos los fondos oscuros sobrantes en el body
        var backdrops = document.querySelectorAll('.modal-backdrop');
        backdrops.forEach(function (backdrop) {
            backdrop.remove();
        });
    }
}


/**
 * * @param {boolean} visible Controla la visibilidad del modal
 */
function mostrarModalEliminar(visible) {
    const element = document.getElementById('modalEliminar');
    let instance = bootstrap.Modal.getInstance(element);
    if (!instance) {
        instance = new bootstrap.Modal(element);
    }
    if (visible) {
        instance.show();
    }
    else {
        instance.hide();
        // Limpieza forzada para eliminar rastros que el UpdatePanel deja huérfanos
        document.body.classList.remove('modal-open');
        document.body.style.overflow = '';
        document.body.style.paddingRight = '';

        // Buscar y destruir todos los fondos oscuros sobrantes en el body
        var backdrops = document.querySelectorAll('.modal-backdrop');
        backdrops.forEach(function (backdrop) {
            backdrop.remove();
        });
    }
}
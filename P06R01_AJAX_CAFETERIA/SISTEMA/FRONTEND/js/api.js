/*
 * api.js — Todas las llamadas AJAX a la API de la cafetería (jQuery $.ajax).
 *
 * AJAX = el navegador pide o envía datos al servidor en segundo plano y actualiza solo
 * una parte de la página, sin recargarla completa.
 *
 * Cada función regresa el objeto jqXHR de jQuery: se usa con .done() (éxito),
 * .fail() (error) y .always() (siempre, al terminar).
 */
var CafeApi = (function ($) {
  'use strict';

  // Dirección de la API (perfil "http" de Visual Studio). Cámbiala aquí si usas otro puerto.
  var URL_API = 'http://localhost:5220/api';

  function peticion(metodo, ruta, datos) {
    var opciones = {
      url: URL_API + ruta,
      type: metodo,          // GET, POST...
      dataType: 'json',      // jQuery convierte la respuesta JSON en objetos de JavaScript
      timeout: 10000         // 10 s máximo de espera
    };
    if (datos !== undefined) {
      opciones.contentType = 'application/json; charset=utf-8';
      opciones.data = JSON.stringify(datos);
    }
    return $.ajax(opciones);
  }

  // Convierte cualquier error de $.ajax en un mensaje legible para el usuario
  function mensajeError(xhr, textStatus) {
    if (textStatus === 'timeout') {
      return 'La API tardó demasiado en responder. Intenta de nuevo.';
    }
    if (!xhr || xhr.status === 0) {
      return 'No hay conexión con la API. Verifica que esté corriendo en ' + URL_API.replace('/api', '') + '.';
    }
    var cuerpo = null;
    try { cuerpo = JSON.parse(xhr.responseText); } catch (e) { /* respuesta sin JSON */ }

    if (cuerpo && cuerpo.errors) {                 // errores de validación (400)
      var mensajes = [];
      $.each(cuerpo.errors, function (campo, lista) {
        mensajes = mensajes.concat(lista);
      });
      return mensajes.join(' ');
    }
    if (cuerpo && cuerpo.detail) return cuerpo.detail; // reglas de negocio (404 / 409)
    if (xhr.status === 404) return 'No se encontró lo que buscas.';
    return 'Ocurrió un error inesperado (' + xhr.status + ').';
  }

  return {
    // GET /api/productos  o  /api/productos?categoria=Bebidas
    productos: function (categoria) {
      return peticion('GET', '/productos' + (categoria ? '?categoria=' + encodeURIComponent(categoria) : ''));
    },
    // GET /api/productos/categorias
    categorias: function () {
      return peticion('GET', '/productos/categorias');
    },
    // GET /api/productos/populares?top=3
    populares: function (top) {
      return peticion('GET', '/productos/populares?top=' + (top || 3));
    },
    // POST /api/pedidos   { cliente: {...}, productos: [{ productoID, cantidad }] }
    crearPedido: function (pedido) {
      return peticion('POST', '/pedidos', pedido);
    },
    // GET /api/pedidos/5
    pedido: function (id) {
      return peticion('GET', '/pedidos/' + encodeURIComponent(id));
    },
    mensajeError: mensajeError
  };
})(jQuery);

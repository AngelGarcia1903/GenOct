/*
 * cafeteria.js — Pinta en las páginas los datos que llegan por AJAX (ver js/api.js).
 *   index.html          -> favoritos (productos más pedidos)
 *   menu.html           -> carta completa con filtros por categoría
 *   today-special.html  -> productos de Panaderia
 *   pedido.html         -> carrito, envío del pedido y consulta de un pedido
 */
(function ($) {
  'use strict';

  // La BD no guarda imágenes: se usa una foto por categoría
  var IMAGENES = { Bebidas: 'img/special-1.jpg', Panaderia: 'img/special-2.jpg' };
  var IMAGEN_GENERICA = 'img/special-1.jpg';
  var dinero = new Intl.NumberFormat('es-MX', { style: 'currency', currency: 'MXN' });

  /* ------------------------------------------------------------------
   * Carrito: en el navegador solo se guardan IDs y cantidades.
   * Nombres, precios y stock siempre se piden a la API.
   * ------------------------------------------------------------------ */
  var Carrito = {
    clave: 'cafehouse.carrito',
    memoria: [],

    leer: function () {
      try {
        var datos = JSON.parse(window.localStorage.getItem(this.clave));
        return $.isArray(datos) ? datos : [];
      } catch (e) {
        return this.memoria.slice();
      }
    },
    guardar: function (items) {
      this.memoria = items;
      try { window.localStorage.setItem(this.clave, JSON.stringify(items)); } catch (e) { /* sin almacenamiento */ }
      actualizarContador();
    },
    cantidadDe: function (id) {
      var item = $.grep(this.leer(), function (x) { return x.productoID === id; })[0];
      return item ? item.cantidad : 0;
    },
    poner: function (id, cantidad) {
      var items = $.grep(this.leer(), function (x) { return x.productoID !== id; });
      if (cantidad > 0) items.push({ productoID: id, cantidad: cantidad });
      this.guardar(items);
    },
    // Suma sin pasar del stock; regresa cuántas unidades se agregaron realmente
    agregar: function (id, cantidad, maximo) {
      var actual = this.cantidadDe(id);
      var nueva = Math.min(actual + cantidad, maximo);
      this.poner(id, nueva);
      return nueva - actual;
    },
    vaciar: function () { this.guardar([]); },
    unidades: function () {
      var total = 0;
      $.each(this.leer(), function (i, x) { total += x.cantidad; });
      return total;
    }
  };

  function actualizarContador() {
    var n = Carrito.unidades();
    $('[data-carrito-total]').text(n).toggleClass('is-empty', n === 0);
  }

  /* ---------------------------- Utilidades de pintado ---------------------------- */
  function mostrarCargando($contenedor, mensaje) {
    $contenedor.empty().append($('<p class="estado-carga" role="status">').text(mensaje || 'Cargando…'));
  }

  function mostrarAlerta($contenedor, tipo, mensaje, reintentar) {
    var $alerta = $('<div>', { 'class': 'alerta alerta-' + tipo, role: tipo === 'error' ? 'alert' : 'status' })
      .append($('<p>').text(mensaje));
    if (reintentar) {
      $alerta.append($('<button type="button" class="button button-small">Reintentar</button>').on('click', reintentar));
    }
    $contenedor.empty().append($alerta);
  }

  function tarjetaProducto(p) {
    var $card = $('<article class="coffee-card product-card">');
    $card.append($('<div class="card-photo">').append(
      // .attr() y no $('<img>', {width}) : jQuery convertiría width/height en estilo en línea
      $('<img>').attr({ src: IMAGENES[p.categoria] || IMAGEN_GENERICA, alt: '', width: 286, height: 166, loading: 'lazy' })
    ));

    var $copy = $('<div class="card-copy">');
    $copy.append($('<span class="eyebrow">').text(p.categoria.toUpperCase()));
    $copy.append($('<h3>').text(p.nombre));
    $copy.append($('<p class="precio">').text(dinero.format(p.precio)));
    $copy.append($('<p class="stock">').toggleClass('agotado', p.stock === 0)
      .text(p.stock === 0 ? 'Agotado' : 'Disponibles: ' + p.stock));

    if (p.stock > 0) {
      var idCantidad = 'cantidad-' + p.productoID + '-' + Math.random().toString(36).slice(2, 7);
      var $cantidad = $('<input>', { id: idCantidad, type: 'number', min: 1, max: p.stock, value: 1, 'class': 'cantidad' });
      var $mensaje = $('<p class="card-msg" role="status">');
      var $boton = $('<button type="button" class="button button-small">Agregar al pedido</button>');

      $boton.on('click', function () {
        var cantidad = parseInt($cantidad.val(), 10);
        if (isNaN(cantidad) || cantidad < 1) cantidad = 1;
        var agregadas = Carrito.agregar(p.productoID, cantidad, p.stock);
        $mensaje.text(agregadas > 0
          ? 'Agregado: ' + agregadas + ' al pedido.'
          : 'Ya tienes en tu pedido todas las disponibles.');
      });

      $copy.append($('<div class="card-actions">').append(
        $('<label class="sr-only">').attr('for', idCantidad).text('Cantidad de ' + p.nombre),
        $cantidad,
        $boton
      ), $mensaje);
    }
    return $card.append($copy);
  }

  function pintarTarjetas($contenedor, lista, mensajeVacio) {
    $contenedor.empty();
    if (!lista.length) {
      mostrarAlerta($contenedor, 'info', mensajeVacio);
      return;
    }
    $.each(lista, function (i, p) { $contenedor.append(tarjetaProducto(p)); });
  }

  function textoProductos(n) {
    return n + (n === 1 ? ' producto' : ' productos');
  }

  /* ---------------------------- index.html: favoritos ---------------------------- */
  function cargarFavoritos() {
    var $lista = $('#favoritos-lista');
    mostrarCargando($lista, 'Cargando favoritos…');
    CafeApi.populares(3)
      .done(function (lista) { pintarTarjetas($lista, lista, 'Todavía no hay productos en la carta.'); })
      .fail(function (xhr, estado) { mostrarAlerta($lista, 'error', CafeApi.mensajeError(xhr, estado), cargarFavoritos); });
  }

  /* ---------------------------- menu.html: carta ---------------------------- */
  var categoriaActual = '';

  function cargarCategorias() {
    CafeApi.categorias()
      .done(function (categorias) {
        var $filtros = $('#filtros').empty();
        $.each([''].concat(categorias), function (i, categoria) {
          $filtros.append($('<button type="button">')
            .attr('data-categoria', categoria)
            .attr('aria-pressed', String(categoria === categoriaActual))
            .text(categoria || 'Todos'));
        });
        $('.filter-bar').prop('hidden', false);
      })
      .fail(function (xhr, estado) { mostrarAlerta($('#carta-lista'), 'error', CafeApi.mensajeError(xhr, estado), iniciarCarta); });
  }

  function cargarCarta() {
    var $lista = $('#carta-lista');
    mostrarCargando($lista, 'Cargando la carta…');
    CafeApi.productos(categoriaActual)
      .done(function (lista) {
        pintarTarjetas($lista, lista, 'No hay productos en esta categoría.');
        $('#filter-status').text(textoProductos(lista.length));
      })
      .fail(function (xhr, estado) { mostrarAlerta($lista, 'error', CafeApi.mensajeError(xhr, estado), cargarCarta); });
  }

  function iniciarCarta() {
    cargarCategorias();
    cargarCarta();
  }

  /* ---------------------------- today-special.html ---------------------------- */
  function cargarEspeciales() {
    var $lista = $('#especiales-lista');
    mostrarCargando($lista, 'Cargando la panadería…');
    CafeApi.productos('Panaderia')
      .done(function (lista) { pintarTarjetas($lista, lista, 'Hoy no hay pan disponible.'); })
      .fail(function (xhr, estado) { mostrarAlerta($lista, 'error', CafeApi.mensajeError(xhr, estado), cargarEspeciales); });
  }

  /* ---------------------------- pedido.html ---------------------------- */
  function tablaPedido(p) {
    var $tabla = $('<table class="tabla-pedido">').append(
      '<thead><tr><th scope="col">Producto</th><th scope="col">Cant.</th><th scope="col">Precio</th><th scope="col">Subtotal</th></tr></thead>'
    );
    var $cuerpo = $('<tbody>');
    $.each(p.detalles, function (i, d) {
      $cuerpo.append($('<tr>').append(
        $('<td>').text(d.producto),
        $('<td>').text(d.cantidad),
        $('<td>').text(dinero.format(d.precioUnitario)),
        $('<td>').text(dinero.format(d.subtotal))
      ));
    });
    $tabla.append($cuerpo, $('<tfoot>').append($('<tr>').append(
      $('<th scope="row" colspan="3">').text('Total'),
      $('<td>').text(dinero.format(p.total))
    )));

    var fecha = p.fecha ? new Date(p.fecha).toLocaleString('es-MX', { dateStyle: 'medium', timeStyle: 'short' }) : '';
    return $('<div class="pedido-detalle">').append(
      $('<p class="pedido-cabecera">').append(
        $('<strong>').text('Pedido #' + p.pedidoID),
        $('<span class="estado-pedido">').text(p.estado || '')
      ),
      $('<p class="pedido-meta">').text(p.cliente + (fecha ? ' · ' + fecha : '')),
      $tabla
    );
  }

  function pintarCarrito() {
    var $lista = $('#carrito-lista');
    var $enviar = $('#enviar-pedido');
    var items = Carrito.leer();

    if (!items.length) {
      $lista.empty().append($('<div class="alerta alerta-info">').append(
        $('<p>').text('Tu pedido está vacío. '),
        $('<a class="text-link" href="menu.html">Ir a la carta ↗</a>')
      ));
      $('#carrito-total').text(dinero.format(0));
      $enviar.prop('disabled', true);
      return;
    }

    mostrarCargando($lista, 'Actualizando precios y existencias…');
    $enviar.prop('disabled', true);

    // Se piden todos los productos para mostrar precio y stock actuales
    CafeApi.productos()
      .done(function (productos) {
        var porId = {};
        $.each(productos, function (i, p) { porId[p.productoID] = p; });

        // Quita del carrito lo que ya no existe o se agotó, y ajusta cantidades al stock
        var vigentes = [];
        $.each(items, function (i, item) {
          var p = porId[item.productoID];
          if (p && p.stock > 0) vigentes.push({ productoID: p.productoID, cantidad: Math.min(item.cantidad, p.stock) });
        });
        if (vigentes.length !== items.length || JSON.stringify(vigentes) !== JSON.stringify(items)) {
          Carrito.guardar(vigentes);
        }
        if (!vigentes.length) { pintarCarrito(); return; }

        var total = 0;
        var $ul = $('<ul class="carrito">');
        $.each(vigentes, function (i, item) {
          var p = porId[item.productoID];
          var subtotal = p.precio * item.cantidad;
          total += subtotal;
          var idCantidad = 'carrito-cantidad-' + p.productoID;

          var $cantidad = $('<input>', { id: idCantidad, type: 'number', min: 1, max: p.stock, value: item.cantidad, 'class': 'cantidad' })
            .on('change', function () {
              var valor = parseInt($(this).val(), 10);
              if (isNaN(valor) || valor < 1) valor = 1;
              Carrito.poner(p.productoID, Math.min(valor, p.stock));
              pintarCarrito();
            });
          var $quitar = $('<button type="button" class="link-button">Quitar</button>')
            .attr('aria-label', 'Quitar ' + p.nombre)
            .on('click', function () {
              Carrito.poner(p.productoID, 0);
              pintarCarrito();
            });

          $ul.append($('<li class="carrito-item">').append(
            $('<div>').append(
              $('<strong>').text(p.nombre),
              $('<small>').text(dinero.format(p.precio) + ' c/u · disponibles: ' + p.stock)
            ),
            $('<div class="carrito-controles">').append(
              $('<label class="sr-only">').attr('for', idCantidad).text('Cantidad de ' + p.nombre),
              $cantidad,
              $('<span class="carrito-subtotal">').text(dinero.format(subtotal)),
              $quitar
            )
          ));
        });

        $lista.empty().append($ul);
        $('#carrito-total').text(dinero.format(total));
        $enviar.prop('disabled', false);
      })
      .fail(function (xhr, estado) {
        mostrarAlerta($lista, 'error', CafeApi.mensajeError(xhr, estado), pintarCarrito);
      });
  }

  function iniciarPedido() {
    var $form = $('#pedido-form');
    var $estado = $('#pedido-estado');
    var $enviar = $('#enviar-pedido');

    pintarCarrito();

    $form.on('submit', function (evento) {
      evento.preventDefault();
      $form.find('input').each(function () { this.value = $.trim(this.value); });
      if (!this.checkValidity()) {
        this.reportValidity();
        return;
      }

      var datos = {
        cliente: {
          nombre: $('#nombre').val(),
          email: $('#email').val(),
          telefono: $('#telefono').val() || null
        },
        productos: Carrito.leer()
      };
      if (!datos.productos.length) {
        mostrarAlerta($estado, 'error', 'Agrega al menos un producto a tu pedido.');
        return;
      }

      $enviar.prop('disabled', true).text('Enviando…');
      $estado.empty();

      // POST por AJAX: la página no se recarga
      CafeApi.crearPedido(datos)
        .done(function (pedido) {
          Carrito.vaciar();
          $form[0].reset();
          pintarCarrito();
          $estado.empty().append(
            $('<div class="alerta alerta-exito" role="status">').append(
              $('<p>').text('¡Pedido registrado! Guarda tu número de pedido para consultarlo.'),
              tablaPedido(pedido)
            )
          );
        })
        .fail(function (xhr, estado) {
          mostrarAlerta($estado, 'error', CafeApi.mensajeError(xhr, estado));
          pintarCarrito(); // por si cambió el stock
        })
        .always(function () {
          $enviar.text('Confirmar pedido');
        });
    });

    $('#consulta-form').on('submit', function (evento) {
      evento.preventDefault();
      if (!this.checkValidity()) {
        this.reportValidity();
        return;
      }
      var id = $('#consulta-id').val();
      var $resultado = $('#consulta-resultado');
      mostrarCargando($resultado, 'Buscando el pedido…');

      CafeApi.pedido(id)
        .done(function (pedido) { $resultado.empty().append(tablaPedido(pedido)); })
        .fail(function (xhr, estado) {
          mostrarAlerta($resultado, 'error',
            xhr.status === 404 ? 'No existe el pedido #' + id + '.' : CafeApi.mensajeError(xhr, estado));
        });
    });
  }

  /* ---------------------------- Arranque ---------------------------- */
  $(function () {
    actualizarContador();
    if ($('#favoritos-lista').length) cargarFavoritos();
    if ($('#carta-lista').length) {
      iniciarCarta();
      $('#filtros').on('click', 'button', function () {
        categoriaActual = $(this).attr('data-categoria');
        $('#filtros button').attr('aria-pressed', 'false');
        $(this).attr('aria-pressed', 'true');
        cargarCarta();
      });
    }
    if ($('#especiales-lista').length) cargarEspeciales();
    if ($('#pedido-form').length) iniciarPedido();
  });
})(jQuery);

'use strict';
document.documentElement.classList.add('js');
const menuButton = document.querySelector('.menu-toggle');
const navigation = document.querySelector('#navigation');
function closeMenu() {
  navigation.classList.remove('is-open');
  menuButton.setAttribute('aria-expanded', 'false');
}
menuButton.addEventListener('click', () => {
  const open = navigation.classList.toggle('is-open');
  menuButton.setAttribute('aria-expanded', String(open));
});
navigation.addEventListener('click', event => { if (event.target.closest('a')) closeMenu(); });
document.addEventListener('keydown', event => {
  if (event.key === 'Escape' && navigation.classList.contains('is-open')) {
    closeMenu();
    menuButton.focus();
  }
});
document.addEventListener('click', event => { if (!event.target.closest('.site-header')) closeMenu(); });
window.matchMedia('(max-width: 760px)').addEventListener('change', closeMenu);
document.querySelectorAll('[data-year]').forEach(element => { element.textContent = new Date().getFullYear(); });

const filterBar = document.querySelector('.filter-bar');
if (filterBar) {
  filterBar.hidden = false;
  const buttons = [...document.querySelectorAll('[data-filter]')];
  const cards = [...document.querySelectorAll('[data-category]')];
  buttons.forEach(button => button.addEventListener('click', () => {
    buttons.forEach(item => item.setAttribute('aria-pressed', String(item === button)));
    let count = 0;
    cards.forEach(card => {
      card.hidden = button.dataset.filter !== 'todos' && card.dataset.category !== button.dataset.filter;
      if (!card.hidden) count++;
    });
    document.querySelector('#filter-status').textContent = `${count} ${count === 1 ? 'café para descubrir' : 'cafés para descubrir'}`;
  }));
}

const form = document.querySelector('#contact-form');
if (form) {
  document.querySelector('#download-query').disabled = false;
  const product = new URLSearchParams(window.location.search).get('producto');
  if (product) document.querySelector('#subject').value = `Consulta: ${product}`.slice(0, 160);
  form.addEventListener('submit', event => {
    event.preventDefault();
    const fields = [...form.querySelectorAll('input, textarea')];
    fields.forEach(field => {
      field.value = field.value.trim();
      field.setCustomValidity(field.minLength > 0 && field.value.length < field.minLength
        ? `Escribe al menos ${field.minLength} caracteres, sin contar espacios al inicio o al final.` : '');
    });
    if (!form.reportValidity()) return;
    const data = new FormData(form);
    const text = `CONSULTA PARA CAFE HOUSE\n\nNombre: ${data.get('nombre')}\nCorreo: ${data.get('correo')}\nAsunto: ${data.get('asunto')}\n\n${data.get('mensaje')}\n\nDocumento generado localmente. No enviado. No confirma un pedido.\n`;
    const url = URL.createObjectURL(new Blob(['\uFEFF', text], { type: 'text/plain;charset=utf-8' }));
    const link = document.createElement('a');
    link.href = url;
    link.download = 'consulta-cafe-house.txt';
    document.body.append(link);
    link.click();
    link.remove();
    setTimeout(() => URL.revokeObjectURL(url), 10000);
    document.querySelector('#form-status').textContent = 'Consulta preparada. Se ha solicitado la descarga del archivo; no se ha enviado ningún mensaje.';
  });
  form.addEventListener('input', event => {
    if (typeof event.target.setCustomValidity === 'function') event.target.setCustomValidity('');
    document.querySelector('#form-status').textContent = '';
  });
}

if ('IntersectionObserver' in window && !window.matchMedia('(prefers-reduced-motion: reduce)').matches) {
  const observer = new IntersectionObserver(entries => {
    entries.forEach(entry => {
      if (entry.isIntersecting) {
        entry.target.classList.add('is-visible');
        observer.unobserve(entry.target);
      }
    });
  }, { threshold: 0.12 });
  document.querySelectorAll('.reveal').forEach(element => observer.observe(element));
}

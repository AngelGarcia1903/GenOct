// Comprobaciones de interacción sin dependencias: node checks.cjs
const assert = require('node:assert/strict');
const fs = require('node:fs');
const vm = require('node:vm');
function element(dataset = {}) {
  const classes = new Set();
  return { dataset, hidden: false, value: '', attrs: {}, events: {},
    classList: { add: c => classes.add(c), remove: c => classes.delete(c), contains: c => classes.has(c), toggle: c => { if (classes.has(c)) { classes.delete(c); return false; } classes.add(c); return true; } },
    setAttribute(k, v) { this.attrs[k] = v; },
    addEventListener(k, fn) { this.events[k] = fn; },
    focus() { this.focused = true; }
  };
}
const menu = element(), nav = element(), bar = element(), status = element();
const buttons = ['todos', 'clasicos', 'con-leche'].map(filter => element({filter}));
const cards = ['clasicos', 'con-leche', 'con-leche'].map(category => element({category}));
const elements = {'.menu-toggle':menu, '#navigation':nav, '.filter-bar':bar, '#filter-status':status};
const events = {};
const document = {documentElement:element(), querySelector:s => elements[s] || null,
  querySelectorAll:s => ({'[data-filter]':buttons, '[data-category]':cards}[s] || []),
  addEventListener:(k, fn) => {events[k] = fn;}};
vm.runInNewContext(fs.readFileSync('js/cafe-house.js', 'utf8'), {document, window:{matchMedia:()=>({addEventListener(){}, matches:false})}, Date});
menu.events.click(); assert.equal(menu.attrs['aria-expanded'], 'true'); assert(nav.classList.contains('is-open'));
events.keydown({key:'Escape'}); assert.equal(menu.attrs['aria-expanded'], 'false'); assert(menu.focused);
buttons[1].events.click(); assert.deepEqual(cards.map(c=>c.hidden), [false,true,true]); assert.equal(status.textContent,'1 café para descubrir');
buttons[2].events.click(); assert.deepEqual(cards.map(c=>c.hidden), [true,false,false]); assert.equal(buttons[1].attrs['aria-pressed'],'false');
buttons[0].events.click(); assert(cards.every(c=>!c.hidden)); assert.equal(status.textContent,'3 cafés para descubrir');
console.log('OK: apertura del menú, Escape y foco, filtros y contador accesible.');

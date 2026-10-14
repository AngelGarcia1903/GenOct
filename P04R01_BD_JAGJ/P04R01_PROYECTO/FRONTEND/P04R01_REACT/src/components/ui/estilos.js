// Clases de Tailwind compartidas. Se guardan en un .js (no .jsx) porque no son componentes.

// Clases de un <input> o <select>. Si hay error, el borde y el anillo de foco se pintan de rojo.
export function claseInput(conError = false) {
  const base =
    'block w-full rounded-lg border bg-white px-3 py-2 text-sm text-slate-900 shadow-sm outline-none transition ' +
    'placeholder:text-slate-400 focus:ring-4 ' +
    'disabled:cursor-not-allowed disabled:bg-slate-100 disabled:text-slate-500'
  const color = conError
    ? 'border-red-400 focus:border-red-500 focus:ring-red-100'
    : 'border-slate-300 focus:border-marca-500 focus:ring-marca-100'
  return `${base} ${color}`
}

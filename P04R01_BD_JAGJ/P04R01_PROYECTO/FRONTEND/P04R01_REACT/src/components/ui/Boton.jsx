import { LoaderCircle } from 'lucide-react'

const VARIANTES = {
  primario: 'bg-marca-600 text-white shadow-sm hover:bg-marca-700 focus-visible:outline-marca-600',
  secundario: 'bg-white text-slate-700 shadow-sm ring-1 ring-inset ring-slate-300 hover:bg-slate-50 focus-visible:outline-slate-400',
  peligro: 'bg-red-600 text-white shadow-sm hover:bg-red-700 focus-visible:outline-red-600',
  exito: 'bg-emerald-600 text-white shadow-sm hover:bg-emerald-700 focus-visible:outline-emerald-600',
  fantasma: 'text-slate-600 hover:bg-slate-100 focus-visible:outline-slate-400',
}

// Botón reutilizable.
// Props propias: variante, icono (componente de lucide-react), cargando.
// Cualquier otra prop (onClick, type, disabled, title...) se pasa tal cual al <button> con {...resto}.
function Boton({ variante = 'primario', icono: Icono, cargando = false, className = '', children, ...resto }) {
  return (
    <button
      type="button"
      {...resto}
      disabled={resto.disabled || cargando}
      className={
        'inline-flex items-center justify-center gap-2 rounded-lg px-4 py-2 text-sm font-semibold transition ' +
        'focus-visible:outline-2 focus-visible:outline-offset-2 disabled:cursor-not-allowed disabled:opacity-50 ' +
        `${VARIANTES[variante]} ${className}`
      }
    >
      {cargando ? (
        <LoaderCircle className="size-4 animate-spin" aria-hidden="true" />
      ) : (
        Icono && <Icono className="size-4" aria-hidden="true" />
      )}
      {children}
    </button>
  )
}

export default Boton

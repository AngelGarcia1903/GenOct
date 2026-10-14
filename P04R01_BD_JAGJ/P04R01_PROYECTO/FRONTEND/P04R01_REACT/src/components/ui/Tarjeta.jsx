import { Check } from 'lucide-react'

// Contenedor blanco con encabezado. "children" es todo lo que se escribe entre <Tarjeta> y </Tarjeta>.
// Props: titulo, subtitulo, icono, paso (número opcional), completado, acciones (JSX a la derecha del título)
function Tarjeta({ titulo, subtitulo, icono: Icono, paso, completado = false, acciones, className = '', children }) {
  return (
    <section className={`rounded-2xl border border-slate-200 bg-white p-5 shadow-sm sm:p-6 ${className}`}>
      {titulo && (
        <header className="mb-5 flex items-start gap-3">
          {paso !== undefined ? (
            <span
              className={`flex size-9 shrink-0 items-center justify-center rounded-full text-sm font-bold ${
                completado ? 'bg-emerald-100 text-emerald-700' : 'bg-marca-100 text-marca-700'
              }`}
            >
              {completado ? <Check className="size-5" aria-label="Completado" /> : paso}
            </span>
          ) : (
            Icono && (
              <span className="flex size-9 shrink-0 items-center justify-center rounded-full bg-marca-100 text-marca-700">
                <Icono className="size-5" aria-hidden="true" />
              </span>
            )
          )}
          <div className="min-w-0 flex-1">
            <h2 className="flex items-center gap-2 text-lg font-semibold text-slate-900">
              {paso !== undefined && Icono && <Icono className="size-5 text-slate-400" aria-hidden="true" />}
              {titulo}
            </h2>
            {subtitulo && <p className="mt-0.5 text-sm text-slate-500">{subtitulo}</p>}
          </div>
          {acciones && <div className="shrink-0">{acciones}</div>}
        </header>
      )}
      {children}
    </section>
  )
}

export default Tarjeta

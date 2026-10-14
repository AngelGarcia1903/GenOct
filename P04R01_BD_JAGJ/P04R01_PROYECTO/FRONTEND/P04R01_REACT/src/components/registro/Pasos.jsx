import { Check } from 'lucide-react'

// Indicador de avance (stepper). Props: pasos -> [{ titulo, completado }]
function Pasos({ pasos }) {
  return (
    <ol className="grid gap-3 sm:grid-cols-3">
      {pasos.map((paso, i) => (
        <li
          key={paso.titulo}
          className={`flex items-center gap-3 rounded-xl border px-4 py-3 transition ${
            paso.completado ? 'border-emerald-200 bg-emerald-50' : 'border-slate-200 bg-white'
          }`}
        >
          <span
            className={`flex size-8 shrink-0 items-center justify-center rounded-full text-sm font-bold ${
              paso.completado ? 'bg-emerald-600 text-white' : 'bg-slate-100 text-slate-500'
            }`}
          >
            {paso.completado ? <Check className="size-4" aria-hidden="true" /> : i + 1}
          </span>
          <div className="min-w-0">
            <p className="text-sm font-semibold text-slate-900">{paso.titulo}</p>
            <p className={`text-xs ${paso.completado ? 'text-emerald-700' : 'text-slate-500'}`}>
              {paso.completado ? 'Completado' : 'Pendiente'}
            </p>
          </div>
        </li>
      ))}
    </ol>
  )
}

export default Pasos

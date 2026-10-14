import { CircleAlert } from 'lucide-react'

// Envuelve un input con su etiqueta, texto de ayuda y mensaje de error.
// El input va como "children":  <Campo id="email" etiqueta="Email" error={...}><input id="email" /></Campo>
function Campo({ id, etiqueta, error, ayuda, children }) {
  return (
    <div className="space-y-1.5">
      <label htmlFor={id} className="block text-sm font-medium text-slate-700">
        {etiqueta}
      </label>
      {children}
      {error ? (
        <p className="flex items-start gap-1.5 text-sm text-red-600" role="alert">
          <CircleAlert className="mt-0.5 size-4 shrink-0" aria-hidden="true" />
          {error}
        </p>
      ) : (
        ayuda && <p className="text-xs text-slate-500">{ayuda}</p>
      )}
    </div>
  )
}

export default Campo

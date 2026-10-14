import { CircleAlert, TriangleAlert, Info, CircleCheck } from 'lucide-react'

// Un objeto de configuración por tipo: colores e ícono.
const TIPOS = {
  error: { clases: 'border-red-200 bg-red-50 text-red-800', Icono: CircleAlert },
  aviso: { clases: 'border-amber-200 bg-amber-50 text-amber-800', Icono: TriangleAlert },
  info: { clases: 'border-sky-200 bg-sky-50 text-sky-800', Icono: Info },
  exito: { clases: 'border-emerald-200 bg-emerald-50 text-emerald-800', Icono: CircleCheck },
}

// Caja de mensaje. Props: tipo ('error' | 'aviso' | 'info' | 'exito'), titulo, children
function Alerta({ tipo = 'info', titulo, className = '', children }) {
  const { clases, Icono } = TIPOS[tipo]
  return (
    <div
      className={`flex gap-3 rounded-lg border px-4 py-3 text-sm ${clases} ${className}`}
      role={tipo === 'error' ? 'alert' : 'status'}
    >
      <Icono className="mt-0.5 size-5 shrink-0" aria-hidden="true" />
      <div className="min-w-0">
        {titulo && <p className="font-semibold">{titulo}</p>}
        {children && <div className={titulo ? 'mt-0.5' : ''}>{children}</div>}
      </div>
    </div>
  )
}

export default Alerta

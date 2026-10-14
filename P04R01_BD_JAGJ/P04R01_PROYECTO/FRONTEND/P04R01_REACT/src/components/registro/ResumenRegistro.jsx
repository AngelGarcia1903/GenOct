import { CircleCheck, Printer } from 'lucide-react'
import Boton from '../ui/Boton'

// Fila de la ficha: etiqueta a la izquierda, valor a la derecha
function Fila({ etiqueta, children, mono = false }) {
  return (
    <div className="grid grid-cols-3 gap-4 px-5 py-3 text-sm">
      <dt className="font-medium text-slate-500">{etiqueta}</dt>
      <dd className={`col-span-2 text-slate-900 ${mono ? 'font-mono' : ''}`}>{children}</dd>
    </div>
  )
}

// Toque personal: "ficha" del registro guardado, lista para imprimir o guardar como PDF.
// Props: registro -> lo que regresó POST api/Persona/finalizar
function ResumenRegistro({ registro }) {
  const { persona } = registro
  const fecha = registro.fechaRegistro ? new Date(registro.fechaRegistro).toLocaleString('es-MX') : ''

  return (
    <section className="overflow-hidden rounded-2xl border border-emerald-200 bg-white shadow-sm print:border-slate-300 print:shadow-none">
      <header className="flex flex-wrap items-center justify-between gap-4 bg-emerald-50 px-5 py-4">
        <div className="flex items-center gap-3">
          <CircleCheck className="size-8 text-emerald-600" aria-hidden="true" />
          <div>
            <h2 className="text-lg font-semibold text-emerald-900">Registro #{registro.idRegistro} guardado</h2>
            <p className="text-sm text-emerald-700">Fecha de registro: {fecha}</p>
          </div>
        </div>
        <Boton variante="secundario" icono={Printer} onClick={() => window.print()} className="print:hidden">
          Imprimir / Guardar PDF
        </Boton>
      </header>

      <dl className="divide-y divide-slate-100">
        <Fila etiqueta="ID de usuario" mono>{persona.idUsuario}</Fila>
        <Fila etiqueta="Nombre">{persona.nombreNormalizado}</Fila>
        <Fila etiqueta="Hobby">{persona.hobby}</Fila>
        <Fila etiqueta="Fecha de nacimiento">{persona.fechaNacimiento} ({persona.edad} años)</Fila>
        <Fila etiqueta="Email">{persona.email}</Fila>
        <Fila etiqueta="Preposiciones">{persona.preposiciones.join(', ') || '—'}</Fila>
        <Fila etiqueta="Código postal" mono>{registro.cp}</Fila>
        <Fila etiqueta="Estado">{registro.estado} ({registro.codEstado})</Fila>
        <Fila etiqueta="Municipio">{registro.municipio}</Fila>
        <Fila etiqueta="Colonia">{registro.colonia} ({registro.codColonia})</Fila>
      </dl>
    </section>
  )
}

export default ResumenRegistro

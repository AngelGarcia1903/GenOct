import { useState } from 'react'
import { toast } from 'sonner'
import { IdCard, Copy, Check, Sparkles } from 'lucide-react'
import Preposiciones from './Preposiciones'
import Tarjeta from '../ui/Tarjeta'
import Alerta from '../ui/Alerta'

// Un dato del resultado: etiqueta pequeña arriba y valor abajo
function Dato({ etiqueta, children }) {
  return (
    <div>
      <dt className="text-xs font-medium uppercase tracking-wide text-slate-500">{etiqueta}</dt>
      <dd className="mt-0.5 text-sm text-slate-900">{children}</dd>
    </div>
  )
}

// Resultado de la Característica 2: nombre normalizado, ID, hobby y preposiciones.
// Props: resultado -> lo que regresó POST api/Persona/procesar
function ResultadoPersona({ resultado }) {
  const [copiado, setCopiado] = useState(false)

  async function copiarId() {
    try {
      await navigator.clipboard.writeText(resultado.idUsuario)
      setCopiado(true)
      toast.success(`ID ${resultado.idUsuario} copiado`)
      setTimeout(() => setCopiado(false), 2000)
    } catch {
      toast.error('No se pudo copiar el ID')
    }
  }

  return (
    <Tarjeta icono={Sparkles} titulo="Resultado del procesamiento" subtitulo="Revisa los datos antes de finalizar.">
      <div className="grid gap-6 lg:grid-cols-[minmax(0,18rem)_1fr]">
        {/* ID destacado */}
        <div className="flex flex-col justify-between rounded-xl bg-gradient-to-br from-marca-600 to-marca-900 p-5 text-white">
          <div className="flex items-center gap-2 text-sm text-marca-100">
            <IdCard className="size-5" aria-hidden="true" />
            ID del usuario
          </div>
          <p className="my-4 font-mono text-3xl font-bold tracking-wider">{resultado.idUsuario}</p>
          <button
            type="button"
            onClick={copiarId}
            className="inline-flex w-fit items-center gap-1.5 rounded-lg bg-white/15 px-3 py-1.5 text-xs font-medium transition hover:bg-white/25"
          >
            {copiado ? <Check className="size-3.5" aria-hidden="true" /> : <Copy className="size-3.5" aria-hidden="true" />}
            {copiado ? 'Copiado' : 'Copiar ID'}
          </button>
        </div>

        <div className="space-y-4">
          <div>
            <p className="text-xs font-medium uppercase tracking-wide text-slate-500">Nombre normalizado</p>
            <p className="text-xl font-semibold text-slate-900">{resultado.nombreNormalizado}</p>
          </div>

          <dl className="grid gap-4 sm:grid-cols-3">
            <Dato etiqueta="Nombre(s)">{resultado.nombres}</Dato>
            <Dato etiqueta="Apellido paterno">{resultado.apellidoPaterno}</Dato>
            <Dato etiqueta="Apellido materno">{resultado.apellidoMaterno || <span className="text-slate-400">(no capturado)</span>}</Dato>
            <Dato etiqueta="Hobby">{resultado.hobby}</Dato>
            <Dato etiqueta="Email">{resultado.email}</Dato>
            <Dato etiqueta="Edad">{resultado.edad} años</Dato>
          </dl>

          {resultado.avisos.length > 0 && (
            <Alerta tipo="aviso" titulo="Avisos">
              <ul className="list-inside list-disc">
                {resultado.avisos.map((aviso) => <li key={aviso}>{aviso}</li>)}
              </ul>
            </Alerta>
          )}

          <Preposiciones lista={resultado.preposiciones} />
        </div>
      </div>
    </Tarjeta>
  )
}

export default ResultadoPersona

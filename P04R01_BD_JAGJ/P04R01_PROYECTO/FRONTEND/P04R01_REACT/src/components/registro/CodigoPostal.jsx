import { useState } from 'react'
import { MapPin, Search } from 'lucide-react'
import { consultarCP } from '../../services/registroService'
import { validarCP } from '../../utils/validaciones'
import Tarjeta from '../ui/Tarjeta'
import Campo from '../ui/Campo'
import Boton from '../ui/Boton'
import Alerta from '../ui/Alerta'
import { claseInput } from '../ui/estilos'

// Característica 1: consulta de información a partir de un Código Postal.
// Props:
//   datos       -> respuesta de la API (o null si aún no hay consulta válida). Vive en el padre.
//   coloniaSel  -> código de la colonia seleccionada ('' si ninguna)
//   onDatos     -> avisa al padre cuando llegan datos nuevos (o null para limpiarlos)
//   onColonia   -> avisa al padre qué colonia se seleccionó
function CodigoPostal({ datos, coloniaSel, onDatos, onColonia }) {
  const [cp, setCp] = useState('')
  const [tocado, setTocado] = useState(false)       // para no mostrar errores antes de que escriba algo
  const [avisoLetras, setAvisoLetras] = useState('') // si intentó escribir algo que no es número
  const [consultando, setConsultando] = useState(false)
  const [error, setError] = useState('')

  // Estado DERIVADO: se calcula en cada render a partir de "cp"; no se guarda en otro useState.
  const errorCP = validarCP(cp)
  const cpValido = errorCP === ''

  function manejarCambio(evento) {
    const escrito = evento.target.value
    const soloNumeros = escrito.replace(/\D/g, '') // regla: aceptar únicamente números
    setAvisoLetras(escrito !== soloNumeros ? 'Solo se aceptan números; se ignoraron los demás caracteres.' : '')
    setCp(soloNumeros)
    setTocado(true)
    setError('')
    if (datos) onDatos(null) // si cambia el CP, los resultados anteriores ya no aplican
  }

  async function consultar(evento) {
    evento.preventDefault()
    if (!cpValido) return
    setConsultando(true)
    setError('')
    try {
      const respuesta = await consultarCP(cp)
      onDatos(respuesta)
    } catch (err) {
      // 404 sin información, 503 sin comunicación, 502 error del servicio, 0 API apagada...
      setError(err.message)
      onDatos(null)
    } finally {
      setConsultando(false)
    }
  }

  const coloniaElegida = datos?.colonias.find((c) => c.codigo === coloniaSel)
  const completado = Boolean(datos && coloniaSel)

  return (
    <Tarjeta paso={1} completado={completado} icono={MapPin} titulo="Código postal"
      subtitulo="Consulta el estado y las colonias de tu código postal.">
      <form onSubmit={consultar} className="flex items-start gap-2">
        <div className="flex-1">
          <Campo id="cp" etiqueta="Código postal" error={tocado && !cpValido ? errorCP : ''}
            ayuda="5 dígitos, solo números.">
            <input
              id="cp"
              inputMode="numeric"
              autoComplete="postal-code"
              maxLength={5}
              placeholder="Ej. 06700"
              value={cp}
              onChange={manejarCambio}
              onBlur={() => setTocado(true)}
              aria-invalid={tocado && !cpValido}
              className={`${claseInput(tocado && !cpValido)} font-mono text-base tracking-widest`}
            />
          </Campo>
        </div>
        {/* El botón solo se habilita cuando el CP es válido. mt-7 lo alinea con el input (debajo de la etiqueta) */}
        <Boton type="submit" icono={Search} cargando={consultando} disabled={!cpValido} className="mt-7">
          {consultando ? 'Consultando' : 'Consultar'}
        </Boton>
      </form>

      <div className="mt-4 space-y-3">
        {avisoLetras && <Alerta tipo="aviso">{avisoLetras}</Alerta>}
        {error && <Alerta tipo="error" titulo="No se pudo consultar">{error}</Alerta>}

        {datos && (
          <>
            <dl className="grid grid-cols-2 gap-3 rounded-xl bg-slate-50 p-4 text-sm">
              <div>
                <dt className="text-xs font-medium uppercase tracking-wide text-slate-500">Estado</dt>
                <dd className="font-semibold text-slate-900">{datos.estado}</dd>
              </div>
              <div>
                <dt className="text-xs font-medium uppercase tracking-wide text-slate-500">Código de estado</dt>
                <dd className="font-mono font-semibold text-slate-900">{datos.codEstado}</dd>
              </div>
              <div className="col-span-2">
                <dt className="text-xs font-medium uppercase tracking-wide text-slate-500">Municipio</dt>
                <dd className="text-slate-900">
                  {datos.municipio} <span className="font-mono text-slate-500">({datos.codMunicipio})</span>
                </dd>
              </div>
            </dl>

            <Campo id="colonia" etiqueta={`Colonia (${datos.colonias.length} encontradas)`}>
              <select id="colonia" value={coloniaSel} onChange={(e) => onColonia(e.target.value)}
                className={claseInput()}>
                <option value="">— Selecciona una colonia —</option>
                {datos.colonias.map((colonia) => (
                  <option key={colonia.codigo} value={colonia.codigo}>
                    {colonia.descripcion}
                  </option>
                ))}
              </select>
            </Campo>

            {coloniaElegida && (
              <p className="flex items-center justify-between rounded-lg border border-marca-200 bg-marca-50 px-4 py-2.5 text-sm">
                <span className="text-marca-900">Código de la colonia</span>
                <span className="font-mono text-base font-bold text-marca-700">{coloniaElegida.codigo}</span>
              </p>
            )}
          </>
        )}
      </div>
    </Tarjeta>
  )
}

export default CodigoPostal

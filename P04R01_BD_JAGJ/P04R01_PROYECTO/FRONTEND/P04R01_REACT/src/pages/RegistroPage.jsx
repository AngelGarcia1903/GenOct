import { useState } from 'react'
import { toast } from 'sonner'
import { Lock } from 'lucide-react'
import Pasos from '../components/registro/Pasos'
import CodigoPostal from '../components/registro/CodigoPostal'
import PersonaForm from '../components/registro/PersonaForm'
import ResultadoPersona from '../components/registro/ResultadoPersona'
import AccionesRegistro from '../components/registro/AccionesRegistro'
import ResumenRegistro from '../components/registro/ResumenRegistro'
import Alerta from '../components/ui/Alerta'
import DialogoConfirmar from '../components/ui/DialogoConfirmar'
import { finalizarRegistro } from '../services/registroService'

// Página de la prueba Thona. Une las 3 características y guarda el estado que comparten.
function RegistroPage() {
  const [datosCP, setDatosCP] = useState(null)           // respuesta de api/CodigoPostal
  const [coloniaSel, setColoniaSel] = useState('')       // código de la colonia elegida
  const [resultado, setResultado] = useState(null)       // respuesta de api/Persona/procesar
  const [personaEnviada, setPersonaEnviada] = useState(null) // lo que se mandó a procesar (se reenvía al finalizar)
  const [finalizado, setFinalizado] = useState(false)
  const [guardando, setGuardando] = useState(false)
  const [registro, setRegistro] = useState(null)         // respuesta de api/Persona/finalizar
  const [error, setError] = useState('')
  const [confirmando, setConfirmando] = useState(false)  // diálogo "¿Seguro que deseas finalizar?"
  const [intento, setIntento] = useState(0)              // se usa como "key" para reiniciar los formularios

  // Estado derivado: requisitos para finalizar (también son las instrucciones para el usuario)
  const cpListo = Boolean(datosCP && coloniaSel)
  const requisitos = [
    { texto: datosCP ? 'Código postal consultado' : 'Consultar un código postal válido', cumplido: Boolean(datosCP) },
    { texto: 'Seleccionar una colonia', cumplido: Boolean(coloniaSel) },
    { texto: 'Procesar los datos de la persona', cumplido: Boolean(resultado) },
  ]
  const pasos = [
    { titulo: 'Código postal', completado: cpListo },
    { titulo: 'Datos de la persona', completado: Boolean(resultado) },
    { titulo: 'Finalizar', completado: finalizado },
  ]

  function manejarDatosCP(datos) {
    setDatosCP(datos)
    setColoniaSel('') // colonias nuevas -> la selección anterior ya no aplica
  }

  function manejarProcesado(res, datosEnviados) {
    setResultado(res)
    setPersonaEnviada(datosEnviados)
    toast.success('Datos procesados', { description: `ID generado: ${res.idUsuario}` })
  }

  async function finalizar() {
    setConfirmando(false)
    setGuardando(true)
    setError('')
    try {
      const guardado = await finalizarRegistro(personaEnviada, datosCP.cp, coloniaSel)
      setRegistro(guardado)
      setFinalizado(true)
      toast.success(`Registro #${guardado.idRegistro} guardado`)
    } catch (err) {
      setError(err.message)
      toast.error('No se pudo finalizar')
    } finally {
      setGuardando(false)
    }
  }

  function reiniciar() {
    setDatosCP(null)
    setColoniaSel('')
    setResultado(null)
    setPersonaEnviada(null)
    setRegistro(null)
    setError('')
    setFinalizado(false)
    setIntento((n) => n + 1) // nueva key -> CodigoPostal y PersonaForm se vuelven a crear vacíos
    window.scrollTo({ top: 0, behavior: 'smooth' })
  }

  return (
    <div className="space-y-6">
      <div className="print:hidden">
        <h1 className="text-2xl font-bold tracking-tight text-slate-900">Registro de usuario</h1>
        <p className="mt-1 text-sm text-slate-500">
          Consulta tu código postal, captura tus datos y finaliza para guardar el registro.
        </p>
      </div>

      <div className="print:hidden">
        <Pasos pasos={pasos} />
      </div>

      {finalizado && (
        <div className="print:hidden">
          <Alerta tipo="info" titulo="Información finalizada">
            <span className="inline-flex items-center gap-1.5">
              <Lock className="size-3.5" aria-hidden="true" />
              Los datos quedaron bloqueados y ya no se pueden editar.
            </span>
          </Alerta>
        </div>
      )}

      {registro && <ResumenRegistro registro={registro} />}

      {/* fieldset disabled deshabilita TODOS los inputs y botones de adentro: así ya no se puede editar.
          key={intento}: al reiniciar, React vuelve a crear los formularios vacíos. */}
      <fieldset
        key={intento}
        disabled={finalizado || guardando}
        className={`grid min-w-0 items-start gap-6 lg:grid-cols-2 print:hidden ${finalizado ? 'opacity-75' : ''}`}
      >
        <CodigoPostal
          datos={datosCP}
          coloniaSel={coloniaSel}
          onDatos={manejarDatosCP}
          onColonia={setColoniaSel}
        />
        <PersonaForm
          procesado={Boolean(resultado)}
          onProcesado={manejarProcesado}
          onCambio={() => setResultado(null)}
        />
      </fieldset>

      {resultado && (
        <div className="print:hidden">
          <ResultadoPersona resultado={resultado} />
        </div>
      )}

      {error && <Alerta tipo="error" titulo="No se pudo finalizar">{error}</Alerta>}

      <AccionesRegistro
        requisitos={requisitos}
        finalizado={finalizado}
        guardando={guardando}
        onFinalizar={() => setConfirmando(true)}
        onReiniciar={reiniciar}
      />

      <DialogoConfirmar
        abierto={confirmando}
        titulo="¿Finalizar el registro?"
        mensaje="Se guardará en la base de datos y ya no podrás editar la información."
        textoConfirmar="Sí, finalizar"
        onConfirmar={finalizar}
        onCancelar={() => setConfirmando(false)}
      />
    </div>
  )
}

export default RegistroPage

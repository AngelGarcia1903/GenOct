import { useState } from 'react'
import { UserRound, Sparkles } from 'lucide-react'
import { procesarPersona } from '../../services/registroService'
import {
  validarNombre,
  validarHobby,
  validarFechaNacimiento,
  validarEmail,
  hoyISO,
} from '../../utils/validaciones'
import Tarjeta from '../ui/Tarjeta'
import Campo from '../ui/Campo'
import Boton from '../ui/Boton'
import Alerta from '../ui/Alerta'
import { claseInput } from '../ui/estilos'

const VALORES_INICIALES = { nombreCompleto: '', hobby: '', fechaNacimiento: '', email: '' }

// Característica 2: captura y procesamiento de la persona.
// Props:
//   procesado                             -> true si ya hay un resultado vigente (para la palomita del paso)
//   onProcesado(resultado, datosEnviados) -> la API procesó bien a la persona
//   onCambio()                            -> el usuario modificó algo; el resultado anterior ya no aplica
function PersonaForm({ procesado, onProcesado, onCambio }) {
  const [valores, setValores] = useState(VALORES_INICIALES)
  const [tocados, setTocados] = useState({})                 // campos que el usuario ya visitó
  const [intentoEnviar, setIntentoEnviar] = useState(false)  // después de presionar "Procesar" se muestran todos los errores
  const [erroresServidor, setErroresServidor] = useState({}) // errores por campo que regresó la API
  const [errorGeneral, setErrorGeneral] = useState('')
  const [procesando, setProcesando] = useState(false)

  // Estado derivado: los errores se recalculan en cada render con los valores actuales.
  const erroresCliente = {
    nombreCompleto: validarNombre(valores.nombreCompleto),
    hobby: validarHobby(valores.hobby),
    fechaNacimiento: validarFechaNacimiento(valores.fechaNacimiento),
    email: validarEmail(valores.email),
  }
  const hayErrores = Object.values(erroresCliente).some((e) => e !== '')

  function errorDe(campo) {
    const visible = tocados[campo] || intentoEnviar
    return (visible && erroresCliente[campo]) || erroresServidor[campo] || ''
  }

  function manejarCambio(evento) {
    const { name, value } = evento.target
    setValores({ ...valores, [name]: value })
    setErroresServidor({ ...erroresServidor, [name]: '' })
    setErrorGeneral('')
    onCambio()
  }

  function manejarBlur(evento) {
    setTocados({ ...tocados, [evento.target.name]: true })
  }

  async function manejarEnvio(evento) {
    evento.preventDefault()
    setIntentoEnviar(true)
    if (hayErrores) return // las instrucciones ya se muestran debajo de cada campo

    const datos = { ...valores, fechaNacimiento: valores.fechaNacimiento || null }
    setProcesando(true)
    setErrorGeneral('')
    try {
      const resultado = await procesarPersona(datos)
      onProcesado(resultado, datos)
    } catch (err) {
      setErroresServidor(err.errores ?? {})
      setErrorGeneral(err.message)
    } finally {
      setProcesando(false)
    }
  }

  // Atributos comunes de cada input: valor, manejadores y estilo según si tiene error.
  // Se "esparcen" en el input con {...propsDe('email')}
  function propsDe(campo) {
    const error = errorDe(campo)
    return {
      id: campo,
      name: campo,
      value: valores[campo],
      onChange: manejarCambio,
      onBlur: manejarBlur,
      'aria-invalid': error !== '',
      className: claseInput(error !== ''),
    }
  }

  return (
    <Tarjeta paso={2} completado={procesado} icono={UserRound} titulo="Datos de la persona"
      subtitulo="Normalizamos tu nombre y generamos tu ID de usuario.">
      <form onSubmit={manejarEnvio} noValidate className="space-y-4">
        <Campo id="nombreCompleto" etiqueta="Nombre completo" error={errorDe('nombreCompleto')}
          ayuda="Orden: nombre(s), apellido paterno, apellido materno.">
          <input {...propsDe('nombreCompleto')} maxLength={100} autoComplete="name"
            placeholder="Ej. maría de la cruz pérez" />
        </Campo>

        <Campo id="hobby" etiqueta="Hobby" error={errorDe('hobby')}>
          <input {...propsDe('hobby')} maxLength={150} placeholder="Ej. leer novelas" />
        </Campo>

        <div className="grid gap-4 sm:grid-cols-2">
          <Campo id="fechaNacimiento" etiqueta="Fecha de nacimiento" error={errorDe('fechaNacimiento')}
            ayuda="Debes tener al menos 5 años.">
            <input {...propsDe('fechaNacimiento')} type="date" max={hoyISO()} />
          </Campo>

          <Campo id="email" etiqueta="Email" error={errorDe('email')}>
            <input {...propsDe('email')} type="email" maxLength={100} autoComplete="email"
              placeholder="nombre@dominio.com" />
          </Campo>
        </div>

        {errorGeneral && <Alerta tipo="error">{errorGeneral}</Alerta>}

        <Boton type="submit" icono={Sparkles} cargando={procesando} className="w-full sm:w-auto">
          {procesando ? 'Procesando' : 'Procesar'}
        </Boton>
      </form>
    </Tarjeta>
  )
}

export default PersonaForm

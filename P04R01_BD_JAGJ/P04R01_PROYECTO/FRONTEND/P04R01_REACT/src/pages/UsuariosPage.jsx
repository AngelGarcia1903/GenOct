import { useState, useEffect } from 'react'
import { toast } from 'sonner'
import { Users, RefreshCw } from 'lucide-react'
import UsuarioForm from '../components/UsuarioForm'
import UsuariosTabla from '../components/UsuariosTabla'
import BuscarUsuario from '../components/BuscarUsuario'
import Tarjeta from '../components/ui/Tarjeta'
import Boton from '../components/ui/Boton'
import Alerta from '../components/ui/Alerta'
import DialogoConfirmar from '../components/ui/DialogoConfirmar'
import {
  obtenerUsuarios,
  crearUsuario,
  actualizarUsuario,
  eliminarUsuario,
} from '../services/usuariosService'

// Página del CRUD de usuarios. Guarda el estado compartido (la lista de usuarios)
// y se lo pasa a los componentes hijos por props.
function UsuariosPage() {
  const [usuarios, setUsuarios] = useState([])            // lista que viene de la API
  const [usuarioEditar, setUsuarioEditar] = useState(null) // usuario seleccionado para editar
  const [cargando, setCargando] = useState(true)
  const [error, setError] = useState('')
  const [idEliminar, setIdEliminar] = useState(null)       // id pendiente de confirmar en el diálogo

  // Vuelve a pedir la lista a la API (después de crear, editar, eliminar o con "Recargar").
  async function cargarUsuarios() {
    try {
      const datos = await obtenerUsuarios()
      setUsuarios(datos)
      setError('')
    } catch (err) {
      setError(`No se pudo cargar la lista. ¿La API está corriendo? (${err.message})`)
    } finally {
      setCargando(false)
    }
  }

  // useEffect con [] vacío: se ejecuta UNA vez, cuando el componente aparece en pantalla.
  // Aquí se usa la promesa directamente (.then/.catch) para que los setState ocurran
  // cuando llega la respuesta, no en el momento en que corre el efecto.
  useEffect(() => {
    obtenerUsuarios()
      .then((datos) => setUsuarios(datos))
      .catch((err) => setError(`No se pudo cargar la lista. ¿La API está corriendo? (${err.message})`))
      .finally(() => setCargando(false))
  }, [])

  // Crear o actualizar, según si hay un usuario en edición. Regresa true si salió bien.
  async function guardarUsuario(datos) {
    setError('')
    try {
      if (usuarioEditar) {
        await actualizarUsuario(usuarioEditar.idUsuarios, datos)
        toast.success('Usuario actualizado')
        setUsuarioEditar(null)
      } else {
        await crearUsuario(datos)
        toast.success('Usuario creado')
      }
      await cargarUsuarios() // la API no regresa el registro, así que volvemos a pedir la lista
      return true
    } catch (err) {
      setError(err.message)
      return false
    }
  }

  function iniciarEdicion(usuario) {
    setError('')
    setUsuarioEditar(usuario)
    window.scrollTo({ top: 0, behavior: 'smooth' })
  }

  async function confirmarEliminar() {
    const id = idEliminar
    setIdEliminar(null)
    setError('')
    try {
      await eliminarUsuario(id)
      toast.success(`Usuario #${id} eliminado`)
      if (usuarioEditar?.idUsuarios === id) setUsuarioEditar(null)
      await cargarUsuarios()
    } catch (err) {
      setError(err.message)
    }
  }

  return (
    <div className="space-y-6">
      <div>
        <h1 className="text-2xl font-bold tracking-tight text-slate-900">Usuarios</h1>
        <p className="mt-1 text-sm text-slate-500">Alta, consulta, edición y baja de la tabla TBLUsuarios.</p>
      </div>

      {error && <Alerta tipo="error">{error}</Alerta>}

      <div className="grid items-start gap-6 lg:grid-cols-[22rem_1fr]">
        <div className="space-y-6">
          {/* key: cuando cambia (otro usuario o "nuevo"), React crea el formulario de nuevo con sus datos */}
          <UsuarioForm
            key={usuarioEditar?.idUsuarios ?? 'nuevo'}
            usuarioEditar={usuarioEditar}
            onGuardar={guardarUsuario}
            onCancelar={() => setUsuarioEditar(null)}
          />
          <BuscarUsuario />
        </div>

        <Tarjeta
          icono={Users}
          titulo="Lista de usuarios"
          subtitulo={cargando ? 'Cargando…' : `${usuarios.length} registros`}
          acciones={<Boton variante="secundario" icono={RefreshCw} onClick={cargarUsuarios}>Recargar</Boton>}
        >
          {cargando ? (
            <p className="py-8 text-center text-sm text-slate-500">Cargando...</p>
          ) : (
            <UsuariosTabla
              usuarios={usuarios}
              idSeleccionado={usuarioEditar?.idUsuarios}
              onEditar={iniciarEdicion}
              onEliminar={setIdEliminar}
            />
          )}
        </Tarjeta>
      </div>

      <DialogoConfirmar
        abierto={idEliminar !== null}
        peligro
        titulo={`¿Eliminar el usuario #${idEliminar}?`}
        mensaje="Esta acción no se puede deshacer."
        textoConfirmar="Eliminar"
        onConfirmar={confirmarEliminar}
        onCancelar={() => setIdEliminar(null)}
      />
    </div>
  )
}

export default UsuariosPage

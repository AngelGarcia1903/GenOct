import { useState } from 'react'
import { Pencil, UserPlus, Save, X } from 'lucide-react'
import Tarjeta from './ui/Tarjeta'
import Campo from './ui/Campo'
import Boton from './ui/Boton'
import { claseInput } from './ui/estilos'

// Valores vacíos del formulario. Los nombres son iguales a las columnas/propiedades de la API.
const FORMULARIO_VACIO = {
  nombre: '',
  hobby: '',
  fechanacimiento: '',
  email: '',
}

// Componente: formulario para CREAR o EDITAR un usuario.
// Props (datos que le pasa el componente padre, UsuariosPage):
//   usuarioEditar -> usuario a editar, o null si estamos creando uno nuevo
//   onGuardar     -> función que se llama con los datos al enviar el formulario
//   onCancelar    -> función para salir del modo edición
function UsuarioForm({ usuarioEditar, onGuardar, onCancelar }) {
  // Estado: lo que el usuario va escribiendo en los inputs.
  // El valor inicial se calcula UNA vez, cuando el componente se monta:
  // si hay usuario a editar, se llena con sus datos; si no, va vacío.
  // (UsuariosPage le pone una "key" distinta a este componente por cada usuario,
  //  así React lo vuelve a montar y este valor inicial se recalcula.)
  const [formulario, setFormulario] = useState(() =>
    usuarioEditar
      ? {
          nombre: usuarioEditar.nombre,
          hobby: usuarioEditar.hobby ?? '',
          // La API manda "2026-10-06T12:23:56.45"; el input date solo acepta "2026-10-06".
          fechanacimiento: usuarioEditar.fechanacimiento.slice(0, 10),
          email: usuarioEditar.email,
        }
      : FORMULARIO_VACIO
  )

  // Un solo manejador para todos los inputs: usa el atributo "name" para saber qué campo cambió.
  function manejarCambio(evento) {
    const { name, value } = evento.target
    setFormulario({ ...formulario, [name]: value })
  }

  async function manejarEnvio(evento) {
    evento.preventDefault() // evita que el navegador recargue la página
    const exito = await onGuardar({
      ...formulario,
      hobby: formulario.hobby.trim() === '' ? null : formulario.hobby, // hobby acepta NULL en la BD
    })
    // Si se guardó bien y estábamos creando, limpiamos el formulario.
    if (exito && !usuarioEditar) {
      setFormulario(FORMULARIO_VACIO)
    }
  }

  return (
    <Tarjeta
      icono={usuarioEditar ? Pencil : UserPlus}
      titulo={usuarioEditar ? `Editar usuario #${usuarioEditar.idUsuarios}` : 'Nuevo usuario'}
      className={usuarioEditar ? 'ring-2 ring-marca-200' : ''}
    >
      <form onSubmit={manejarEnvio} className="space-y-4">
        <Campo id="nombre" etiqueta="Nombre">
          <input id="nombre" name="nombre" value={formulario.nombre} onChange={manejarCambio}
            required maxLength={100} className={claseInput()} />
        </Campo>

        <Campo id="hobby" etiqueta="Hobby" ayuda="Opcional.">
          <input id="hobby" name="hobby" value={formulario.hobby} onChange={manejarCambio}
            maxLength={150} className={claseInput()} />
        </Campo>

        <Campo id="fechanacimiento" etiqueta="Fecha de nacimiento">
          <input id="fechanacimiento" name="fechanacimiento" type="date"
            value={formulario.fechanacimiento} onChange={manejarCambio} required className={claseInput()} />
        </Campo>

        <Campo id="email" etiqueta="Email">
          <input id="email" name="email" type="email" value={formulario.email}
            onChange={manejarCambio} required maxLength={100} className={claseInput()} />
        </Campo>

        <div className="flex gap-2">
          <Boton type="submit" icono={Save} className="flex-1">
            {usuarioEditar ? 'Actualizar' : 'Guardar'}
          </Boton>
          {usuarioEditar && (
            <Boton variante="secundario" icono={X} onClick={onCancelar}>Cancelar</Boton>
          )}
        </div>
      </form>
    </Tarjeta>
  )
}

export default UsuarioForm

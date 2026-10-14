import { Pencil, Trash2 } from 'lucide-react'

// Componente: muestra la lista de usuarios en una tabla.
// No tiene estado propio: solo recibe datos y funciones por props.
//   usuarios       -> arreglo de usuarios que vino de la API
//   idSeleccionado -> id del usuario que se está editando (se resalta la fila)
//   onEditar       -> se llama con el usuario cuando se presiona "Editar"
//   onEliminar     -> se llama con el id cuando se presiona "Eliminar"
function UsuariosTabla({ usuarios, idSeleccionado, onEditar, onEliminar }) {
  if (usuarios.length === 0) {
    return <p className="py-8 text-center text-sm text-slate-500">No hay usuarios registrados.</p>
  }

  return (
    // overflow-x-auto: en pantallas chicas la tabla se desplaza horizontalmente en lugar de romper el diseño
    <div className="-mx-5 overflow-x-auto sm:-mx-6">
      <table className="min-w-full text-sm">
        <thead className="border-y border-slate-200 bg-slate-50 text-left text-xs font-semibold uppercase tracking-wide text-slate-500">
          <tr>
            <th className="px-5 py-3 sm:pl-6">ID</th>
            <th className="px-3 py-3">Nombre</th>
            <th className="px-3 py-3">Hobby</th>
            <th className="px-3 py-3 whitespace-nowrap">Nacimiento</th>
            <th className="px-3 py-3">Email</th>
            <th className="px-5 py-3 text-right sm:pr-6"><span className="sr-only">Acciones</span></th>
          </tr>
        </thead>
        <tbody className="divide-y divide-slate-100">
          {/* map() convierte cada usuario en una fila <tr>. "key" ayuda a React a identificar cada fila. */}
          {usuarios.map((usuario) => (
            <tr
              key={usuario.idUsuarios}
              className={usuario.idUsuarios === idSeleccionado ? 'bg-marca-50' : 'hover:bg-slate-50'}
            >
              <td className="px-5 py-3 font-mono text-slate-500 sm:pl-6">{usuario.idUsuarios}</td>
              <td className="px-3 py-3 font-medium text-slate-900">{usuario.nombre}</td>
              <td className="px-3 py-3 text-slate-600">{usuario.hobby ?? '—'}</td>
              <td className="px-3 py-3 whitespace-nowrap text-slate-600">{usuario.fechanacimiento.slice(0, 10)}</td>
              <td className="px-3 py-3 text-slate-600">{usuario.email}</td>
              <td className="px-5 py-3 text-right whitespace-nowrap sm:pr-6">
                <button
                  type="button"
                  onClick={() => onEditar(usuario)}
                  className="rounded-md p-1.5 text-slate-500 transition hover:bg-marca-50 hover:text-marca-700"
                  title="Editar"
                  aria-label={`Editar usuario ${usuario.idUsuarios}`}
                >
                  <Pencil className="size-4" aria-hidden="true" />
                </button>
                <button
                  type="button"
                  onClick={() => onEliminar(usuario.idUsuarios)}
                  className="rounded-md p-1.5 text-slate-500 transition hover:bg-red-50 hover:text-red-600"
                  title="Eliminar"
                  aria-label={`Eliminar usuario ${usuario.idUsuarios}`}
                >
                  <Trash2 className="size-4" aria-hidden="true" />
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}

export default UsuariosTabla

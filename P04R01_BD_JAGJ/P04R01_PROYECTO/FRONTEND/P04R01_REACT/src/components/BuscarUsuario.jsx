import { useState } from 'react'
import { Search } from 'lucide-react'
import { obtenerUsuario } from '../services/usuariosService'
import Tarjeta from './ui/Tarjeta'
import Boton from './ui/Boton'
import Alerta from './ui/Alerta'
import { claseInput } from './ui/estilos'

// Componente: busca un usuario por su ID (usa GET api/usuarios/{id}).
// Maneja su propio estado porque nadie más necesita estos datos.
function BuscarUsuario() {
  const [id, setId] = useState('')
  const [resultado, setResultado] = useState(null)
  const [mensaje, setMensaje] = useState('')

  async function buscar(evento) {
    evento.preventDefault()
    setResultado(null)
    setMensaje('')
    try {
      const usuario = await obtenerUsuario(id)
      setResultado(usuario)
    } catch (error) {
      // La API regresa 404 cuando el id no existe.
      setMensaje(error.status === 404 ? `No existe el usuario con ID ${id}.` : error.message)
    }
  }

  return (
    <Tarjeta icono={Search} titulo="Buscar por ID">
      <form onSubmit={buscar} className="flex gap-2">
        <input type="number" min="1" value={id} onChange={(e) => setId(e.target.value)} required
          placeholder="Ej. 3" aria-label="ID del usuario" className={claseInput()} />
        <Boton type="submit" variante="secundario" icono={Search}>Buscar</Boton>
      </form>

      {mensaje && <Alerta tipo="aviso" className="mt-4">{mensaje}</Alerta>}

      {resultado && (
        <dl className="mt-4 space-y-2 rounded-xl bg-slate-50 p-4 text-sm">
          <div className="flex justify-between gap-4"><dt className="text-slate-500">ID</dt><dd className="font-mono">{resultado.idUsuarios}</dd></div>
          <div className="flex justify-between gap-4"><dt className="text-slate-500">Nombre</dt><dd className="text-right font-medium">{resultado.nombre}</dd></div>
          <div className="flex justify-between gap-4"><dt className="text-slate-500">Hobby</dt><dd className="text-right">{resultado.hobby ?? '—'}</dd></div>
          <div className="flex justify-between gap-4"><dt className="text-slate-500">Nacimiento</dt><dd>{resultado.fechanacimiento.slice(0, 10)}</dd></div>
          <div className="flex justify-between gap-4"><dt className="text-slate-500">Email</dt><dd className="truncate">{resultado.email}</dd></div>
        </dl>
      )}
    </Tarjeta>
  )
}

export default BuscarUsuario

import { useEffect, useRef } from 'react'
import { TriangleAlert } from 'lucide-react'
import Boton from './Boton'

// Ventana de confirmación con el elemento nativo <dialog> (reemplaza a window.confirm).
// Props: abierto, titulo, mensaje, textoConfirmar, peligro, onConfirmar, onCancelar
function DialogoConfirmar({ abierto, titulo, mensaje, textoConfirmar = 'Confirmar', peligro = false, onConfirmar, onCancelar }) {
  // useRef guarda una referencia al elemento <dialog> real del DOM
  const ref = useRef(null)

  // Efecto: sincroniza la prop "abierto" con el diálogo del navegador (showModal / close).
  useEffect(() => {
    const dialogo = ref.current
    if (abierto && !dialogo.open) dialogo.showModal()
    if (!abierto && dialogo.open) dialogo.close()
  }, [abierto])

  return (
    <dialog
      ref={ref}
      // La tecla Esc dispara "cancel": lo tratamos igual que presionar Cancelar
      onCancel={(e) => {
        e.preventDefault()
        onCancelar()
      }}
      className="m-auto w-[min(28rem,calc(100%-2rem))] rounded-2xl bg-white p-6 shadow-xl backdrop:bg-slate-900/50"
    >
      <div className="flex gap-4">
        <span
          className={`flex size-10 shrink-0 items-center justify-center rounded-full ${
            peligro ? 'bg-red-100 text-red-600' : 'bg-marca-100 text-marca-700'
          }`}
        >
          <TriangleAlert className="size-5" aria-hidden="true" />
        </span>
        <div>
          <h3 className="text-base font-semibold text-slate-900">{titulo}</h3>
          <p className="mt-1 text-sm text-slate-600">{mensaje}</p>
        </div>
      </div>
      <div className="mt-6 flex justify-end gap-2">
        <Boton variante="secundario" onClick={onCancelar}>Cancelar</Boton>
        <Boton variante={peligro ? 'peligro' : 'primario'} onClick={onConfirmar}>
          {textoConfirmar}
        </Boton>
      </div>
    </dialog>
  )
}

export default DialogoConfirmar

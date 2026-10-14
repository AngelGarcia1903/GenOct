import { Flag, RotateCcw, CircleCheck, Circle, Lock } from 'lucide-react'
import Tarjeta from '../ui/Tarjeta'
import Boton from '../ui/Boton'

// Característica 3: botones Finalizar y Reiniciar.
// Reglas:
//   - Finalizar solo se habilita cuando se cumplen todos los requisitos y aún no se ha finalizado.
//   - Reiniciar NO se puede presionar si no se ha presionado Finalizar.
// Props: requisitos ([{ texto, cumplido }]), finalizado, guardando, onFinalizar, onReiniciar
function AccionesRegistro({ requisitos, finalizado, guardando, onFinalizar, onReiniciar }) {
  const todoListo = requisitos.every((r) => r.cumplido)
  const puedeFinalizar = todoListo && !finalizado && !guardando

  let subtitulo = 'Completa los pasos para poder finalizar.'
  if (todoListo) subtitulo = 'Todo listo. Al finalizar se guardará el registro y ya no podrás editarlo.'
  if (finalizado) subtitulo = 'Registro finalizado. Presiona Reiniciar para capturar uno nuevo.'

  return (
    <Tarjeta paso={3} completado={finalizado} icono={Flag} titulo="Finalizar" subtitulo={subtitulo}
      className="print:hidden">
      <div className="flex flex-col gap-5 sm:flex-row sm:items-end sm:justify-between">
        {/* Checklist: lo que falta (o ya se cumplió) para poder finalizar */}
        <ul className="space-y-1.5">
          {requisitos.map((r) => (
            <li key={r.texto} className={`flex items-center gap-2 text-sm ${r.cumplido ? 'text-emerald-700' : 'text-slate-500'}`}>
              {r.cumplido
                ? <CircleCheck className="size-4" aria-hidden="true" />
                : <Circle className="size-4" aria-hidden="true" />}
              {r.texto}
            </li>
          ))}
        </ul>

        <div className="flex gap-2">
          <Boton
            variante="secundario"
            icono={RotateCcw}
            onClick={onReiniciar}
            disabled={!finalizado}
            title={finalizado ? 'Empezar un registro nuevo' : 'Disponible después de finalizar'}
          >
            Reiniciar
          </Boton>
          <Boton
            variante="exito"
            icono={finalizado ? Lock : Flag}
            onClick={onFinalizar}
            cargando={guardando}
            disabled={!puedeFinalizar}
            title={puedeFinalizar ? 'Guardar y bloquear la información' : 'Completa los pasos pendientes'}
          >
            {finalizado ? 'Finalizado' : guardando ? 'Guardando' : 'Finalizar'}
          </Boton>
        </div>
      </div>
    </Tarjeta>
  )
}

export default AccionesRegistro

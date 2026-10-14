import { useState } from 'react'
import { ArrowDownAZ, ArrowDownZA, ListOrdered } from 'lucide-react'

const ORDENES = [
  { id: 'asc', texto: 'A → Z', Icono: ArrowDownAZ },
  { id: 'desc', texto: 'Z → A', Icono: ArrowDownZA },
  { id: 'original', texto: 'Original', Icono: ListOrdered },
]

// Lista de preposiciones encontradas en el nombre, con opción de ordenarla alfabéticamente.
// Props: lista -> arreglo de strings, en el orden en que aparecen en el nombre
function Preposiciones({ lista }) {
  const [orden, setOrden] = useState('original') // 'original' | 'asc' | 'desc'

  // Estado derivado: la lista ordenada se calcula al dibujar.
  // [...lista] crea una COPIA: sort() modifica el arreglo y las props no se deben modificar.
  let visibles = [...lista]
  if (orden === 'asc') visibles.sort((a, b) => a.localeCompare(b, 'es'))
  if (orden === 'desc') visibles.sort((a, b) => b.localeCompare(a, 'es'))

  return (
    <div className="rounded-xl border border-slate-200 p-4">
      <div className="flex flex-wrap items-center justify-between gap-3">
        <h3 className="text-sm font-semibold text-slate-900">
          Preposiciones encontradas <span className="text-slate-500">({lista.length})</span>
        </h3>

        {lista.length > 0 && (
          // Control segmentado: el botón del orden activo se ve "presionado" (aria-pressed)
          <div className="inline-flex rounded-lg bg-slate-100 p-0.5" role="group" aria-label="Ordenar preposiciones">
            {ORDENES.map(({ id, texto, Icono }) => (
              <button
                key={id}
                type="button"
                onClick={() => setOrden(id)}
                aria-pressed={orden === id}
                className={`inline-flex items-center gap-1.5 rounded-md px-2.5 py-1 text-xs font-medium transition ${
                  orden === id ? 'bg-white text-marca-700 shadow-sm' : 'text-slate-600 hover:text-slate-900'
                }`}
              >
                <Icono className="size-3.5" aria-hidden="true" />
                {texto}
              </button>
            ))}
          </div>
        )}
      </div>

      {lista.length === 0 ? (
        <p className="mt-2 text-sm text-slate-500">No se encontraron preposiciones en el nombre.</p>
      ) : (
        <ol className="mt-3 flex flex-wrap gap-2">
          {visibles.map((p, i) => (
            <li key={p} className="inline-flex items-center gap-1.5 rounded-full bg-marca-50 px-3 py-1 text-sm font-medium text-marca-700 ring-1 ring-inset ring-marca-200">
              <span className="text-xs text-marca-500">{i + 1}.</span>
              {p}
            </li>
          ))}
        </ol>
      )}
    </div>
  )
}

export default Preposiciones

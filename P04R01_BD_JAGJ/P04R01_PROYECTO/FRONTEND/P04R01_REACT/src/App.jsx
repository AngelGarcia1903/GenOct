import { useState } from 'react'
import { Toaster } from 'sonner'
import { ClipboardList, Users, ShieldCheck } from 'lucide-react'
import RegistroPage from './pages/RegistroPage'
import UsuariosPage from './pages/UsuariosPage'

const PESTANAS = [
  { id: 'registro', texto: 'Registro Thona', Icono: ClipboardList },
  { id: 'usuarios', texto: 'Usuarios (CRUD)', Icono: Users },
]

// Componente raíz: encabezado, pestañas y la página activa. Sin react-router; basta un estado.
function App() {
  const [vista, setVista] = useState('registro') // 'registro' | 'usuarios'

  return (
    <div className="min-h-screen">
      {/* print:hidden -> no aparece al imprimir la ficha del registro */}
      <header className="border-b border-slate-200 bg-white print:hidden">
        <div className="mx-auto flex max-w-6xl flex-wrap items-center justify-between gap-4 px-4 py-4">
          <div className="flex items-center gap-3">
            <span className="flex size-10 items-center justify-center rounded-xl bg-marca-600 text-white">
              <ShieldCheck className="size-6" aria-hidden="true" />
            </span>
            <div>
              <p className="text-base font-bold leading-tight text-slate-900">Plataforma Digital</p>
              <p className="text-xs text-slate-500">Prueba técnica · Thona Seguros</p>
            </div>
          </div>

          <nav className="flex gap-1 rounded-xl bg-slate-100 p-1" aria-label="Secciones">
            {PESTANAS.map(({ id, texto, Icono }) => (
              <button
                key={id}
                type="button"
                onClick={() => setVista(id)}
                aria-current={vista === id ? 'page' : undefined}
                className={`inline-flex items-center gap-2 rounded-lg px-3 py-1.5 text-sm font-medium transition ${
                  vista === id ? 'bg-white text-marca-700 shadow-sm' : 'text-slate-600 hover:text-slate-900'
                }`}
              >
                <Icono className="size-4" aria-hidden="true" />
                {texto}
              </button>
            ))}
          </nav>
        </div>
      </header>

      <main className="mx-auto max-w-6xl px-4 py-8">
        {vista === 'registro' ? <RegistroPage /> : <UsuariosPage />}
      </main>

      {/* Contenedor de notificaciones (toasts). Se muestran con toast.success('...') desde cualquier componente */}
      <Toaster position="top-right" richColors closeButton />
    </div>
  )
}

export default App

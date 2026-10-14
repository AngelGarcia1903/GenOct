import react from '@vitejs/plugin-react'
import tailwindcss from '@tailwindcss/vite'
import { defineConfig } from 'vite'

// https://vite.dev/config/
export default defineConfig({
  // tailwindcss(): genera solo las clases de Tailwind que se usan en el código
  plugins: [react(), tailwindcss()],
})

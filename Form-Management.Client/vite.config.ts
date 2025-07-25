import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5000
  },
  preview: {
    port: 5000,
    host: true,
    strictPort: true,
    allowedHosts: [process.env.ALLOWED_CLIENT_HOSTS!]
  }
})
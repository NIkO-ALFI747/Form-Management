import { type FC } from 'react'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.bundle.min.js'
import './App.css'
import AppRouter from './components/Routers/AppRouter.tsx'

const App: FC = () => {
  return (
    <AppRouter />
  )
}

export default App
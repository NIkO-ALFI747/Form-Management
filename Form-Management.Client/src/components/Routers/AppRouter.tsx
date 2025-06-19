import { type FC, useState, useEffect } from 'react'
import { BrowserRouter, Routes, Route, Navigate } from 'react-router'
import UsersManagementPage from '../../pages/UsersManagementPage.tsx'
import SignUpPage from '../../pages/SignUpPage.tsx'
import LoginPage from '../../pages/LoginPage.tsx'
import HomePage from '../../pages/HomePage.tsx'
import ErrorPage from '../../pages/ErrorPage.tsx'

const AppRouter: FC = () => {

  const [isAuth, setIsAuth] = useState<boolean>(() => {
    return sessionStorage.getItem('isAuth') === 'true'
  })

  useEffect(() => {
    sessionStorage.setItem('isAuth', String(isAuth))
  }, [isAuth])

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<HomePage isAuth={isAuth} setIsAuth={setIsAuth} />} />
        <Route path="/users_management" element={
          isAuth ? <UsersManagementPage isAuth={isAuth} setIsAuth={setIsAuth} /> :
            <Navigate to="/login" />
        } />
        <Route path="/signup" element={
          isAuth ? <Navigate to="/users_management" /> :
            <SignUpPage setIsAuth={setIsAuth} />
        } />
        <Route path="/login" element={
          isAuth ? <Navigate to="/users_management" /> :
            <LoginPage setIsAuth={setIsAuth} />
        } />
        <Route path="/error" element={<ErrorPage />} />
        <Route path="*" element={<Navigate to="/error" />} />
      </Routes>
    </BrowserRouter>
  )
}

export default AppRouter
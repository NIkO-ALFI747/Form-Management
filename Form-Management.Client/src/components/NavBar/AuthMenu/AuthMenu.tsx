import { type FC } from 'react'
import { Nav } from 'react-bootstrap/'
import SignOutItem from './SingOutItem.tsx'
import NavLink from '../NavLink.tsx'

interface AuthMenuProps {
  currentPath: string
  isAuth: boolean
  setIsAuth: React.Dispatch<React.SetStateAction<boolean>>
}

const AuthMenu: FC<AuthMenuProps> = ({ currentPath, isAuth, setIsAuth }) => {
  return (
    <Nav className="ms-auto align-items-center fs-5">
      {isAuth ? <SignOutItem setIsAuth={setIsAuth}>Sign Out</SignOutItem> : ''}
      {!isAuth ? <NavLink currentPath={currentPath} to='/login'>Login</NavLink> : ''}
      {!isAuth ? <NavLink currentPath={currentPath} to='/signup'>Sign Up</NavLink> : ''}
    </Nav>
  )
}

export default AuthMenu
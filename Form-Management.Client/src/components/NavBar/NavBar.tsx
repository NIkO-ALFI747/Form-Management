import { type FC } from 'react'
import { NavLink as RouterNavLink, useLocation } from 'react-router'
import { Container, Navbar, Nav } from 'react-bootstrap/'
import { FaWpforms } from 'react-icons/fa'
import AuthMenu from './AuthMenu/AuthMenu.tsx'
import NavLink from './NavLink.tsx'

interface NavBarProps {
  isAuth: boolean
  setIsAuth: React.Dispatch<React.SetStateAction<boolean>>
}

const NavBar: FC<NavBarProps> = ({ isAuth, setIsAuth }) => {

  const location = useLocation()

  const currentPath = location.pathname

  return (
    <Navbar collapseOnSelect expand="sm" className="bg-body-secondary">
      <Container fluid>
        <RouterNavLink to='/' className="navbar-brand">
          <FaWpforms className="me-2 mb-1" />
          Forms
        </RouterNavLink>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="flex-grow-1 justify-content-center align-items-center fs-5">
            <NavLink currentPath={currentPath} to='/' activation={true}>Home</NavLink>
            {isAuth ?
              <NavLink currentPath={currentPath} to='/users_management' activation={true}>Users</NavLink> : ''
            }
          </Nav>
          <AuthMenu currentPath={currentPath} isAuth={isAuth} setIsAuth={setIsAuth} />
        </Navbar.Collapse>
      </Container>
    </Navbar>
  )
}

export default NavBar
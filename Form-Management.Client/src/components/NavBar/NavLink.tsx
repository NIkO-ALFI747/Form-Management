import { type FC, type PropsWithChildren } from 'react'
import { NavLink as RouterNavLink } from 'react-router'

interface NavLinkProps extends PropsWithChildren {
  currentPath: string
  to: string
  activation?: boolean
}

const NavLink: FC<NavLinkProps> = ({ currentPath, to, children, activation }) => {
  return (
    <RouterNavLink
      className={`nav-link ${(currentPath == to && activation) ? 'fw-bold active' : ''}`}
      aria-current={(currentPath == to && activation) ? "page" : "false"}
      to={to}
    >
      {children}
    </RouterNavLink>
  )
}

export default NavLink
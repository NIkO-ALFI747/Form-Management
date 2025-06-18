import { type FC, type PropsWithChildren } from 'react'
import { Button } from 'react-bootstrap/'

interface SignOutItemProps extends PropsWithChildren {
  setIsAuth: React.Dispatch<React.SetStateAction<boolean>>
}

const SignOutItem: FC<SignOutItemProps> = ({ setIsAuth, children }) => {
  return (
    <Button
      variant="link"
      className="nav-link text-decoration-none"
      onClick={() => setIsAuth(false)}
    >
      {children}
    </Button>
  )
}

export default SignOutItem
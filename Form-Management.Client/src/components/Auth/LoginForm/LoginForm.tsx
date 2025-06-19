import { useState, type FC } from 'react'
import { Form } from 'react-bootstrap/'
import InputGroup from './InputGroup.tsx'
import SubmitSection from '../SubmitSection.tsx'
import type { LoginRequest } from '../../../contracts/LoginRequest.tsx'

interface LoginFormProps {
  setIsAuth: React.Dispatch<React.SetStateAction<boolean>>
}

const LoginForm: FC<LoginFormProps> = ({ setIsAuth }) => {

  const [successfulLogin, setSuccessfulLogin] = useState<boolean>(true)

  const [user, setUser] = useState<LoginRequest | null>(null)

  const loginUser = async () => {
    setIsAuth(true)
  }

  const onSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    try {
      await loginUser()
    } catch {
      setSuccessfulLogin(false)
    }
    setUser(null)
  }

  return (
    <Form onSubmit={onSubmit}>
      <InputGroup user={user} setUser={setUser} />
      <SubmitSection successfulAuth={successfulLogin}
        setSuccessfulAuth={setSuccessfulLogin}
        btnTitle='Login'
        alertTitle='Failed to login!'
      />
    </Form>
  )
}

export default LoginForm
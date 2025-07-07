import { useState, type FC } from 'react'
import { AxiosError } from 'axios'
import { Form } from 'react-bootstrap/'
import InputGroup from './InputGroup.tsx'
import SubmitSection from '../SubmitSection.tsx'
import type { LoginRequest } from '../../../contracts/LoginRequest.tsx'
import { loginUser as loginUserService } from '../../../services/users.ts'

interface LoginFormProps {
  setIsAuth: React.Dispatch<React.SetStateAction<boolean>>
}

const LoginForm: FC<LoginFormProps> = ({ setIsAuth }) => {

  const [successfulLogin, setSuccessfulLogin] = useState<boolean>(true)

  const [user, setUser] = useState<LoginRequest | null>(null)

  const [alertTitle, setAlertTitle] = useState<string>('Failed to login!')

  const [passwordErrorMessage, _] = useState<string>("Please provide a valid password.");

  const loginUser = async (user: LoginRequest) => {
    try {
      await loginUserService(user)
      setIsAuth(true)
    } catch (e) {
      const alertTitle = (e as AxiosError).response?.data as string | undefined
      alertTitle && setAlertTitle(alertTitle)
      setSuccessfulLogin(false)
    }
  }

  const onSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    await loginUser(user!)
    setUser(null)
  }

  return (
    <Form onSubmit={onSubmit}>
      <InputGroup user={user} setUser={setUser} passwordErrorMessage={passwordErrorMessage} />
      <SubmitSection successfulAuth={successfulLogin}
        setSuccessfulAuth={setSuccessfulLogin}
        btnTitle='Login'
        alertTitle={alertTitle}
      />
    </Form>
  )
}

export default LoginForm
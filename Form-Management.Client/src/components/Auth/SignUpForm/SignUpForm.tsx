import { useState, type FC } from 'react'
import { Form } from 'react-bootstrap/'
import InputGroup from './InputGroup.tsx'
import SubmitSection from '../SubmitSection.tsx'
import type { SignUpRequest } from '../../../contracts/SignUpRequest.tsx'
import { signUpUser as signUpUserService } from '../../../services/users.ts'

interface SignUpFormProps {
  setIsAuth: React.Dispatch<React.SetStateAction<boolean>>
}

const SignUpForm: FC<SignUpFormProps> = ({ setIsAuth }) => {

  const [successfulSignUp, setSuccessfulSignUp] = useState<boolean>(true)

  const [user, setUser] = useState<SignUpRequest | null>(null)

  const signUpUser = async (user: SignUpRequest) => {
    await signUpUserService(user)
    setIsAuth(true)
  }

  const onSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    try {
      await signUpUser(user!)
    } catch {
      setSuccessfulSignUp(false)
    }
    setUser(null)
  }

  return (
    <Form onSubmit={onSubmit}>
      <InputGroup user={user} setUser={setUser} />
      <SubmitSection successfulAuth={successfulSignUp}
        setSuccessfulAuth={setSuccessfulSignUp}
        btnTitle='Sign Up'
        alertTitle='Failed to sign up!'
      />
    </Form>
  )
}

export default SignUpForm
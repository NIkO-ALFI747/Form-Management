import { type FC } from 'react'
import type { LoginRequest } from "../../../contracts/LoginRequest.tsx"
import UsersEmail from '../Inputs/UsersEmail.tsx'
import UsersPassword from '../Inputs/UsersPassword.tsx'

interface InputGroupProps {
  user: LoginRequest | null
  setUser: React.Dispatch<React.SetStateAction<LoginRequest | null>>
  passwordErrorMessage: string
}

const InputGroup: FC<InputGroupProps> = ({ user, setUser, passwordErrorMessage }) => {
  return (
    <>
      <UsersEmail<LoginRequest> user={user} setUser={setUser} />
      <UsersPassword<LoginRequest> user={user} setUser={setUser} errorMessage={passwordErrorMessage} />
    </>
  )
}

export default InputGroup
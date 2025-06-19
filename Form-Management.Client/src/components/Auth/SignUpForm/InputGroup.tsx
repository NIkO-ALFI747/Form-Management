import { type FC } from 'react'
import type { SignUpRequest } from "../../../contracts/SignUpRequest.tsx"
import UsersName from '../Inputs/UsersName.tsx'
import UsersEmail from '../Inputs/UsersEmail.tsx'
import UsersPassword from '../Inputs/UsersPassword.tsx'

interface InputGroupProps {
  user: SignUpRequest | null
  setUser: React.Dispatch<React.SetStateAction<SignUpRequest | null>>
}

const InputGroup: FC<InputGroupProps> = ({ user, setUser }) => {
  return (
    <>
      <UsersName<SignUpRequest> user={user} setUser={setUser} />
      <UsersEmail<SignUpRequest> user={user} setUser={setUser} />
      <UsersPassword<SignUpRequest> user={user} setUser={setUser} />
    </>
  )
}

export default InputGroup
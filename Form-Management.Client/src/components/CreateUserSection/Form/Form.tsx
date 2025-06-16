import { useState, type FC } from 'react'
import { Form as BtsForm } from 'react-bootstrap/'
import InputGroup from './InputGroup/InputGroup.tsx'
import SubmitSection from './SubmitSection.tsx'
import type { CreateUserRequest } from '../../../contracts/CreateUserRequest.tsx'
import { createUser as createUserService } from '../../../services/users.ts'

const Form: FC = () => {

  const [submitted, setSubmitted] = useState<boolean>(false)

  const [user, setUser] = useState<CreateUserRequest | null>(null)

  const createUser = async (user: CreateUserRequest) => {
    await createUserService(user)
    setUser(null)
  }

  const onSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    await createUser(user!)
    setSubmitted(true)
  }

  return (
    <BtsForm onSubmit={onSubmit}>
      <InputGroup user={user} setUser={setUser} />
      <SubmitSection submitted={submitted} setSubmitted={setSubmitted} />
    </BtsForm>
  )
}

export default Form
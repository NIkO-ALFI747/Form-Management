import { useState, useRef, useEffect, type FC } from 'react'
import { Form as BootstrapForm } from 'react-bootstrap/'
import InputGroup from './InputGroup/InputGroup.tsx'
import SubmitSection from './SubmitSection.tsx'
import type { CreateUserRequest } from '../../../contracts/CreateUserRequest.tsx'
import { createUser } from '../../../services/users.ts'

const Form: FC = () => {
  const [submitted, setSubmitted] = useState<boolean>(false)
  const timeoutRef = useRef<number | null>(null)
  const [user, setUser] = useState<CreateUserRequest | null>(null)

  const onCreate = async (user: CreateUserRequest) => {
    await createUser(user)
    setUser(null)
  }

  useEffect(() => {
    if (submitted) {
      if (timeoutRef.current) window.clearTimeout(timeoutRef.current)
      timeoutRef.current = window.setTimeout(() => {
        setSubmitted(false)
        timeoutRef.current = null
      }, 1000)
    }
  }, [submitted])

  const onSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    await onCreate(user!)
    setSubmitted(true)
  }

  return (
    <BootstrapForm onSubmit={onSubmit}>
      <InputGroup user={user} setUser={setUser} />
      <SubmitSection submitted={submitted} />
    </BootstrapForm>
  )
}

export default Form
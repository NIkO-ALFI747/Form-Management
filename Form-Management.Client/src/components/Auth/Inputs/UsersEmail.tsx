import { type JSX } from 'react'
import Email from './Email.tsx'

interface UsersEmailProps<T> {
  user: T | null
  setUser: React.Dispatch<React.SetStateAction<T | null>>
}

const UsersEmail = <T extends { email: string }>(
  { user, setUser }: UsersEmailProps<T>
): JSX.Element => {

  const setEmail = (email: string) => setUser({ ...user!, email })

  return (
    <Email email={user?.email} onChangeEmail={setEmail} />
  )
}

export default UsersEmail
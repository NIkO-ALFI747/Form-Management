import { type JSX } from 'react'
import Name from './Name.tsx'

interface UsersNameProps<T> {
  user: T | null
  setUser: React.Dispatch<React.SetStateAction<T | null>>
}

const UsersName = <T extends { name: string }>(
  { user, setUser }: UsersNameProps<T>
): JSX.Element => {

  const setName = (name: string) => setUser({ ...user!, name })

  return (
    <Name name={user?.name} onChangeName={setName} />
  )
}

export default UsersName
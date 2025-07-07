import { type ChangeEvent, type JSX } from 'react'
import Name from './Name.tsx'

interface UsersNameProps<T> {
  user: T | null
  setUser: React.Dispatch<React.SetStateAction<T | null>>
}

const UsersName = <T extends { name: string }>(
  { user, setUser }: UsersNameProps<T>
): JSX.Element => {

  const setName = (e: ChangeEvent<HTMLInputElement>) => setUser({ ...user!, name: e.target.value })

  return (
    <Name name={user?.name!} onChangeName={setName} />
  )
}

export default UsersName
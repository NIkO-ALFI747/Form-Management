import { type JSX } from 'react'
import Password from './Password.tsx'

interface UsersPasswordProps<T> {
  user: T | null
  setUser: React.Dispatch<React.SetStateAction<T | null>>
}

const UsersPassword = <T extends { password: string }>(
  { user, setUser }: UsersPasswordProps<T>
): JSX.Element => {

  const setPassword = (password: string) => setUser({ ...user!, password })

  return (
    <Password password={user?.password} onChangePassword={setPassword} />
  )
}

export default UsersPassword
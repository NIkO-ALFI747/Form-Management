import { type ChangeEvent, type JSX } from 'react'
//import Password from './Password.tsx'
import Password2 from './Password2.tsx'

interface UsersPasswordProps<T> {
  user: T | null
  setUser: React.Dispatch<React.SetStateAction<T | null>>
  errorMessage: string
}

const UsersPassword = <T extends { password: string }>(
  { user, setUser, /*errorMessage*/ }: UsersPasswordProps<T>
): JSX.Element => {

  const setPassword = (e: ChangeEvent<HTMLInputElement>) => setUser({ ...user!, password: e.target.value })

  return (
    <Password2 password={user?.password!} onChangePassword={setPassword} /*errorMessage={errorMessage}*/ />
  )
}

export default UsersPassword
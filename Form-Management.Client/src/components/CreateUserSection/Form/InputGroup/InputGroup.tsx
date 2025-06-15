import { type FC } from 'react'
import Name from './Name.tsx'
import Email from './Email.tsx'
import Password from './Password.tsx'
import type { InputGroupProps } from './InputGroupProps.tsx'

const InputGroup: FC<InputGroupProps> = ({user, setUser}) => {
  return (
    <>
      <Name user={user} setUser={setUser}/>
      <Email user={user} setUser={setUser}/>
      <Password user={user} setUser={setUser}/>
    </>
  )
}

export default InputGroup
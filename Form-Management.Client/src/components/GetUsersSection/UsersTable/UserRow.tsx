import { type FC } from 'react'
import type { IUser } from '../../../types/User.tsx'

interface UserRowProps {
  user: IUser
}

const UserRow: FC<UserRowProps> = ({ user }) => {
  return (
    <tr>
      {
        Object.entries(user).map(([key, value]) => (
          <td key={key}>{String(value)}</td>
        ))
      }
    </tr>
  )
}

export default UserRow
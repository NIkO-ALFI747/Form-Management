import { type FC } from 'react'
import type { User } from '../../../types/User.tsx'
import RowCheckbox from './RowCheckbox.tsx'

interface TRowProps {
  userIndex: number
  user: User
  setUsers: React.Dispatch<React.SetStateAction<User[]>>
  setSelectedCount: React.Dispatch<React.SetStateAction<number>>
}

const TRow: FC<TRowProps> = ({ userIndex, user, setUsers, setSelectedCount }) => {
  return (
    <tr>
      <td>
        <RowCheckbox userIndex={userIndex} user={user}
          setUsers={setUsers} setSelectedCount={setSelectedCount}
        />
      </td>
      {
        Object.entries(user).map(([key, value]) => (
          key !== "isSelected" ? <td key={key}>{String(value)}</td> : null
        ))
      }
    </tr>
  )
}

export default TRow
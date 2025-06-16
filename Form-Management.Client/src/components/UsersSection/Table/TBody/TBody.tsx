import { type FC } from 'react'
import type { User } from '../../types/User.tsx'
import TRow from './TRow/TRow.tsx'

interface TBodyProps {
  users: User[]
  setUsers: React.Dispatch<React.SetStateAction<User[]>>
  setSelectedCount: React.Dispatch<React.SetStateAction<number>>
}

const TBody: FC<TBodyProps> = ({ users, setUsers, setSelectedCount }) => {
  return (
    <tbody>
      {
        users.map((user, index) =>
          <TRow userIndex={index} user={user}
            setUsers={setUsers}
            setSelectedCount={setSelectedCount}
            key={user.id}
          />
        )
      }
    </tbody>
  )
}

export default TBody
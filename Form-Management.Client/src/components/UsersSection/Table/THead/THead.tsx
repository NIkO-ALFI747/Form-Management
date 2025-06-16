import { type FC } from 'react'
import type { User } from '../../types/User.tsx'
import HeadCheckbox from './HeadCheckbox.tsx'

interface THeadProps {
  users: User[]
  setUsers: React.Dispatch<React.SetStateAction<User[]>>
  selectedCount: number
  setSelectedCount: React.Dispatch<React.SetStateAction<number>>
}

const THead: FC<THeadProps> = ({ users, setUsers, selectedCount, setSelectedCount }) => {
  return (
    <thead>
      <tr>
        <th>
          <HeadCheckbox users={users} setUsers={setUsers} selectedCount={selectedCount} setSelectedCount={setSelectedCount} />
        </th>
        <th>#</th>
        <th>Name</th>
        <th>Email</th>
        <th>Password</th>
      </tr>
    </thead>
  )
}

export default THead
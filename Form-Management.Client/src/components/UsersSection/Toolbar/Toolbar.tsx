import { useState, type FC } from 'react'
import LoadUsersButton from './LoadUsersButton.tsx'
import DeleteButton from './DeleteButton.tsx'
import type { User } from '../types/User.tsx'

interface ToolbarProps {
  isLoading: boolean
  users: User[]
  setUsers: React.Dispatch<React.SetStateAction<User[]>>
  selectedCount: number
  setSelectedCount: React.Dispatch<React.SetStateAction<number>>
  loadUsers: () => Promise<void>
}

const Toolbar: FC<ToolbarProps> = ({ isLoading, users, setUsers,
  selectedCount, setSelectedCount, loadUsers }) => {

  const [isDeleting, setDeleting] = useState<boolean>(false)

  return (
    <div className="d-flex justify-content-between align-items-center mb-1">
      <LoadUsersButton isLoading={isLoading} loadUsers={loadUsers} isDeleting={isDeleting} />
      <DeleteButton selectedCount={selectedCount} setSelectedCount={setSelectedCount}
        isDeleting={isDeleting} setDeleting={setDeleting} isLoading={isLoading}
        users={users} setUsers={setUsers}
      />
    </div>
  )
}

export default Toolbar
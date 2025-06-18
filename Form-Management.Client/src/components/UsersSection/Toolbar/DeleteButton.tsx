import { type FC } from 'react'
import Button from 'react-bootstrap/Button'
import { BsTrashFill } from 'react-icons/bs'
import type { User } from '../types/User.tsx'
import { deleteUsers } from '../../../services/users.ts'

interface DeleteButtonProps {
  selectedCount: number
  setSelectedCount: React.Dispatch<React.SetStateAction<number>>
  isDeleting: boolean
  setDeleting: React.Dispatch<React.SetStateAction<boolean>>
  isLoading: boolean
  users: User[]
  setUsers: React.Dispatch<React.SetStateAction<User[]>>
}

const DeleteButton: FC<DeleteButtonProps> = ({ selectedCount, setSelectedCount,
  isDeleting, setDeleting, isLoading, users, setUsers }) => {

  const onDeleteUsers = async () => {
    setDeleting(true)
    await deleteUsers(users
      .filter(u => u.isSelected)
      .map(u => u.id)
    )
    setUsers(prevUsers => prevUsers.filter(prU => !prU.isSelected))
    setSelectedCount(0)
    setDeleting(false)
  }

  return (
    <Button
      className="btn-light text-danger border border-danger-subtle"
      title="Delete"
      disabled={isDeleting || selectedCount === 0 || isLoading}
      onClick={!(isDeleting && selectedCount === 0 && isLoading) ? onDeleteUsers : undefined}
    >
      <BsTrashFill className="mb-1"/>
    </Button>
  )
}

export default DeleteButton
import { type FC } from 'react'
import Button from 'react-bootstrap/Button'
import type { User } from '../types/User'

interface DeleteButtonProps {
  selectedCount: number
  setSelectedCount: React.Dispatch<React.SetStateAction<number>>
  isDeleting: boolean
  setDeleting: React.Dispatch<React.SetStateAction<boolean>>
  isLoading: boolean
  setUsers: React.Dispatch<React.SetStateAction<User[]>>
}

const DeleteButton: FC<DeleteButtonProps> = ({ selectedCount, setSelectedCount,
  isDeleting, setDeleting, isLoading, setUsers }) => {

  const onDeleteUsers = () => {
    setDeleting(true)
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
      <i className="bi bi-trash-fill"></i>
    </Button>
  )
}

export default DeleteButton
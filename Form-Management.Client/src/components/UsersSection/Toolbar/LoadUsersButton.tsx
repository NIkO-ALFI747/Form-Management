import { type FC } from 'react'
import Button from 'react-bootstrap/Button'

interface LoadUsersButtonProps {
  isLoading: boolean
  loadUsers: () => Promise<void>
  isDeleting: boolean
}

const LoadUsersButton: FC<LoadUsersButtonProps> = ({ isLoading, loadUsers, isDeleting }) => {
  
  const onLoadUsers = () => loadUsers()
  
  return (
    <Button
      variant="primary"
      disabled={isLoading || isDeleting}
      onClick={!(isLoading || isDeleting) ? onLoadUsers : undefined}
    >
      {isLoading ? 'Loading…' : 'Load users'}
    </Button>
  )
}

export default LoadUsersButton